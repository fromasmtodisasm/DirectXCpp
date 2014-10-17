
//Texture2D shaderTexture;
//SamplerState SampleType;

cbuffer LightBuffer
{
	float4 diffuseColor = float4(1, 1, 1, 1);;
	float3 lightDirection = float3(0, 1, 0);
	float padding = 1.0f;
};

cbuffer MatrixBuffer
{
	matrix worldMatrix;
	matrix viewMatrix;
	matrix projectionMatrix;
};

//////////////////////
////   TYPES
//////////////////////
struct VertexInputType
{
	float4 position : POSITION;
	float4 tex : COLOR;
	float3 normal : NORMAL;
};

struct PixelInputType
{
	float4 position : SV_POSITION;
	float4 tex : COLOR;
	float3 normal : NORMAL;
};

/////////////////////////////////////
/////   Vertex Shader
/////////////////////////////////////
PixelInputType LightVertexShader(VertexInputType input)
{
	PixelInputType output;

	// Change the position vector to be 4 units for proper matrix calculations.
	input.position.w = 1.0f;

	// Calculate the position of the vertex against the world, view, and projection matrices.
	output.position = mul(input.position, worldMatrix);
	output.position = mul(output.position, viewMatrix);
	output.position = mul(output.position, projectionMatrix);

	// Store the texture coordinates for the pixel shader.
	output.tex = input.tex;

	// Calculate the normal vector against the world matrix only.
	output.normal = mul(input.normal, (float3x3)worldMatrix);

	// Normalize the normal vector.
	output.normal = normalize(output.normal);

	return output;
}

//////////////////////
////   Pixel Shader
/////////////////////
float4 LightPixelShader(PixelInputType input) : SV_TARGET
{
	//float4 textureColor;
	float3 lightDir;
	float lightIntensity;
	float4 color;

	//textureColor = input.tex;

	// Invert the light direction for calculations.
	lightDir = -lightDirection;

	// Calculate the amount of the light on this pixel.
	lightIntensity = saturate(dot(input.normal, lightDir));

	// Determine the final amount of diffuse color based on the diffuse color combined with the light intensity.
	color = saturate(diffuseColor * lightIntensity);

	color = color * input.tex;;
	return color;
}

technique11 Diffuse
{
	pass Pass1
	{
		SetVertexShader(CompileShader(vs_5_0, LightVertexShader()));
		SetPixelShader(CompileShader(ps_5_0, LightPixelShader()));
	}
}

