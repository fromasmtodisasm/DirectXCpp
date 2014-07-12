#include <d3d11.h>

#pragma once
class Model2D
{
private:

	ID3D11Buffer *vertexBuffer;
	
	void CreateVertexBuffer(ID3D11Device*);
	
	//void CopyVerticesToBuffer(D3D11_BUFFER_DESC, VERTEX[]);

public:
	Model2D();
	~Model2D();
	
	void RenderBuffersFrame(ID3D11DeviceContext*);
	void Init(ID3D11Device*);
	void Clean(void);
};

