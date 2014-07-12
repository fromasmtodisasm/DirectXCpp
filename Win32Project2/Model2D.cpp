#include "Model2D.h"
#include "D3DClass.h" //temp shold be n graphiclass


struct VERTEX
{
	FLOAT X, Y, Z;
	D3DXCOLOR Color;
};


Model2D::Model2D()
{
	vertexBuffer = 0;
}

Model2D::~Model2D()
{
}

void Model2D::Init(ID3D11Device* device)
{
	//its not possible to modify whats drawn from the otuside now
	//this method is relly short now , but it cares ONLY about 
	//creating the data, as for me, when theres an urge for commenting
	//a block of code = > comment as method, for da sake of separating logical blocks

	VERTEX OurVertices[] =
	{
		{ 0.0f, 0.5f, 0.0f, D3DXCOLOR(1.0f, 0.0f, 0.0f, 1.0f) },
		{ 0.45f, -0.5, 0.0f, D3DXCOLOR(0.0f, 1.0f, 0.0f, 1.0f) },
		{ -0.45f, -0.5f, 0.0f, D3DXCOLOR(0.0f, 0.0f, 1.0f, 1.0f) }
	};
	//its needed to create buffer for every piece of data?
   
	CreateVertexBuffer(device);
	
	//model is responsible for its buffer, but maybe
	//D3Dclass should be responsible for copying it
}

void Model2D::CreateVertexBuffer(ID3D11Device* device)
{
	D3D11_BUFFER_DESC bufferDesc;
	ZeroMemory(&bufferDesc, sizeof(bufferDesc));

	bufferDesc.Usage = D3D11_USAGE_DYNAMIC; //write only acess 
	bufferDesc.ByteWidth = sizeof(VERTEX)* 3; //use as a vertex buffer
	bufferDesc.BindFlags = D3D11_BIND_VERTEX_BUFFER;
	bufferDesc.CPUAccessFlags = D3D11_CPU_ACCESS_WRITE;

	device->CreateBuffer(&bufferDesc, NULL, &vertexBuffer); //create buffer without any inialization data
}

void Model2D::RenderBuffersFrame(ID3D11DeviceContext* deviceContext)
{
	UINT stride = sizeof(VERTEX); // vertex buffer to display
	UINT offset = 0;
	deviceContext->IASetVertexBuffers(0, 1, &vertexBuffer, &stride, &offset);
	deviceContext->IASetPrimitiveTopology(D3D10_PRIMITIVE_TOPOLOGY_TRIANGLELIST);
	deviceContext->Draw(3, 0);
}

void Model2D::Clean()
{
	//vertexBuffer->Release();
}