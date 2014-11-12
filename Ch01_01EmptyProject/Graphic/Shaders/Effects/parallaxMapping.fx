Texture2D shaderTextures[3];
SamplerState SampleType;

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
	float3 camera : TEXCOORD1;
};

//Transforming verticves and doing color calculatios
VertexShaderOutput ParallaxMapVertexShader(VertexInputType input)
{
	VertexShaderOutput output;
	input.position.w = 1.0f;
	
	float3 P = mul(worldViewProj, input.position).xyz;
	float3 cameraDirection = P - cameraPosition;
	
	float3x3 tangentToWorldSpace;

	//explicit truncation of vector type - need a cast in most times
	//in original code there isnt a cast, and its working ? wtf
	// for some reason lot of example code is totally messed up, when its got implemented
	
	tangentToWorldSpace[0] = mul(normalize(input.tangent), (float3x3)worldInverseTranspose);
	tangentToWorldSpace[1] = mul(normalize(input.binormal), (float3x3)worldInverseTranspose);
	tangentToWorldSpace[2] = mul(normalize(input.normal), (float3x3)worldInverseTranspose);

	float3x3 worldToTangentSpace = transpose(tangentToWorldSpace);

	output.position = mul(worldViewProj, input.position);
	output.tex = input.tex;
	output.camera = mul(cameraDirection, worldToTangentSpace);
	
	return output;
}

float4 ParallaxMapPixelShader(VertexShaderOutput input) : SV_TARGET
{
	bool useParallaxMapping = true;

	float2 computedTextureCoords;
	float3 h = normalize(input.camera);

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
	float4 vFinalColor = shaderTextures[0].Sample(SampleType, computedTextureCoords);

	return vFinalColor;
}

technique11 BumpMap
{
	pass Pass1
	{
		SetVertexShader(CompileShader(vs_5_0, ParallaxMapVertexShader()));
		SetPixelShader(CompileShader(ps_5_0, ParallaxMapPixelShader()));
	}
}