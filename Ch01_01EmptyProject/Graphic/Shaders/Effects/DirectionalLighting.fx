//same shader as diffuse, just in different way
Texture2D shaderTextures[3];
SamplerState SampleType;

cbuffer LightBuffer
{
	float4 diffuseColor;
	float3 lightDirection;

	float4 ambient;
	float4 diffuse;
	float4 specular;
};

cbuffer CameraBuffer
{
	float3 cameraPosition;
	float padding;
};

cbuffer MatrixBuffer
{
	float4x4 worldViewProj;
	float4x4 worldMatrix;
	float4x4 worldInverseTranspose;
};

//used by PS
cbuffer Material
{
	float4 m_ambient;
	float4 m_diffuse;
	float4 m_specular;
	float3 m_directional;
	float  m_shininess;
};

struct VertexInputType
{
	float4 position : POSITION;
	float2 tex : TEXCOORD;
	float3 normal : NORMAL;
	float3 tangent : TANGENT;
};

struct PixelInputType
{
	float4 position : SV_POSITION;
	float2 tex : TEXCOORD0;
	//float3 normal : NORMAL;
	float3 halfVector : TEXCOORD1;
	float3 lightDirection : TEXCOORD2;
	//float4 diffuseColor : COLOR0; //?
	//float4 specular : COLOR1;
};

PixelInputType LightVertexShader(VertexInputType input)
{
	PixelInputType output;
	input.position.w = 1.0f;

	float3 worldPosition = mul((float4)input.position, worldMatrix).xyz;
		float3 lightDir = -lightDirection;
		float3 viewDirection = cameraPosition - worldPosition;
		float3 halfVector = normalize(normalize(lightDir) + normalize(viewDirection));

		float3 normal = mul(input.normal, (float3x3)worldInverseTranspose);
		float3 tangent = mul(input.tangent, (float3x3)worldInverseTranspose);
		float3 binormal = cross(normal, tangent) * 0.0f;
		//mul(input.binormal, (float3x3)worldMatrix);

		//here we make out tbn matrix used for calc. of texel (halfVector)
		float3x3 tbnMatrix = float3x3(tangent.x, binormal.x, normal.x,
		tangent.y, binormal.y, normal.y,
		tangent.z, binormal.z, normal.z
		);
	
	output.lightDirection = mul(lightDir, tbnMatrix);

	//output.diffuseColor = material.diffuse; //* light.diffuse;
	//output.specular = material.specular;// *light.specular;


	output.position = mul(worldViewProj, input.position);//worldViewProj;
	// Store the texture coordinates for the pixel shader to use.
	output.tex = input.tex;

	output.halfVector = mul(tbnMatrix, halfVector);

	return output;
}


float4 LightPixelShader(PixelInputType input) : SV_TARGET
{
	//float3 height = normalize(input.halfVector);

	//float3 lightDir = normalize(input.lightDirection);
	//float3 normal = normalize(shaderTextures[1].Sample(SampleType, input.tex).rgb * 2.0f - 1.0f);
	////float3 normal = input.normal;

	//float nDotL = saturate(dot(normal, lightDir));
	//float nDotH = saturate(dot(normal, height));
	//float power = (nDotL == 0.0f) ? 0.0f : pow(nDotH, material.shininess);

	//float4 color = float4(1, 0, 1, 0);//(material.ambient) + (input.diffuse * nDotL) + (input.specular * power);// * (globalAmbient) // + light.ambient


		// Calculate the amount of the light on this pixel.
		//float lightIntensity = saturate(dot(normal, lightDirection));
	// Determine the final amount of diffuse color based on the diffuse color combined with the light intensity.
	//color = saturate(input.diffuseColor * lightIntensity);



	float4 textureCoord = shaderTextures[0].Sample(SampleType, input.tex);
		//color = color * textureCoord;
	
	//color = shaderTexture.Sample(SampleType, input.tex);
	
	return textureCoord;
}

technique11 Diffuse
{
	pass Pass1
	{
		SetVertexShader(CompileShader(vs_5_0, LightVertexShader()));
		SetPixelShader(CompileShader(ps_5_0, LightPixelShader()));
	}
}

//	if (bParallax == true)
//	{
//	float height = tex2D(heightMap, IN.texCoord).r;

//	height = height * scaleBias.x + scaleBias.y;
//	texCoord = IN.texCoord + (height * h.xy);
//	}
//	else
//	{
//		texCoord = IN.texCoord;
//	}
