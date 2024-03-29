﻿///////////////////////
////   GLOBALS
///////////////////////
cbuffer WorldViewProj
{
	float4x4 worldViewProj;
}
//////////////////////
////   TYPES
//////////////////////

struct VertexInputType
{
	//float4 position : POSITION;
	float3 position : POSITION;
	float4 color : COLOR;
};

struct PixelInputType
{
	float4 position : SV_POSITION;
	float4 color : COLOR;
};

/////////////////////////////////////
/////   Vertex Shader
/////////////////////////////////////
PixelInputType ColorVertexShader(VertexInputType input)
{
	PixelInputType output;

	// Change the position vector to be 4 units for proper matrix calculations.
	//input.position.w = 1.0f;

	// Calculate the position of the vertex against the world, view, and projection matrices.
	//output.position = mul(input.position, worldMatrix);
	//output.position = mul(output.position, viewMatrix);
	//output.position = mul(output.position, projectionMatrix);

	output.position = mul(float4(input.position, 1.0f), worldViewProj);

	// Store the input color for the pixel shader to use.
	output.color = input.color; //mul(input.color, float4(0.2, 0.2, 0.1, 0.3));
	
	return output;
}

float4 ColorPixelShader(PixelInputType input) : SV_TARGET
{
	return input.color;
}

technique11 ColorTech
{
	pass P0
	{
		SetVertexShader(CompileShader(vs_5_0, ColorVertexShader()));
		SetPixelShader(CompileShader(ps_5_0, ColorPixelShader()));
		//SetRasterizerState(WireFrameRS);
	}
}