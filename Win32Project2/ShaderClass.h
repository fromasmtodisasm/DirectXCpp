#pragma once
#include <d3d11.h>
#include <d3dx10math.h>
#include <d3dx11async.h>
#include <fstream>

class ShaderClass
{
private:
	ID3D11VertexShader *vertexShader;
	ID3D11PixelShader *pixelShader;
	ID3D11InputLayout* layout;
	ID3D11Buffer* vertexBuffer;

	struct VERTEX
	{
		FLOAT X, Y, Z;
		D3DXCOLOR Color;
	};

	void SetShader(ID3D11DeviceContext*);

public:
	ShaderClass();
	~ShaderClass();

	void Init(ID3D11Device*, HWND);
	void RenderShader(ID3D11DeviceContext*);
	void Clean(void);
};

