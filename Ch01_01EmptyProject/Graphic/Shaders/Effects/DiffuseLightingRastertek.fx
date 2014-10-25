Texture2D shaderTextures[3];
SamplerState SampleType;

cbuffer LightBuffer
{
	float4 ambientColor;
	float4 diffuseColor;
	float3 lightDirection;
	float specularPower;
	float4 specularColor;
};

cbuffer CameraBuffer
{
	float3 cameraPosition;
	float padding;
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
	float2 tex : TEXCOORD0;
	float3 normal : NORMAL;
	float3 tangent : TANGENT;
	//float3 binormal : BINORMAL;
};

struct VertexShaderOutput
{
	float4 position : SV_POSITION;
	float2 tex : TEXCOORD0;
	float3 halfVector: TEXCOORD1;
	float3 lightDir : TEXCOORD2;
	float3 normal : NORMAL;
	float3 binormal : BINORMAL;
	float3 tangent : TANGENT;
	/*float3 diffuse : NORMAL;
	float3 specular : TEXCOORD;*/
};






//Transforming verticves and doing color calculatios

VertexShaderOutput DirLightingVertexShaderTwo(VertexInputType input)
{
	VertexShaderOutput output;
	float4 worldPosition;
	output.normal = mul(input.normal, (float3x3)worldMatrix);
	output.normal = normalize(output.normal);

	float3 binormal;
	float3 lightDir;

	float3 fd;

	// Change the position vector to be 4 units for proper matrix calculations.
	input.position.w = 1.0f;

	// Calculate the position of the vertex against the world, view, and projection matrices.
	output.position = mul(input.position, worldMatrix);
	output.position = mul(output.position, viewMatrix);
	output.position = mul(output.position, projectionMatrix);

	output.tex = input.tex;
	
	float3 f = float3(1, 1, 1);
	//float3 rd = (float3)lightDirection;
	output.lightDir = f;// cross(f, lightDirection);
	
	float3 worldPos = mul(input.position, worldMatrix).xyz;
	
	float3 viewDir = cameraPosition - worldPos;
	float3 halfVector = normalize(normalize(lightDirection) + normalize(viewDir));
	

	output.tangent = mul(input.tangent, (float3x3)worldMatrix);
	output.tangent = normalize(output.tangent);

	binormal = cross(output.normal, output.tangent) * 0.0f;  //input.tangent.w

	float3x3 tbnMatrix = float3x3(output.tangent.x, binormal.x, output.normal.x,
		output.tangent.y, binormal.y, output.normal.y,
		output.tangent.z, binormal.z, output.normal.z
		);
	output.halfVector = mul(halfVector, tbnMatrix);
	//lightDir = -lightDirection;
	// fd = mul(lightDir, tbnMatrix);
	// output.lightDir = fd;//lightDir  
	// 
			//float3 halfVector = normalize(normalize(-lightDirection) + normalize(viewDir));


	//output.binormal = normalize(binormal);
	output.binormal = binormal;
	return output;
	 
	//		

	//		float3 worldPos = mul(input.position, worldMatrix).xyz;
	//		float3 lightDir = -lightDirection;
	//


}

float4 DirLightingPixelShaderTwo(VertexShaderOutput input) : SV_TARGET
{
	
		//float3 h = normalize(input.halfVector);
		////if parallax mapping ... else
	
		//float3 l = normalize(input.lightDir);
		////in example code is bump map referenced as a normal map
		//float4 bumpMap = shaderTextures[1].Sample(SampleType, input.tex);
		//// Expand the range of the normal value from (0, +1) to (-1, +1).
		//bumpMap = (bumpMap * 2.0f) - 1.0f;
		//float3 n = normalize(bumpMap.xyz);
	
		//float nDotL = saturate(dot(n, l));
		//float nDotH = saturate(dot(n, h));
		////material
		//float power = (nDotL == 0.0f) ? 0.0f : pow(nDotH, 90.0f);
	
		////as far im able to get it, nDotL and nDotH are used for lighting effect
		//float4 color = (diffuseColor * nDotL) + (specularColor * power);
	
		////textureColor = s
		//color = color * shaderTextures[0].Sample(SampleType, input.tex);
		//return color;
	
	
	
	
	
	
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
	
	lightDir = -input.lightDir;
	//calculate amount of the light based on normal value
	lightIntensity = saturate(dot(bumpNormal, lightDir));

	// Determine the final diffuse color based on the diffuse color and the amount of light intensity.
	color = saturate(diffuseColor * lightIntensity);

	color = color * textureColor;
	return color;
}

