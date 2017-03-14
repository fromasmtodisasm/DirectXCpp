#pragma once
#include <d3d11.h>
#include <d3dcompiler.h>
#include <directxmath.h>

using namespace DirectX;

class TextureShaderClass
{
public:
	TextureShaderClass();
	TextureShaderClass(const TextureShaderClass&);
	~TextureShaderClass();


	bool Initialize(ID3D11Device *device, HWND hwnd);
	void Shutdown();
	bool Render(ID3D11DeviceContext *deviceContext, int indexCount,  XMMATRIX& worldMatrix,  XMMATRIX& viewMatrix,  XMMATRIX& projectionMatrix, ID3D11ShaderResourceView *texture);

private:
	bool InitializeShader(ID3D11Device* device, HWND hwnd, WCHAR* vsFilename, WCHAR* psFilename);
	void ShutdownShader();
	void OutputShaderErrorMessage(ID3D10Blob *errorMessage, HWND hwnd, WCHAR *shaderFilename);
	bool SetShaderParameters(ID3D11DeviceContext *deviceContext,  XMMATRIX& worldMatrix,  XMMATRIX& viewMatrix,  XMMATRIX& projectionMatrix, ID3D11ShaderResourceView* texture);
	//bool SetShaderParameters(ID3D11DeviceContext *deviceContext, const XMMATRIX& worldMatrix, const XMMATRIX& viewMatrix, const XMMATRIX& projectionMatrix);
	void RenderShader(ID3D11DeviceContext *deviceContext, int indexCount);

	struct MatrixBufferType 
	{
		 XMMATRIX world;
		 XMMATRIX view;
		 XMMATRIX projection;
	};

	ID3D11VertexShader* vertexShader;
	ID3D11PixelShader* pixelShader;
	ID3D11InputLayout* layout;
	ID3D11Buffer* matrixBuffer;

	ID3D11SamplerState *sampleState;
};