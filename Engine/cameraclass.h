#pragma once

#include <directxmath.h>
#include <d3d11.h>
#include <d3dcompiler.h>

using namespace DirectX;

class CameraClass
{
public:
	CameraClass();
	CameraClass(const CameraClass&);

	void SetPosition(float x, float y, float z);
	void SetRotation(float x, float y, float z);
	
	XMFLOAT3 GetPosition();
	XMFLOAT3 GetRotation();
	
	void Render();
	void GetViewMatrix(XMMATRIX& viewMatrix);

	~CameraClass();

private:
	float positionX, positionY, positionZ;
	float rotationX, rotationY, rotationZ;
	XMMATRIX viewMatrix;
};


