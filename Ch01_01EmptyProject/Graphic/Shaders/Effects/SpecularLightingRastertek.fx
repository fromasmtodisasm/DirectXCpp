Texture2D shaderTexture;
SamplerState SampleType;

cbuffer LightBuffer
{
		float4 ambientColor;
		float4 diffuseColor;
		float3 lightDirection;
		float specularPower;
		float4 specularColor;
};

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

struct VertexInputType
{
	float4 position : POSITION;
	float2 tex : TEXCOORD;
	float3 normal : NORMAL;
};
//PixelInputType
struct VertexShaderOutput
{
	float4 position : SV_POSITION;
	float2 tex : TEXCOORD1;
	float3 normal : NORMAL;
	float3 viewDirection : TEXCOORD;
};

//Transforming verticves and doing color calculatios

VertexShaderOutput SpecularVertexShaderFunction(VertexInputType input)
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
	// Store the texture coordinates for the pixel shader.
	output.normal = mul(input.normal, (float3x3)worldMatrix);
	output.normal = normalize(output.normal);

	worldPosition = mul(input.position, worldMatrix);
	//Determine the viewing direction
	output.viewDirection = cameraPosition.xyz - worldPosition.xyz;

	output.viewDirection = normalize(output.viewDirection);
	
	return output;
}

float4 SpecularPixelShaderFunction(VertexShaderOutput input) : SV_TARGET
{
	float4 textureColor;
	float3 lightDir;
	float lightIntensity;
	float4 color;
	float3 reflection;
	float4 specular;

	textureColor = shaderTexture.Sample(SampleType, input.tex);

	color = ambientColor;

	specular = float4(0.0f, 0.0f, 0.0f, 0.0f);
	// Invert the light direction for calculations.
	lightDir = -lightDirection;

	// Calculate the amount of the light on this pixel.
	lightIntensity = saturate(dot(input.normal, lightDir));

	// Determine the final amount of diffuse color based on the diffuse color combined with the light intensity.
	color += saturate(diffuseColor * lightIntensity);

	reflection = normalize(2 * lightIntensity * input.normal - lightDir);

	specular = pow(saturate(dot(reflection, input.viewDirection)), specularPower);

	color = color * textureColor;
	
	color = saturate(color + specular);
	
	return color;
}

technique11 Specular
{
	pass Pass1
	{
		SetVertexShader(CompileShader(vs_5_0, SpecularVertexShaderFunction()));
		SetPixelShader(CompileShader(ps_5_0, SpecularPixelShaderFunction()));
	}
}