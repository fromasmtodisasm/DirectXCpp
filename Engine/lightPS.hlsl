Texture2D shaderTexture;
SamplerState SampleType;

cbuffer LightBuffer
{
	float4 ambientColor;
    float4 diffuseColor;
    float3 lightDirection;
    float padding;
};


struct PixelInputType
{
    float4 position : SV_POSITION;
    float2 tex : TEXCOORD0;
    float3 normal : NORMAL;
};

float4 LightPixelShader(PixelInputType input) : SV_TARGET
{
    float4 textureColor;
    float3 lightDir;
    float lightIntensity;
    float4 color;

    // Sample the pixel color from the texture using the sampler at this texture coordinate location.
    textureColor = shaderTexture.Sample(SampleType, input.tex);
	//default output color = ambient light value for all pixels
	color = ambientColor;

    //invert light direction    
    lightDir = -lightDirection;

    //calculate amount of light per pixel
    lightIntensity = saturate(dot(input.normal, lightDir));

	if(lightIntensity > 0.0f)
	{
		//Determine final diffuse color based on the diffuse color and the amount of the light intensity
		color += (diffuseColor * lightIntensity);
	}

	//satureate the final light color
    color = saturate(color);
	
    // Multiply the texture pixel and the final diffuse color to get the final pixel color result.
    color = color * textureColor;

    return color;
}
