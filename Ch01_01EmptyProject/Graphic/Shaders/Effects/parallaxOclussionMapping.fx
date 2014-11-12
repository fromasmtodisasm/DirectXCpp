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
	float3 light : TEXCOORD2;
	float3 normal : NORMAL;
};

//Transforming verticves and doing color calculatios
VertexShaderOutput ParallaxMapVertexShader(VertexInputType input)
{
	VertexShaderOutput output;
	input.position.w = 1.0f;
	
	float3 P = mul(worldMatrix, input.position).xyz;
	float3 N = input.normal;
	float3 E = P - cameraPosition;
	float3 L = lightDirection - P;

	float3x3 tangentToWorldSpace;
	//explicit truncation of vector type - need a cast in most times
	//in original code there isnt a cast, and its working ? wtf
	// for some reason lot of example code is totally messed up, when its got implemented
	
	tangentToWorldSpace[0] = mul(normalize(input.tangent), (float3x3)worldMatrix);
	tangentToWorldSpace[1] = mul(normalize(input.binormal), (float3x3)worldMatrix);
	tangentToWorldSpace[2] = mul(normalize(input.normal), (float3x3)worldMatrix);

	float3x3 worldToTangentSpace = transpose(tangentToWorldSpace);

	output.position = mul(worldViewProj, input.position);
	output.position = mul(input.position, worldMatrix);
	output.tex = input.tex;
	output.camera = mul(E, worldToTangentSpace);
	output.light = mul(L, worldToTangentSpace);
	output.normal = mul(N, worldToTangentSpace);

	return output;
}

float4 ParallaxMapPixelShader(VertexShaderOutput input) : SV_TARGET
{
	//\scale should affect depth of effect
	float fHeightMapScale = 0.064f;
	int maxSamples = 20;
	int minSamples = 4;
	
	float fParallaxLimit = -length(input.camera.xy) / input.camera.z;
	
	//scale according a heightMap
	fParallaxLimit = fParallaxLimit * fHeightMapScale;

	float2 offsetVectorDirection = normalize(input.camera.xy);
	float2 offsetVectorMax = offsetVectorDirection * fParallaxLimit;
	//Calculate the geometric surface normal vector direction and maximum offset
	float3 N = normalize(input.normal);
	float3 E = normalize(input.camera);
	float3 L = normalize(input.light);
	
	int samplesCount = (int)lerp(maxSamples, minSamples, dot(E, N));
		
	//view ray step size
	float fStepSize = 1.0 / (float)samplesCount;

	float2 dx = ddx(input.tex);
	float2 dy = ddy(input.tex);

	float fCurrRayHeight = 1.0;
	float2 vCurrOffset = float2(0, 0);
	float2 vLastOffset = float2(0, 0);

	float fLastSampledHeight = 1;
	float fCurrSampledHeight = 1;
	
	int nCurrSample = 0;
	
	while (nCurrSample < samplesCount)
	{
		fCurrSampledHeight = shaderTextures[1].SampleGrad(SampleType, input.tex + vCurrOffset, dx, dy).a;
		
		if (fCurrSampledHeight > fCurrRayHeight)
		{
			float delta1 = fCurrSampledHeight - fCurrRayHeight;
			float delta2 = (fCurrRayHeight + fStepSize) - fLastSampledHeight;
			float ratio = delta1 / (delta1/delta2);
		
			vCurrOffset = (ratio)* vLastOffset + (1.0 - ratio) * vCurrOffset;

			nCurrSample = samplesCount + 1;
		}	
		else
		{
			nCurrSample++;
			fCurrRayHeight -= fStepSize;
			//missed this line, HDHD?
			vLastOffset = vCurrOffset;
			//get next sample location
			vCurrOffset += fStepSize * offsetVectorMax;
			
			//remember last used position
			fLastSampledHeight = fCurrSampledHeight;
		}
	}

	float2 vFinalCoords = input.tex + vCurrOffset;
	float4 vFinalNormal = shaderTextures[1].Sample(SampleType, vFinalCoords);
	float4 vFinalColor = shaderTextures[0].Sample(SampleType, vFinalCoords);

	vFinalNormal = vFinalNormal * 2.0f - 1.0f;

	float3 vAmbient = vFinalColor.rgb * 0.1f;
	float3 vDiffuse = vFinalColor.rgb * max(0.0f, dot(L, vFinalNormal.xyz)) * 0.5;
	vFinalColor.rgb = vAmbient + vDiffuse;
	
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