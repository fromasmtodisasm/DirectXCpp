//rewriten from hlsl 3.0 - http://msdn.microsoft.com/en-us/library/windows/desktop/bb509647%28v=vs.85%29.aspx
Texture2D shaderTextures[2];
SamplerState SampleType;

cbuffer ConstantBuffer
{
	matrix World;
	matrix View;
	matrix Projection;
};

cbuffer LightBuffer
{
	float4 ambientColor;
	float4 diffuseColor;
	float3 lightDirection;
	float specularPower;
	float4 specularColor;
};


cbuffer LightBuffer
{
	//white color !?
	float3 AmbientDown;
	float3 AmbientRange;
	float3 DirToLight;
	float3 DirLightColor;
};

cbuffer CameraBuffer
{
	float3 cameraPosition;
	float padding;
};

//AppplicationToVertex
struct VertexShaderInput
{
	float4 Position : POSITION;
	float3 Normal : NORMAL;
	float UV : TEXCOORD0;
};

struct VertexShaderOutput
{
	float4 Pos : SV_POSITION;
	float2 Uv : TEXCOORD0;
	float Norm : TEXCOORD1;
	float3 WorldPos : TEXCOORD2;
};

float CalcAmbient(float3 normal, float3 color)
{
	float up = normal.y * 0.5 + 0.5;

	float3 ambient = AmbientDown + up * AmbientRange;

		return ambient * color;
}

//float CalcDirectional(float3 position, Material material)
//{
//	//phong diffuse
//	NDotL = dot(DirToLight, material.normal);
//	float3 finalColor = DirLightColor.rgb * saturate(NDotL);
//
//	float toEye = cameraPosition.xyz - position;
//	ToEye.normalize(toEye);
//	float3 HalWay = normalize(ToEye + DirToLight);
//	float NDotH = saturate(dot(HalfWay, material.normal));
//	finalColor += DirLightColor.rgb * pow(NDotH, material.specExp) * material.specIntensity;
//
//	return finalColor * material.diffuseColor.rgb;
//}

//Transforming verticves and doing color calculatios
VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
	VertexShaderOutput output;

	//doing the same thinng as before just inside the shader itself
	float4 worldPosition = mul(input.Position, World);
		float4 viewPosition = mul(worldPosition, View);
		output.Position = mul(viewPosition, Projection);

	return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : SV_TARGET
{
	float4 textureColor;
	float2 texCoord;
	float3 h = normalize(input.HalfVector);

	texCoord = input.Coord;
	
	//diffuseColor
	textureColor = shaderTexture.Sample(SampleType, input.tex);
	textureColor.rgb *= textureColor.rgb;

	float3 finalColor = CalcAmbient(material.normal, material.duffuseColor.rgb);

	finalColor += CalcDirectional(In.WorldPos, material);

	NDotL = dot(DirToLight, material.normal);
	float3 finalColor = DirLightColor.rgb * saturate(NDotL);

	float toEye = cameraPosition.xyz - position;
	ToEye.normalize(toEye);
	float3 HalfWay = normalize(ToEye + DirToLight);
	float NDotH = saturate(dot(HalfWay, material.normal));
	finalColor += DirLightColor.rgb * pow(NDotH, material.specExp) * material.specIntensity;

	return finalColor * material.diffuseColor.rgb;
	
	return float4(finalColor, 1.0);
}

technique11 Lighting
{
	pass Pass1
	{
		SetVertexShader(CompileShader(vs_5_0, VertexShaderFunction()));
		SetPixelShader(CompileShader(ps_5_0, PixelShaderFunction()));
	}
}