technique11 DiffuseLightingParallax
{
	pass Pass1
	{
		SetVertexShader(CompileShader(vs_5_0, DirLightingVertexShaderTwo()));
		SetPixelShader(CompileShader(ps_5_0, DirLightingPixelShaderTwo()));
	}
}


//Texture2D shaderTextures[3];
//SamplerState SampleType;
//
//cbuffer LightBuffer
//{
//	float4 ambientColor;
//	float4 diffuseColor;
//	float3 lightDirection;
//	float specularPower;
//	float4 specularColor;
//};
//
//cbuffer CameraBuffer
//{
//	float3 cameraPosition;
//	float padding;
//};
//
//cbuffer MatrixBuffer
//{
//	matrix worldMatrix;
//	matrix viewMatrix;
//	matrix projectionMatrix;
//};
////////////////////////
//////   TYPES
////////////////////////
//struct VertexInputType
//{
//	float4 position : POSITION;
//	float2 tex : TEXCOORD0;
//	float3 normal : NORMAL;
//	float4 tangent : TANGENT;
//	float3 binormal : BINORMAL;
//};
//
//struct PixelInputType
//{
//	float4 position : SV_POSITION;
//	float2 tex : TEXCOORD0;
//	float3 halfVector: TEXCOORD1;
//	float3 lightDir : TEXCOORD2;
//	float3 normal : NORMAL;
//	float3 b : BINORMAL;
//	float3 t : TANGENT;
//	/*float3 diffuse : NORMAL;
//	float3 specular : TEXCOORD;*/
//};
//
//PixelInputType DirLightingVertexShaderTwo(VertexInputType input)
//{
//	PixelInputType output;
//		output.normal = mul(input.normal, (float3x3)worldMatrix);
//		output.normal = normalize(output.normal);
//		// Calculate the position of the vertex against the world, view, and projection matrices.
//		output.position = mul(input.position, worldMatrix);
//		output.position = mul(output.position, viewMatrix);
//		output.position = mul(output.position, projectionMatrix);
//		
//		// Change the position vector to be 4 units for proper matrix calculations.
//		//input.position.w = 1.0f;
//		
//		float3x3 worldInverseTranspose = (float3x3)transpose(worldMatrix);
//		float3 worldPos = mul(input.position, worldMatrix).xyz;
//		float3 lightDir = -lightDirection;
//
//		float3 viewDir = cameraPosition - worldPos;
//		float3 halfVector = normalize(normalize(lightDir) + normalize(viewDir));
//
//		float3 n = mul(input.normal, (float3x3)worldInverseTranspose);
//		float3 t = mul(input.tangent.xyz, (float3x3)worldInverseTranspose);
//		float3 b = cross(n, t) * input.tangent.w;
//		float3x3 tbnMatrix = float3x3(t.x, b.x, n.x,
//								      t.y, b.y, n.y,
//									  t.z, b.z, n.z
//									 );
//    // Store the texture coordinates for the pixel shader.
//	output.tex = input.tex;
//	
//	output.halfVector = mul(halfVector, tbnMatrix);
//	output.lightDir = mul(lightDir, tbnMatrix);
//	output.b = b;
//
//	return output;
//}
//
//float4 DirLightingPixelShaderTwo(PixelInputType input) : SV_TARGET
//{
//	//float3 h = normalize(input.halfVector);
//	////if parallax mapping ... else
//
//	//float3 l = normalize(input.lightDir);
//	////in example code is bump map referenced as a normal map
//	//float4 bumpMap = shaderTextures[1].Sample(SampleType, input.tex);
//	//// Expand the range of the normal value from (0, +1) to (-1, +1).
//	//bumpMap = (bumpMap * 2.0f) - 1.0f;
//	//float3 n = normalize(bumpMap.xyz);
//
//	//float nDotL = saturate(dot(n, l));
//	//float nDotH = saturate(dot(n, h));
//	////material
//	//float power = (nDotL == 0.0f) ? 0.0f : pow(nDotH, 90.0f);
//
//	////as far im able to get it, nDotL and nDotH are used for lighting effect
//	//float4 color = (diffuseColor * nDotL) + (specularColor * power);
//
//	////textureColor = s
//	//color = color * shaderTextures[0].Sample(SampleType, input.tex);
//	//return color;
//}
//
//technique11 DiffuseLightingParallax
//{
//	pass Pass1
//	{
//		SetVertexShader(CompileShader(vs_5_0, DirLightingVertexShaderTwo()));
//		SetPixelShader(CompileShader(ps_5_0, DirLightingPixelShaderTwo()));
//	}
//}
//
