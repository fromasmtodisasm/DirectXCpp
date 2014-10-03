//not vary per vertex, but can update content of buffer at runtime
cbuffer CPerObject
{
	float4x4 gWorldViewProj;
}

struct VertexIn
{
	float3 Pos : POSITION;
	float4 Color : COLOR;
};

struct VertexOut
{
	float4 PosH : SV_POSITION;
	float4 Color : COLOR;
};

VertexOut VS(VertexIn vin)
{
	VertexOut vout;
	
	//transform to homogenous clip space
	vout.PosH = mul(float4(vin.Pos, 1.0f), gWorldViewProj);

	vout.Color = vin.Color;

	return vout;
}

float4 PS(VertexOut pin) : SV_TARGET
{
	return pin.Color; 
}

RasterizerState WireFrameRS
{
	FillMode = Wireframe;
	CullMode = Back;
	FrontCounterClockWise = false;
};

technique11 ColorTech
{
	pass P0
	{
		SetVertexShader(CompileShader(vs_5_0, VS() ));
		SetPixelShader(CompileShader(ps_5_0, PS() ));

		SetRasterizerState(WireFrameRS);
	}
}