#include "GraphicsClass.h"


GraphicsClass::GraphicsClass()
{
}


GraphicsClass::~GraphicsClass()
{
}

// set up and initialize Direct3D
void GraphicsClass::Init(HWND hWnd)
{
	d3dClass = new D3DClass;
	d3dClass->InitD3D(hWnd); //maybe should be Init only?
	//? is if not try to do init in contructor

	modelClass = new Model2D;
	modelClass->Init(d3dClass->GetDevice());

	shaderClass = new ShaderClass;
	shaderClass->Init(d3dClass->GetDevice(), hWnd);
}

void GraphicsClass::RenderFrame()
{
	d3dClass->StartRender();
	modelClass->RenderBuffersFrame(d3dClass->GetDeviceContext());
	shaderClass->RenderShader(d3dClass->GetDeviceContext());

	d3dClass->RenderComplete();
}

void GraphicsClass::Clean()
{
	//should be composite, than clean on all nodes
	d3dClass->CleanD3D();
	modelClass->Clean();
	shaderClass->Clean();
}

