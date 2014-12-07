cbuffer MatrixBuffer
{
	float4x4 worldViewProj;
	float4x4 worldMatrix;
	float4x4 worldInverseTranspose;
};

//AppplicationToVertex
struct VertexShaderInput
{
	float4 Position : POSITION;
};

//VertexToPixel
struct VertexShaderOutput
{
	float4 Position : POSITION;
	float4 depthPosition : TEXTURE0;
};

//Transforming verticves and doing color calculatios

VertexShaderOutput VertexDepthShaderFunction(VertexShaderInput input)
{
	VertexShaderOutput output;
	// Change the position vector to be 4 units for proper matrix calculations.
	input.position.w = 1.0f;
	output.position = mul(worldViewProj, (float4)input.position);

	output.depthPosition = output.position;
	
	return output;
}

float4 PixelDepthShaderFunction(VertexShaderOutput input) : SV_TARGET
{
	float depthValue = input.depthPosition.z / input.depthPosition.w;
	
	if (depthValue < 0.9f)
	{
		color = float4(1.0, 0.0f, 0.0f 1.0f);
	}

	if (depthValue > 0.9f)
	{
		color = float4(0.0, 1.0, 0.0f, 1.0f);
	}

	if (depthValue > 0.925f)
	{
		color = float4(0.0, 0.0f, 1.0f, 1.0f);
	}

	return color;
}	

technique11 Ambient
{
	pass Pass1
	{
		SetVertexShader(CompileShader(vs_5_0, VertexDepthShaderFunction()));
		SetPixelShader(CompileShader(ps_5_0, PixelDepthShaderFunction()));
	}
}