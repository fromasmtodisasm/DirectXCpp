#include "D3DClass.h"
#include "Model2D.h"
#include "ShaderClass.h"


#pragma once
class GraphicsClass
{
private:
	D3DClass* d3dClass;
	Model2D* modelClass;
	ShaderClass* shaderClass;

	void Clean(void);

public:
	GraphicsClass();
	~GraphicsClass();
	
	void RenderFrame();
	void Init(HWND); //this is  Temp for initing d3d

};

