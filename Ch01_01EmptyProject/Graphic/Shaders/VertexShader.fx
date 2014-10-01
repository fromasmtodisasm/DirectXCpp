﻿cbuffer CPerObject
{
	float4x4 WorldViewProj;
}

void VS(float3 iPosL : POSITION,
		float4 iColor : COLOR,
		out float4 oPosH : SV_POSITION,
		out float4 oColor : COLOR)
{
	//transform to homogenous clip space
	oPosH = mul(float4(iPosL, 1.0f), gWorldViewProj);

	oColor = iColor;
}