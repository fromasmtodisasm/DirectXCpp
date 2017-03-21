#include "graphicsclass.h"
#include <stdlib.h>

GraphicsClass::GraphicsClass()
{
	direct3D = 0;
	camera = 0;
	model = 0;
	textureShader = 0;
	lightShader = 0;
	light = 0;
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
	camera->SetPosition(0.0f, 0.0f, -10.0f);
	model = new ModelClass();
	if (!model)
	{
		return false;
	}
	result = model->Initialize(direct3D->GetDevice() ,direct3D->GetDeviceContext(), "../Engine/data/cube.txt" , "../Engine/data/stone01.tga");
	if (!result)
	{
		MessageBox(hwnd, L"Model failed to initialize", L"Error", MB_OK);
		return false;
	}

	textureShader = new TextureShaderClass();
	if (!textureShader)
	{
		return false;
	}

	result = textureShader->Initialize(direct3D->GetDevice(), hwnd);
	if (!result)
	{
		MessageBox(hwnd, L"Could not initialize the model object.", L"Error", MB_OK);
		return false;
	}

	lightShader = new LightShaderClass();
	if (!lightShader)
	{
		return false;
	}
	result = lightShader->Initialize(direct3D->GetDevice(), hwnd);

	if (!result)
	{
		MessageBox(hwnd, L"Could not Initialize the light shader object",L"ERROR", MB_OK);
		return false;
	}

	light = new LightClass();
	if (!light)
	{
		return false;
	}

	light->SetAmbientColor(0.15f, 0.15f, 0.15f, 1.0f);
	light->SetDiffuseColor(1.0f, 1.0f, 1.0f, 1.0f);//light->SetDiffuseColor(1.0f, 0.0f, 1.0f, 1.0f);
	light->SetDirection(1.0f, 0.0f, 0.0f);

	return true;
}


void GraphicsClass::Shutdown()
{
	if (light)
	{
		delete light;
		light = 0;
	}

	if (lightShader)
	{
		lightShader->Shutdown();
		delete lightShader;
		lightShader = 0;
	}

	if (textureShader)
	{
		textureShader->Shutdown();
		delete textureShader;
		textureShader = 0;
	}

	if (model)
	{
		model->Shutdown();
		delete model;
		model = 0;
	}

	if (camera)
	{
		delete camera;
		camera = 0;
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
	static float rotation = 0.0f;

	rotation += (float)XM_PI * 0.005f;
	if (rotation > 360.0f)
	{
		rotation -= 360.0f;
	}

	// Render the graphics scene.
	result = Render(rotation);
	if (!result)
	{
		return false;
	}

	return true;
}


bool GraphicsClass::Render(float rotation)
{
	XMMATRIX worldMatrix, viewMatrix, projectionMatrix;
	bool result;
	//clear buffers
	direct3D->BeginScene(0.0f, 0.0f, 0.0f, 1.0f);

	camera->Render();

	direct3D->GetWorldMatrix(worldMatrix);
	camera->GetViewMatrix(viewMatrix);
	direct3D->GetProjectionMatrix(projectionMatrix);

	//Multiply the world matrix by the rotation.
	worldMatrix = XMMatrixRotationY(rotation);

	model->Render(direct3D->GetDeviceContext());

	result = lightShader->Render(direct3D->GetDeviceContext(), model->GetIndexCount(), worldMatrix, viewMatrix, projectionMatrix, model->GetTexture(), light->GetDirection(),light->GetAmbientColor(), light->GetDiffuseColor());

	if (!result)
	{
		return false;
	}

	// Present the rendered scene to the screen.
	direct3D->EndScene();

	return true;
}