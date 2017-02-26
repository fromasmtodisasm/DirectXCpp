#pragma once

#include <directxmath.h>
#include <d3d11.h>
#include <d3dcompiler.h>

using namespace DirectX;
using namespace std;

class CameraClass
{
public:
	CameraClass();
	void SetPosition(float x, float y, float z);
	void SetRotation(float x, float y, float z);
	XMFLOAT3 GetPosition();
	void Render();
	void GetViewMatrix(XMMATRIX& viewMatrix);

	~CameraClass();
protected:

private:
	float positionX, positionY, positionZ;
	float rotationX, rotationY, rotationZ;
	XMMATRIX viewMatrix;
};


