cbuffer MatrixBuffer
{
	float4x4 worldViewProj;
	float4x4 worldMatrix;
	float4x4 worldInverseTranspose;
};

Texture2D shaderTexture;
SamplerState SampleType;

struct VertexInputType
{
	float4 position : POSITION;
	float2 tex : TEXCOORD;
};

struct PixelInputType
{
	float4 position : SV_POSITION;
	float2 tex : TEXCOORD;
};

PixelInputType TextureVertexShader(VertexInputType input)
{
	PixelInputType output;
	// Change the position vector to be 4 units for proper matrix calculations.!!!IMPORTANT
	//but maybe i should use vector3 for input
	input.position.w = 1.0f;
	output.position = mul(worldViewProj, input.position);//worldViewProj;
	// Store the texture coordinates for the pixel shader to use.
	output.tex = input.tex;

	return output;
}

float4 TexturePixelShader(PixelInputType input) : SV_TARGET
{
	float4 textureColor;
	// Sample the pixel color from the texture using the sampler at this texture coordinate location.
	textureColor = shaderTexture.Sample(SampleType, input.tex);

	return textureColor;
}

technique11 Textured
{
	pass Pass1
	{
		SetVertexShader(CompileShader(vs_5_0, TextureVertexShader()));
		SetPixelShader(CompileShader(ps_5_0, TexturePixelShader()));
	}
}