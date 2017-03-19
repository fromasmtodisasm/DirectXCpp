#pragma once
#include <d3d11.h>
#include <d3dcompiler.h>
#include <directxmath.h>

using namespace DirectX;

class LightClass
{
public:
	LightClass();
	LightClass(LightClass& lightclass);
	~LightClass();

	
	void SetDiffuseColor(float red, float green, float blue, float alpha);
	void SetDirection(float x, float y, float z);

	XMFLOAT4 GetDiffuseColor();
	XMFLOAT3 GetDirection();

protected:
	
private:
	XMFLOAT4 diffuseColor;
	XMFLOAT3 direction;
};


