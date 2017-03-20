#pragma once
#include <d3d11.h>
#include <directxmath.h>
#include "textureclass.h"
#include <fstream>

class ModelClass
{
public:
	ModelClass();
	ModelClass(const ModelClass&);
	~ModelClass();



	ID3D11ShaderResourceView* GetTexture();
	bool Initialize(ID3D11Device *device, ID3D11DeviceContext *deviceContext, char *modelFilename, char *textureFilename);
	void Shutdown();
	void Render(ID3D11DeviceContext* deviceContext);

	int GetIndexCount();

protected:

private:
	struct VertexType
	{
		DirectX::XMFLOAT3 position;
		DirectX::XMFLOAT2 texture;
		DirectX::XMFLOAT3 normal;
	};

	struct ModelType
	{
		float x, y, z;
		float tu, tv;
		float nx, ny, nz;
	};

	bool InitializeBuffers(ID3D11Device*);
	void ShutdownBuffers();
	void RenderBuffers(ID3D11DeviceContext*);
	
	bool LoadTexture(ID3D11Device * device, ID3D11DeviceContext *deviceContext, char *textureFilename);
	void ReleaseTexture();
	bool LoadModel(char *filename);
	void ReleaseModel();

	ID3D11Buffer *vertexBuffer, *indexBuffer;
	int vertexCount, indexCount;

	TextureClass *texture;
	ModelType *model;

};