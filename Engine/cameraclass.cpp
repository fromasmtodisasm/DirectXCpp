#include "cameraclass.h"

CameraClass::CameraClass()
{
	positionX = 0.0f;
	positionY = 0.0f;
	positionZ = 0.0f;

	rotationZ = 0.0f;
	rotationY = 0.0f;
	rotationZ = 0.0f;
}

CameraClass::~CameraClass()
{
}

void CameraClass::SetPosition(float x, float y, float z)
{
	positionX = x;
	positionY = y;
	positionZ = z;
}
void CameraClass::SetRotation(float x, float y, float z)
{
	rotationX = x;
	rotationY = y;
	rotationZ = z;
}

XMFLOAT3 CameraClass::GetPosition()
{
	return XMFLOAT3(positionX, positionY, positionZ);
}
void CameraClass::Render()
{
	XMFLOAT3 up, lookAt, position;
	XMVECTOR upVector, positionVector, lookAtVector;
	float yaw, pitch, roll;
	XMMATRIX rotationMatrix;

	//Setup the vectors
	up.x = 0.0f;
	up.y = 1.0f;
	up.z = 0.0f;

	//Load into structures
	upVector = XMLoadFloat3(&up);
	
	//setup the the position of camera
	position.x = positionX;
	position.y = positionY;
	position.y = positionZ;

	positionVector = XMLoadFloat3(&position);

	//setup where the camera is looking by default
	lookAt.x = 0.0f;
	lookAt.y = 0.0f;
	lookAt.z = 1.0f;

	lookAtVector = XMLoadFloat3(&lookAt);

	pitch = rotationX * 0.017453295f;
	yaw = rotationY * 0.017453295f;
	roll = rotationZ * 0.017453292f;

	//Create rotation matrix
	rotationMatrix = XMMatrixRotationRollPitchYaw(pitch, yaw, roll);
	//Trasform the lookAt and up vector by the rotation matrix so the view is correctly rotated at the origin
	lookAtVector = XMVector3TransformCoord(lookAtVector, rotationMatrix);
	upVector = XMVector3TransformCoord(upVector, rotationMatrix);
	lookAtVector = XMVectorAdd(positionVector, lookAtVector);

	upVector = XMVector3TransformCoord(upVector, rotationMatrix);

	lookAtVector = XMVectorAdd(positionVector, lookAtVector);
	
	//Finally create the view matrix from the three updated vectors
	viewMatrix = XMMatrixLookAtLH(positionVector, lookAtVector, upVector);
}

void CameraClass::GetViewMatrix(XMMATRIX& viewMatrix)
{
	this->viewMatrix = viewMatrix;
}