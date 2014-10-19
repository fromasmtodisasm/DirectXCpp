//rewriten from hlsl 3.0 - http://msdn.microsoft.com/en-us/library/windows/desktop/bb509647%28v=vs.85%29.aspx

cbuffer WorldViewProj
{
	float4x4 World;
	float4x4 View;
	float4x4 Projection;
};

texture ModelTexture;
sampler2D textureSampler = sampler_state
{
	Texture = (ModelTexture);
	MagFilter = Linear; //Magnify
	MinFilter = Linear; //Minify
	//define overflow - clamp is like clip ?!
	AddressU = Clamp;
	AddressV = Clamp;
};

float4x4 WorldInverseTranspose;

//for metalic surfaces it should be around 100-500
float Shininess = 200;
float4 SpecularColor = float4(1, 1, 1, 1);
float SpecularIntensity = 1;
float3 ViewVector = float3(1, 0, 0);

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
	float2 TextureCoordinate : TEXCOORD;
};

//VertexToPixel
struct VertexShaderOutput
{
	float4 Position : POSITION;
	float4 Color : COLOR;
	float3 Normal : TEXCOORD;
	float2 TextureCoordinate : TEXCOORD;
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

	output.Normal = normal;
	output.TextureCoordinate = input.TextureCoordinate;

	return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : SV_TARGET
{
	float3 light = normalize(DiffuseLightDirection);
	float3 normal = normalize(input.Normal);

	//Direction that light directly from the source, once it bounces off the point we are currently looking at?
	float3 reflectionVector = normalize(2 * dot(light, normal) * normal - light);
	float3 viewVector = normalize(mul(ViewVector, World));

	float dotProduct = dot(reflectionVector, viewVector);
	float4 specular = SpecularIntensity * SpecularColor * max(pow(dotProduct, Shininess), 0) * length(input.Color);

	float4 AmbientLighting = AmbientColor * AmbientIntensity;

	//ensure that color fit to valid range (0 - 1) 
	return saturate(textureColor * (input.Color) + AmbientLighting + specular);

	//calculate pixel color based on texture
	float4 textureColor = tex2D(textureSampler, input.TextureCoordinate);
	//replace alpha channel loaded up from texture
	textureColor.a = 1;
}

technique11 Textured
{
	pass Pass1
	{
		SetVertexShader(CompileShader(vs_5_0, TextureVertexShader()));
		SetPixelShader(CompileShader(ps_5_0, TexturePixelShader()));
	}
}