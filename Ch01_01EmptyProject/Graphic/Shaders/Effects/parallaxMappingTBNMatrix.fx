Texture2D shaderTextures[3];
SamplerState SampleType;
//
//cbuffer cPerObject
//{
//
//};
//
//cbuffer cPerFrame
//{
//
//
//};


cbuffer MatrixBuffer
{
	float4x4 worldViewProj;
	float4x4 worldMatrix;
	float4x4 worldInverseTranspose;
};

cbuffer CameraBuffer
{
	float3 cameraPosition;
	float padding;
};

cbuffer LightBuffer
{
	float4 diffuseColor;
	float3 lightDirection;
};

struct VertexInputType
{
	float4 position : POSITION;
	float2 tex : TEXCOORD0;
	float3 normal : NORMAL;
	float3 tangent : TANGENT;
	float3 binormal : BINORMAL;
};
//PixelInputType
struct VertexShaderOutput
{
	float4 position : SV_POSITION;
	float2 tex : TEXCOORD0;

	float3 normal : NORMAL;
	float3 tangent : TANGENT;
	float3 binormal : BINORMAL;

	float3 halfVector : TEXCOORD1;
	float3 lightDirection : TEXCOORD2;
	//float3 worldPosition : TEXCOORD3;

	float4 diffuseColor : TEXCOORD3;
};

//Transforming verticves and doing color calculatios
VertexShaderOutput ParallaxMapVertexShader(VertexInputType input)
{
	VertexShaderOutput output;
	input.position.w = 1.0f;
	
	float3 worldPosition = mul(input.position, worldMatrix).xyz;
	float3 lightDir = lightDirection;
	float3 viewDirection = cameraPosition - worldPosition;
	float3 halfVector = normalize(normalize(lightDir) + normalize(viewDirection));

	float3 normal = mul(input.normal, (float3x3)worldMatrix);
	float3 tangent = mul(input.tangent, (float3x3)worldMatrix);
	float3 binormal = mul(input.binormal, (float3x3)worldMatrix);
	
	//here we make out tbn matrix used for calc. of texel (halfVector)
	float3x3 tbnMatrix = float3x3(tangent.x, binormal.x, normal.x,
								  tangent.y, binormal.y, normal.y,
						    	  tangent.z, binormal.z, normal.z
								 );

	output.normal = normal;
	output.tangent = tangent;
	output.binormal = binormal;

	output.position = mul(worldViewProj, (float4)input.position);
	output.tex = input.tex;
	output.halfVector = mul(halfVector, tbnMatrix);
	output.lightDirection = mul(lightDir, tbnMatrix);// lightDir; //mul(lightDir, worldViewProj); //
	
	output.diffuseColor = diffuseColor;

	return output;
}

float4 ParallaxMapPixelShader(VertexShaderOutput input) : SV_TARGET
{
	bool useParallaxMapping = true;
	float2 computedTextureCoords;
	float3 h = normalize(input.halfVector);

	if (useParallaxMapping == true)
	{
		float scale = 0.09f;
		float bias = -0.03f;

		float height = shaderTextures[1].Sample(SampleType, input.tex).a;
		height = height * scale + bias; 
		computedTextureCoords = input.tex + (height *  h.xy);
	}
	else
	{
		computedTextureCoords = input.tex;
	}
	//sample texture pixel
	float4 textureColor = shaderTextures[0].Sample(SampleType, computedTextureCoords);
	return textureColor;
}

technique11 BumpMap
{
	pass Pass1
	{
		SetVertexShader(CompileShader(vs_5_0, ParallaxMapVertexShader()));
		SetPixelShader(CompileShader(ps_5_0, ParallaxMapPixelShader()));
	}
}