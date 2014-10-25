//rewriten from hlsl 3.0 - http://msdn.microsoft.com/en-us/library/windows/desktop/bb509647%28v=vs.85%29.aspx
Texture2D shaderTexture;
SamplerState SampleType;

cbuffer MatrixBuffer
{
	matrix worldMatrix;
	matrix viewMatrix;
	matrix projectionMatrix;
};

cbuffer LightBuffer
{
	float4 ambientColor;
	float4 diffuseColor;
	float3 lightDirection;
	float padding;
};

//AppplicationToVertex
struct VertexShaderInput
{
	float4 position : POSITION;
	float2 tex : TEXCOORD;
	float3 normal : NORMAL;
};

//VertexToPixel
struct VertexShaderOutput //PixelInputType
{
	float4 position : SV_POSITION;
	float2 tex : TEXCOORD;
	float3 normal : NORMAL;
};

//Transforming verticves and doing color calculatios

VertexShaderOutput LightVertexShaderFunction(VertexShaderInput input)
{
	VertexShaderOutput output;

	input.position.w = 1.0f;

	//doing the same thinng as before just inside the shader itself
	output.position = mul(input.position, worldMatrix);
	output.position = mul(output.position, viewMatrix);
	output.position = mul(output.position, projectionMatrix);

	output.tex = input.tex;

	output.normal = mul(input.normal, (float3x3)worldMatrix);

	output.normal = normalize(output.normal);
	return output;
}

float4 LightPixelShaderFunction(VertexShaderOutput input) : SV_TARGET
{
	float4 textureColor;
	float3 lightDir;
	float lightIntensity;

	float4 color;
	
	//sample texture
	textureColor = shaderTextures.Sample(SampleType, input.tex);

	color = ambientColor;

	lightDir = -lightDirection;

	lightIntensity = saturate(dot(input.normal, lightDir));

	if (lightIntensity > 0.0f)
	{
		color += (diffuseColor * lightIntensity);
	}

	color = saturate(color);
	
	return color * textureColor;
}

technique11 Ambient
{
	pass Pass1
	{
		SetVertexShader(CompileShader(vs_5_0, LightVertexShaderFunction()));
		SetPixelShader(CompileShader(ps_5_0, LightPixelShaderFunction()));
	}
}