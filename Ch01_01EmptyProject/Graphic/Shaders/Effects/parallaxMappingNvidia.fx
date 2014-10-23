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

//if combined with other effects
//cbuffer LightBuffer
//{
//	float4 diffuseColor;
//	float3 lightDirection;
//};


struct VertexInputType
{
	float4 position : POSITION;
	float2 tex : TEXCOORD0;
	float3 normal : NORMAL;
	float3 tangent : TANGENT;
	float3 binormal : BINORMAL;
};

struct VertexShaderOutput
{
	float4 position : SV_POSITION;
	float2 tex : TEXCOORD0;
	float3 normal : NORMAL;
	float3 tangent : TANGENT;
	float3 binormal : BINORMAL;
	float3 viewDirection : TEXCOORD;
};

//Transforming verticves and doing color calculatios

VertexShaderOutput ParallaxMapVertexShader(VertexInputType input)
{
	VertexShaderOutput output;
	float4 worldPosition;
	// Change the position vector to be 4 units for proper matrix calculations.
	input.position.w = 1.0f;

	// Calculate the position of the vertex against the world, view, and projection matrices.
	output.position = mul(input.position, worldMatrix);
	output.position = mul(output.position, viewMatrix);
	output.position = mul(output.position, projectionMatrix);

	output.tex = input.tex;

	output.normal = mul(input.normal, (float3x3)worldMatrix);
	output.normal = normalize(output.normal);

	output.tangent = mul(input.tangent, (float3x3)worldMatrix);
	output.tangent = normalize(output.tangent);

	output.binormal = mul(input.binormal, (float3x3)worldMatrix);
	output.binormal = normalize(output.binormal);
	
	worldPosition = mul(input.position, worldMatrix);
	//Determine the viewing direction
	output.viewDirection = cameraPosition.xyz - worldPosition.xyz;

	output.viewDirection = normalize(output.viewDirection);
	
	return output;
}

float4 ParallaxMapPixelShader(VertexShaderOutput input) : SV_TARGET
{
	float4 textureColor;
	float4 bumpMap;
	float4 height;

	float3 bumpNormal;
	float3 lightDir;
	float lightIntensity;
	float4 color;
	
	//sample texture pixel
	textureColor = shaderTextures[0].Sample(SampleType, input.tex);
	//sample bump map
	bumpMap = shaderTextures[1].Sample(SampleType, input.tex);
	height = shaderTextures[2].Sample(SampleType, input.tex).r;
	//float height = tex2D(heightMap, IN.texCoord).r;

	//height = height * scaleBias.x + scaleBias.y;
	//textureColor = input.tex + (height * h.xy);

	return (height *  0.035 - 0.17) * input. + input.tex;

	//// Expand the range of the normal value from (0, +1) to (-1, +1).
	//bumpMap = (bumpMap * 2.0f) - 1.0f;

	////Calculate the normal from the data in the bump map
	//bumpNormal = (bumpMap.x * input.tangent) + (bumpMap.y * input.binormal) + (bumpMap.z * input.normal);
	////normalize the resulting bump normal
	//bumpNormal = normalize(bumpNormal);

	//lightDir = -lightDirection;
	////calculate amount of the light based on normal value
	//lightIntensity = saturate(dot(bumpNormal, lightDir));

	//// Determine the final diffuse color based on the diffuse color and the amount of light intensity.
	//color = saturate(diffuseColor * lightIntensity);

	

	/*color = color * textureColor;
	return color;*/
}

technique11 ParallaxMapTest
{
	pass Pass1
	{
		SetVertexShader(CompileShader(vs_5_0, ParallaxMapVertexShader()));
		SetPixelShader(CompileShader(ps_5_0, ParallaxMapPixelShader()));
	}
}