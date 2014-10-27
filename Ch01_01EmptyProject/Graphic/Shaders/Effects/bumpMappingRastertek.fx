Texture2D shaderTextures[3];
SamplerState SampleType;

cbuffer MatrixBuffer
{
	matrix worldMatrix;
	matrix viewMatrix;
	matrix projectionMatrix;
};

cbuffer CameraBuffer
{
	float3 cameraPosition;
	float padding;
};

cbuffer LightBuffer
{
	float4 diffuseColor;
	float3 lightDirection;
};

struct VertexInputType
{
	float4 position : POSITION;
	float2 tex : TEXCOORD0;
	float3 normal : NORMAL;
	float3 tangent : TANGENT;
	float3 binormal : BINORMAL;
};
//PixelInputType
struct VertexShaderOutput
{
	float4 position : SV_POSITION;
	float2 tex : TEXCOORD0;
	
	float3 normal : NORMAL;
	float3 tangent : TANGENT;
	float3 binormal : BINORMAL;

	float3 halfVector : TEXCOORD1;
	float3 lightDirection : TEXCOORD2;
	float3 worldPosition : TEXCOORD3;

	float4 diffuseColor : TEXCOORD4;
};

//Transforming verticves and doing color calculatios
VertexShaderOutput BumpMapVertexShader(VertexInputType input)
{
	VertexShaderOutput output;
	// Change the position vector to be 4 units for proper matrix calculations.
	input.position.w = 1.0f;
	output.position = mul(input.position, worldMatrix);
	output.position = mul(output.position, viewMatrix);
	output.position = mul(output.position, projectionMatrix);

	float3 worldPosition = mul(output.position, worldMatrix).xyz;
	float3 lightDir = -lightDirection;
	float3 viewDirection = cameraPosition - worldPosition;
	float3 halfVector = normalize(normalize(lightDirection) + normalize(viewDirection));

	output.normal = mul(input.normal, (float3x3)worldMatrix);
	output.normal = normalize(output.normal);

	output.tangent = mul(input.tangent, (float3x3)worldMatrix);
	output.tangent = normalize(output.tangent);

	output.binormal = mul(input.binormal, (float3x3)worldMatrix);
	output.binormal = normalize(output.binormal);
		

	//here we make out tbn matrix used for calc. of texel (halfVector)
	float3x3 tbnMatrix = float3x3(input.tangent.x, input.binormal.x, input.normal.x,
	input.tangent.y, input.binormal.y, input.normal.y,
	input.tangent.z, input.binormal.z, input.normal.z
	);

	
	output.tex = input.tex;
	output.halfVector = mul(halfVector, tbnMatrix);
	output.lightDirection = lightDir; //mul(lightDir, tbnMatrix);
	
	output.diffuseColor = diffuseColor;
	
	return output;
}

float4 BumpMapPixelShader(VertexShaderOutput input) : SV_TARGET
{
	bool useParallaxMapping = false;
	float2 computedTextureCoords;
	float3 h = normalize(input.halfVector);
	
	if (useParallaxMapping == true)
	{
		float scale = 0.50f;
		float bias = 0.30f;
		
		float height = shaderTextures[2].Sample(SampleType, input.tex).r;
		height = height * scale + bias; //this can be wrong
		computedTextureCoords = input.tex + (height *  h.xy);
	}
	else
	{
		computedTextureCoords = input.tex;
	}

	float3 normalizedLight = normalize(input.lightDirection);

	//sample bump map
	float4 bumpMap = shaderTextures[1].Sample(SampleType, input.tex);
	// Expand the range of the normal value from (0, +1) to (-1, +1).
	bumpMap = (bumpMap * 2.0f) - 1.0f;
	//Calculate the normal from the data in the bump map
	float3 bumpNormal = (bumpMap.x * input.tangent) + (bumpMap.y * input.binormal) + (bumpMap.z * input.normal);
	//normalize the resulting bump normal
	bumpNormal = normalize(bumpNormal);
	//calculate amount of the light based on normal value
	float lightIntensity = saturate(dot(bumpNormal, normalizedLight));
	//sample texture pixel
	float4 textureColor = shaderTextures[0].Sample(SampleType, computedTextureCoords);
	// Determine the final diffuse color based on the diffuse color and the amount of light intensity.
	float4 color = saturate(input.diffuseColor * lightIntensity);
	color = color * textureColor;
	return color;
}

technique11 BumpMap
{
	pass Pass1
	{
		SetVertexShader(CompileShader(vs_5_0, BumpMapVertexShader()));
		SetPixelShader(CompileShader(ps_5_0, BumpMapPixelShader()));
	}
}