#pragma once
#include <d3d11.h>
#include <directxmath.h>

class ModelClass
{
public:
	ModelClass();
	ModelClass(const ModelClass&);
	~ModelClass();

	bool Initialize(ID3D11Device*);
	void Shutdown();
	void Render(ID3D11DeviceContext*);

	int GetIndexCount();

protected:

private:
	struct VertexType
	{
		DirectX::XMFLOAT3 position;
		DirectX::XMFLOAT4 color;
	};

	bool InitializeBuffers(ID3D11Device*);
	void ShuutdownBuffers();
	void RenderBuffers(ID3D11DeviceContext*);
	
	ID3D11Buffer *vertexBuffer, *indexBuffer;
	int vertexCount, indexCount;
};