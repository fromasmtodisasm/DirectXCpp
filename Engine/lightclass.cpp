#include "LightClass.h"

LightClass::LightClass()
{

}

LightClass::~LightClass()
{

}

void LightClass::SetAmbientColor(float red, float green, float blue, float alpha)
{
	ambientColor = XMFLOAT4(red, green, blue, alpha);
}

void LightClass::SetDiffuseColor(float red, float green, float blue, float alpha)
{
	diffuseColor = XMFLOAT4(red, green, blue, alpha);
}

void LightClass::SetDirection(float x, float y, float z)
{
	direction = XMFLOAT3(x, y, z);
}

DirectX::XMFLOAT4 LightClass::GetAmbientColor()
{
	return ambientColor;
}

DirectX::XMFLOAT4 LightClass::GetDiffuseColor()
{
	return diffuseColor;
}

DirectX::XMFLOAT3 LightClass::GetDirection()
{
	return	direction;
}
