#include "graphicsclass.h"

GraphicsClass::GraphicsClass(const GraphicsClass& other)
{
	d3d = 0;
}

GraphicsClass::~GraphicsClass()
{
}

bool GraphicsClass::Initialize(int screenWidth, int screenHeight, HWND hwnd)
{
	bool result;
	
	d3d = new D3DClass;
	if (!d3d)
	{
		return false;
	}

	result = d3d->Initialize(screenWidth, screenHeight, VSYNC_ENABLED, hwnd, FULL_SCREEN, SCREEN_DEPTH, SCREEN_NEAR);
	if (result)
	{
		MessageBox(hwnd, L"Could not start Direct3D", L"Error", MB_OK);
		return false;
	}
	return true;
}

void GraphicsClass::Shutdown()
{
	if (d3d)
	{
		d3d->Shutdown();
		delete d3d;
		d3d = 0;
	}
	
	return;
}

bool GraphicsClass::Frame()
{
	bool result;
	
	result = Render();
	if (result)
	{
		return false;
	}
	return true;
}

bool GraphicsClass::Render()
{
	d3d->BeginScene(0.5f, 0.5f, 0.5f, 1.0f);//clear scene with gray
	d3d->EndScene();

	return true;
}

