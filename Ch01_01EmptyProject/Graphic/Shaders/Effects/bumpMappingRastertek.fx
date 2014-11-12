Texture2D shaderTextures[2];
SamplerState SampleType;

cbuffer MatrixBuffer
{
	float4x4 worldViewProj;
	float4x4 worldMatrix;
	float4x4 worldInverseTranspose;
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
};

//Transforming verticves and doing color calculatios

VertexShaderOutput BumpMapVertexShader(VertexInputType input)
{
	VertexShaderOutput output;
	// Change the position vector to be 4 units for proper matrix calculations.
	input.position.w = 1.0f;
	float3 worldPosition = mul(input.position, worldMatrix).xyz;
	
	output.position = mul(worldViewProj, (float4)input.position);

	output.tex = input.tex;

	output.normal = mul(input.normal, (float3x3)worldMatrix);
	output.normal = normalize(output.normal);

	output.tangent = mul(input.tangent, (float3x3)worldMatrix);
	output.tangent = normalize(output.tangent);

	output.binormal = mul(input.binormal, (float3x3)worldMatrix);
	output.binormal = normalize(output.binormal);

	return output;
}

float4 BumpMapPixelShader(VertexShaderOutput input) : SV_TARGET
{
	float4 textureColor;
	float4 bumpMap;
	float3 bumpNormal;
	float3 lightDir;
	float lightIntensity;
	float4 color;

	//sample texture pixel
	textureColor = shaderTextures[0].Sample(SampleType, input.tex);
	//sample bump map
	bumpMap = shaderTextures[1].Sample(SampleType, input.tex);

	// Expand the range of the normal value from (0, +1) to (-1, +1).
	bumpMap = (bumpMap * 2.0f) - 1.0f;

	//Calculate the normal from the data in the bump map
	bumpNormal = (bumpMap.x * input.tangent) + (bumpMap.y * input.binormal) + (bumpMap.z * input.normal);
	//normalize the resulting bump normal
	bumpNormal = normalize(bumpNormal);

	lightDir = -lightDirection;
	//calculate amount of the light based on normal value
	lightIntensity = saturate(dot(bumpNormal, lightDir));

	// Determine the final diffuse color based on the diffuse color and the amount of light intensity.
	color = saturate(diffuseColor * lightIntensity);

	color = color * textureColor;
	return color;
}

technique11 BumpMap
{
	pass Pass1
	{
		SetVertexShader(CompileShader(vs_5_0, BumpMapVertexShader()));
		SetPixelShader(CompileShader(ps_5_0, BumpMapPixelShader()));
	}
}

//Texture2D shaderTextures[3];
//SamplerState SampleType;
////
////cbuffer cPerObject
////{
////
////};
////
////cbuffer cPerFrame
////{
////
////
////};
//
//
//cbuffer MatrixBuffer
//{
//	float4x4 worldViewProj;
//	float4x4 worldMatrix;
//	float4x4 worldInverseTranspose;
//};
//
//cbuffer CameraBuffer
//{
//	float3 cameraPosition;
//	float padding;
//};
//
//cbuffer LightBuffer
//{
//	float4 diffuseColor;
//	float3 lightDirection;
//};
//
//struct VertexInputType
//{
//	float4 position : POSITION;
//	float2 tex : TEXCOORD0;
//	float3 normal : NORMAL;
//	float3 tangent : TANGENT;
//	float3 binormal : BINORMAL;
//};
////PixelInputType
//struct VertexShaderOutput
//{
//	float4 position : SV_POSITION;
//	float2 tex : TEXCOORD0;
//
//	float3 normal : NORMAL;
//	float3 tangent : TANGENT;
//	float3 binormal : BINORMAL;
//
//	float3 halfVector : TEXCOORD1;
//	float3 lightDirection : TEXCOORD2;
//	//float3 worldPosition : TEXCOORD3;
//
//	float4 diffuseColor : TEXCOORD3;
//};
//
////Transforming verticves and doing color calculatios
//VertexShaderOutput BumpMapVertexShader(VertexInputType input)
//{
//	VertexShaderOutput output;
//	input.position.w = 1.0f;
//	
//	float3 worldPosition = mul(input.position, worldMatrix).xyz;
//	float3 lightDir = lightDirection;
//	float3 viewDirection = cameraPosition - worldPosition;
//	float3 halfVector = normalize(normalize(lightDir) + normalize(viewDirection));
//
//	float3 normal = mul(input.normal, (float3x3)worldInverseTranspose);
//	float3 tangent = mul(input.tangent, (float3x3)worldInverseTranspose);
//	float3 binormal = mul(input.binormal, (float3x3)worldInverseTranspose);
//	//float3 binormal = cross(normal, tangent) * 0.0f;
//	
//	//here we make out tbn matrix used for calc. of texel (halfVector)
//	float3x3 tbnMatrix = float3x3(tangent.x, binormal.x, normal.x,
//								  tangent.y, binormal.y, normal.y,
//						    	  tangent.z, binormal.z, normal.z
//								 );
//
//	output.normal = normal;
//	output.tangent = tangent;
//	output.binormal = binormal;
//
//	output.position = mul(worldViewProj, (float4)input.position);
//	output.tex = input.tex;
//	output.halfVector = mul(halfVector, tbnMatrix);
//	output.lightDirection = mul(lightDir, tbnMatrix);// lightDir; //mul(lightDir, worldViewProj); //
//	
//	output.diffuseColor = diffuseColor;
//
//	return output;
//}
//
//float4 BumpMapPixelShader(VertexShaderOutput input) : SV_TARGET
//{
//	//I shouldd probably get rid of bumMapping for now...
//	//its maybe colliding with that parallax shit
//
//	bool useParallaxMapping = true;
//	float2 computedTextureCoords;
//	float3 h = normalize(input.halfVector);
//
//	if (useParallaxMapping == true)
//	{
//		float scale = 0.065f;
//		float bias = -0.03f;
//
//		float height = shaderTextures[2].Sample(SampleType, input.tex).r;
//		height = height * scale + bias; 
//		computedTextureCoords = input.tex + (height *  h.xy);
//	}
//	else
//	{
//		computedTextureCoords = input.tex;
//	}
//
//	float3 normalizedLight = input.lightDirection; //normalize(input.lightDirection);
//
//	//sample bump map
//	float4 bumpMap = shaderTextures[1].Sample(SampleType, input.tex);
//	// Expand the range of the normal value from (0, +1) to (-1, +1).
//	bumpMap = (bumpMap * 2.0f) - 1.0f;
//	//Calculate the normal from the data in the bump map
//	float3 bumpNormal = (bumpMap.x * input.tangent) + (bumpMap.y * input.binormal) + (bumpMap.z * input.normal);
//	//normalize the resulting bump normal
//	bumpNormal = normalize(bumpNormal);
//	//calculate amount of the light based on normal value
//	float lightIntensity = saturate(dot(bumpNormal, normalizedLight));
//	//sample texture pixel
//	float4 textureColor = shaderTextures[0].Sample(SampleType, computedTextureCoords);
//	// Determine the final diffuse color based on the diffuse color and the amount of light intensity.
//	float4 color = saturate(input.diffuseColor * lightIntensity);
//	color = color * textureColor;
//	return color;
//}
//
//technique11 BumpMap
//{
//	pass Pass1
//	{
//		SetVertexShader(CompileShader(vs_5_0, BumpMapVertexShader()));
//		SetPixelShader(CompileShader(ps_5_0, BumpMapPixelShader()));
//	}
//}