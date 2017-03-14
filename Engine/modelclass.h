#pragma once
#include <d3d11.h>
#include <directxmath.h>
#include "textureclass.h"

class ModelClass
{
public:
	ModelClass();
	ModelClass(const ModelClass&);
	~ModelClass();

	ID3D11ShaderResourceView* GetTexture();
	bool Initialize(ID3D11Device *device, ID3D11DeviceContext *deviceContext, char *textureFilename);
	void Shutdown();
	void Render(ID3D11DeviceContext*);

	int GetIndexCount();

protected:

private:
	struct VertexType
	{
		DirectX::XMFLOAT3 position;
		DirectX::XMFLOAT2 texture;
	};

	bool InitializeBuffers(ID3D11Device*);
	void ShutdownBuffers();
	void RenderBuffers(ID3D11DeviceContext*);
	
	bool LoadTexture(ID3D11Device * device, ID3D11DeviceContext * deviceContext, char *textureFilename);
	void ReleaseTexture();

	ID3D11Buffer *vertexBuffer, *indexBuffer;
	int vertexCount, indexCount;

	TextureClass *texture;


};