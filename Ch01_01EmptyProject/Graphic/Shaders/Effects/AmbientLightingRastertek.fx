//rewriten from hlsl 3.0 - http://msdn.microsoft.com/en-us/library/windows/desktop/bb509647%28v=vs.85%29.aspx

cbuffer ConstantBuffer : register(b0)
{
	matrix World;
	matrix View;
	matrix Projection;
}


//white color !?
float4 AmbientColor = float4(1, 1, 1, 1);
float AmbientIntensity = 0.1;

//AppplicationToVertex
struct VertexShaderInput
{
	float4 Position : POSITION;
	float2 tex : TEXCOORD;
	float3 normal : NORMAL;
};

//VertexToPixel
struct VertexShaderOutput //PixelInputType
{
	float4 Position : SV_POSITION;
	float2 tex : TEXCOORD;
	float3 normal : NORMAL;;
};

//Transforming verticves and doing color calculatios

VertexShaderOutput LightVertexShader(VertexShaderInput input)
{
	VertexShaderOutput output;

	input.position.w = 1.0f;

	//doing the same thinng as before just inside the shader itself
	output.Position = mul(input.Position, World);
	output.Position = mul(worldPosition, View);
	output.Position = mul(viewPosition, Projection);

	output.tex = input.tex;

	output.normal = mul(input.normal, (float3x3)worldMatrix);

	output.normal = normalize(output.normal);
	return output;



float4 PixelShaderFunction(VertexShaderOutput input) : SV_TARGET
{
	float4 textureColor;
	float3 lightDir;
	float lightIntensity;

	float4 color;
	
	textureColor = shaderTexture.Sample(SampleType, input.tex);

	color = ambientColor;

	lightDir = -lightDirection;

	lightIntensity = saturate(dot(input.normal, lightDir));

	if (lightIntensity > 0.0f)
	{
		color += (difuseColor * lightIntensity);
	}

	color = saturate(color);
	
	return color * textureColor;;


}

technique11 Ambient
{
	pass Pass1
	{
		SetVertexShader(CompileShader(vs_5_0, VertexShaderFunction()));
		SetPixelShader(CompileShader(ps_5_0, PixelShaderFunction()));
	}
}