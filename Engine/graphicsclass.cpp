#include "graphicsclass.h"
#include <stdlib.h>

GraphicsClass::GraphicsClass()
{
	direct3D = 0;
	camera = 0;
	model = 0;
	colorShader = 0;
}


GraphicsClass::GraphicsClass(const GraphicsClass& other)
{
}


GraphicsClass::~GraphicsClass()
{
}


bool GraphicsClass::Initialize(int screenWidth, int screenHeight, HWND hwnd)
{
	bool result;


	// Create the Direct3D object.
	direct3D = new D3DClass();
	if (!direct3D)
	{
		return false;
	}

	// Initialize the Direct3D object.
	result = direct3D->Initialize(screenWidth, screenHeight, VSYNC_ENABLED, hwnd, FULL_SCREEN, SCREEN_DEPTH, SCREEN_NEAR);
	if (!result)
	{
		MessageBox(hwnd, L"Could not initialize Direct3D.", L"Error", MB_OK);
		return false;
	}

	camera = new CameraClass();
	if (!camera)
	{
		return false;
	}
	camera->SetPosition(0.0f, 0.0f, -5.0f);
	model = new ModelClass();
	if (!model)
	{
		return false;
	}
	result = model->Initialize(direct3D->GetDevice());
	if (!result)
	{
		MessageBox(hwnd, L"Mdel failed to initialize", L"Error", MB_OK);
		return false;
	}
	colorShader = new ColorShaderClass();
	if (!colorShader)
	{
		return false;
	}

	result = colorShader->Initialize(direct3D->GetDevice(), hwnd);
	if (!result)
	{
		MessageBox(hwnd, L"Could not initiailize color shader", L"Error", MB_OK);
		return false;
	}

	return true;
}


void GraphicsClass::Shutdown()
{
	if (colorShader)
	{
		colorShader->Shutdown();
		delete colorShader;
		colorShader = 0;
	}

	if (model)
	{
		model->Shutdown();
		delete model;
		model = 0;
	}


	if (direct3D)
	{
		direct3D->Shutdown();
		delete direct3D;
		direct3D = 0;
	}
}


bool GraphicsClass::Frame()
{
	bool result;
	// Render the graphics scene.
	result = Render();
	if (!result)
	{
		return false;
	}

	return true;
}


bool GraphicsClass::Render()
{
	XMMATRIX worldMatrix, viewMatrix, projectionMatrix;
	bool result;
	//clear buffers
	direct3D->BeginScene(1.0f, 1.0f, 1.0f,1.0f);

	camera->Render();

	direct3D->GetWorldMatrix(worldMatrix);
	camera->GetViewMatrix(viewMatrix);
	direct3D->GetProjectionMatrix(projectionMatrix);

	model->Render(direct3D->GetDeviceContext());
	result = colorShader->Render(direct3D->GetDeviceContext(), model->GetIndexCount(), worldMatrix, viewMatrix, projectionMatrix);
	if (!result)
	{
		return false;
	}

	// Present the rendered scene to the screen.
	direct3D->EndScene();

	return true;
}