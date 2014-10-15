//rewriten from hlsl 3.0 - http://msdn.microsoft.com/en-us/library/windows/desktop/bb509647%28v=vs.85%29.aspx

cbuffer MatrixBuffer
{
	float4x4 World;
	float4x4 View;
	float4x4 Projection;
};

float4x4 WorldInverseTranspose;

float3 DiffuseLightDirection = float3(1, 0, 0);
float4 DiffuseColor = float4(1, 1, 1, 1);
float DiffuseIntensity = 1.0;

//white color !?
float4 AmbientColor = float4(1, 1, 1, 1);
float AmbientIntensity = 0.1;

//AppplicationToVertex
struct VertexShaderInput
{
	float4 Position : POSITION;
	float4 Normal : NORMAL;
};

//VertexToPixel
struct VertexShaderOutput
{
	float4 Position : POSITION;
	float4 Color : COLOR;
};

//Transforming verticves and doing color calculatios

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
	VertexShaderOutput output;

	//doing the same thinng as before just inside the shader itself
	float4 worldPosition = mul(input.Position, World);
		float4 viewPosition = mul(worldPosition, View);
		output.Position = mul(viewPosition, Projection);

	float4 normal = mul(input.Normal, WorldInverseTranspose);

		//angle between surface normal vector and the light, which we use to compute intensity of light
		// if direct , use 100% of intensity, if at the side, object is not enlighted at all
		float4 lightIntensity = dot(normal, DiffuseLightDirection);
		output.Color = saturate(DiffuseColor * DiffuseIntensity * lightIntensity);

	return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : SV_TARGET
{
	return saturate(input.Color + AmbientColor * AmbientIntensity);
}

technique11 Diffuse
{
	pass Pass1
	{
		SetVertexShader(CompileShader(vs_5_0, VertexShaderFunction()));
		SetPixelShader(CompileShader(ps_5_0, PixelShaderFunction()));
	}
}