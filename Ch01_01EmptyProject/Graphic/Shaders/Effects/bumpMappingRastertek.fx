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
	//float3 halfVector : TEXCOORD1;
	float3 worldPosition : POS;
};

//Transforming verticves and doing color calculatios
VertexShaderOutput BumpMapVertexShader(VertexInputType input)
{
	VertexShaderOutput output;
	// Change the position vector to be 4 units for proper matrix calculations.
	input.position.w = 1.0f;

	output.position = mul(input.position, worldMatrix);

	// Calculate the position of the vertex against the world, view, and projection matrices.
	//output.position = worldPosition;
	output.position = mul(output.position, viewMatrix);
	output.position = mul(output.position, projectionMatrix);

	//i need to use fully initialized positions
	float3 worldPosition = mul(output.position, worldMatrix).xyz;
		output.worldPosition = worldPosition;

	output.tex = input.tex;

	output.normal = mul(input.normal, (float3x3)worldMatrix);
	output.normal = normalize(output.normal);

	output.tangent = mul(input.tangent, (float3x3)worldMatrix);
	output.tangent = normalize(output.tangent);

	output.binormal = mul(input.binormal, (float3x3)worldMatrix);
	output.binormal = normalize(output.binormal);

	return output;
}

float4 BumpMapPixelShader(VertexShaderOutput input) : SV_TARGET
{
	float4 textureColor;
	float4 bumpMap;
	float3 bumpNormal;
	float3 lightDir;
	float lightIntensity;
	float4 color;

	//I could compute tbn matrix at pixel shader(?)
	//here we make out tbn matrix used for calc. of texel (halfVector)
	float3x3 tbnMatrix = float3x3(input.tangent.x, input.binormal.x, input.normal.x,
		input.tangent.y, input.binormal.y, input.normal.y,
		input.tangent.z, input.binormal.z, input.normal.z
		);

	//position could be wrong
	float3 viewDirection = cameraPosition - input.worldPosition;
		//	//	float3 halfVector =	float3 viewDir = cameraPosition - worldPos; normalize(lightDirection) + normalize(viewDirection);
		float3 halfVector = normalize(normalize(lightDirection) + normalize(viewDirection));

		//	halfVector = mul(halfVector, tbnMatrix);

		//sample bump map
		bumpMap = shaderTextures[1].Sample(SampleType, input.tex);
	// Expand the range of the normal value from (0, +1) to (-1, +1).
	bumpMap = (bumpMap * 2.0f) - 1.0f;
	//Calculate the normal from the data in the bump map
	bumpNormal = (bumpMap.x * input.tangent) + (bumpMap.y * input.binormal) + (bumpMap.z * input.normal);
	//normalize the resulting bump normal
	bumpNormal = normalize(bumpNormal);

	lightDir = -lightDirection;
	//calculate amount of the light based on normal value
	lightIntensity = saturate(dot(bumpNormal, lightDir));

	//HERE IT STARTS - for PARALLAX MAPPING
	float2 computedTextureCoords;
	float2 heightCoord;
	float heightMap = shaderTextures[2].Sample(SampleType, input.tex).r;

	// this was at C++ mainfile g_scaleBias[2] = {0.04f, -0.03f};
	//magic number is equal to scaleBias.x - scaleBias.y
	heightMap = heightMap * 0.04f; //this can be wrong

	computedTextureCoords = input.tex + (heightMap *  mul(halfVector, tbnMatrix).xy);

	//computedTextureCoords = input.tex;

	// Determine the final diffuse color based on the diffuse color and the amount of light intensity.
	color = saturate(diffuseColor * lightIntensity);
	//sample texture pixel
	textureColor = shaderTextures[0].Sample(SampleType, computedTextureCoords);

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