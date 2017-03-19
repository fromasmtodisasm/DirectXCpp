#pragma once
#include "d3dclass.h"
#include "cameraclass.h"
#include "modelclass.h"
//#include "colorshaderclass.h"
#include "textureshaderclass.h"
#include "lightshaderclass.h"
#include "lightclass.h"

const bool FULL_SCREEN = false;
const bool VSYNC_ENABLED = true;
const float SCREEN_DEPTH = 1000.0f;
const float SCREEN_NEAR = 0.1f;

class GraphicsClass
{
public:
	GraphicsClass();
	GraphicsClass(const GraphicsClass&);
	~GraphicsClass();

	bool Initialize(int, int, HWND);
	void Shutdown();
	bool Frame();

private:
	bool Render(float rotation);
	D3DClass *direct3D;
	CameraClass *camera;
	ModelClass *model;
	//ColorShaderClass *colorShader;
	TextureShaderClass* textureShader;

	LightShaderClass* lightShader;
	LightClass* light;
};