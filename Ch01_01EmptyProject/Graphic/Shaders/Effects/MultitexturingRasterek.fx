Texture2D shaderTextures[2];
SamplerState SampleType;

cbuffer MatrixBuffer
{
	matrix worldMatrix;
	matrix viewMatrix;
	matrix projectionMatrix;
};

struct VertexInputType
{
	float4 position : POSITION;
	float2 tex : TEXCOORD0;
};
//PixelInputType
struct VertexShaderOutput
{
	float4 position : SV_POSITION;
	float2 tex : TEXCOORD0;
};

//Transforming verticves and doing color calculatios

VertexShaderOutput MultiTextureVertexShader(VertexInputType input)
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
	
	return output;
}

float4  MultiTexturePixelShader(VertexShaderOutput input) : SV_TARGET
{
	float4 textureColor;
	float4 textureColor2;
	float4 blendColor;
	
	textureColor = shaderTextures[0].Sample(SampleType, input.tex);
	textureColor2 = shaderTextures[1].Sample(SampleType, input.tex);

	//blend together and multiply by gamma
	blendColor = textureColor * textureColor2 * 1.5;
	blendColor = saturate(blendColor);

	return blendColor;
}

technique11 MultiTexture
{
	pass Pass1
	{
		SetVertexShader(CompileShader(vs_5_0, MultiTextureVertexShader()));
		SetPixelShader(CompileShader(ps_5_0, MultiTexturePixelShader()));
	}
}