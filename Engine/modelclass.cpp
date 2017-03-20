#include "modelclass.h"
#include <comutil.h>
using namespace DirectX;

ModelClass::ModelClass()
{
	vertexBuffer = 0;
	indexBuffer = 0;
	texture = 0;
	model = 0;
}

ModelClass::ModelClass(const ModelClass &)
{
}

ModelClass::~ModelClass()
{
}

bool ModelClass::Initialize(ID3D11Device *device, ID3D11DeviceContext *deviceContext, char *modelFilename, char *textureFilename)
{
	bool result;

	result = LoadModel(modelFilename);
	if (!result)
	{
		return false;
	}

	result = InitializeBuffers(device);

	if (!result)
	{
		return false;
	}

	result = LoadTexture(device, deviceContext, textureFilename);
	if (!result)
	{
		return false;
	}
	return true;
}

void ModelClass::Shutdown()
{
	ReleaseTexture();
	ShutdownBuffers();
	ReleaseModel();
}

void ModelClass::Render(ID3D11DeviceContext *deviceContext)
{
	RenderBuffers(deviceContext);
}

int ModelClass::GetIndexCount()
{
	return indexCount;
}

bool ModelClass::InitializeBuffers(ID3D11Device *device)
{
	VertexType *vertices;
	unsigned long *indices;
	D3D11_BUFFER_DESC vertexBufferDesc, indexBufferDesc;
	D3D11_SUBRESOURCE_DATA vertexData, indexData;
	HRESULT result;
	int i;

	vertices = new VertexType[vertexCount];
	if (!vertices)
	{
		return false;
	}
	indices = new unsigned long[indexCount];
	if (!indices)
	{
		return false;
	}

	for (int i = 0; i < vertexCount; i++)
	{
		auto currVertex = model[i];
		vertices[i].position = XMFLOAT3(currVertex.x, currVertex.y, currVertex.z);
		vertices[i].texture = XMFLOAT2(currVertex.tu, currVertex.tv);
		vertices[i].normal = XMFLOAT3(currVertex.nx, currVertex.ny, currVertex.nz);

		indices[i] = i;
	}

	vertexBufferDesc.Usage = D3D11_USAGE_DEFAULT;
	vertexBufferDesc.ByteWidth = sizeof(VertexType) * vertexCount;
	vertexBufferDesc.BindFlags = D3D11_BIND_VERTEX_BUFFER;
	vertexBufferDesc.CPUAccessFlags = 0;
	vertexBufferDesc.MiscFlags = 0;
	vertexBufferDesc.StructureByteStride = 0;
	//Give the subresource structure a pointer to the index data
	vertexData.pSysMem = vertices;
	vertexData.SysMemPitch = 0;
	vertexData.SysMemSlicePitch = 0;

	//create an vertexBuffer
	result = device->CreateBuffer(&vertexBufferDesc, &vertexData, &vertexBuffer);
	if (FAILED(result))
	{
		return false;
	}

	//description of static buffer
	indexBufferDesc.Usage = D3D11_USAGE_DEFAULT;
	indexBufferDesc.ByteWidth = sizeof(unsigned long) * indexCount;
	indexBufferDesc.BindFlags = D3D11_BIND_INDEX_BUFFER;
	indexBufferDesc.CPUAccessFlags = 0;
	indexBufferDesc.MiscFlags = 0;
	indexBufferDesc.StructureByteStride = 0;

	//subresource structure a pointer to the index data
	indexData.pSysMem = indices;
	indexData.SysMemPitch = 0;
	indexData.SysMemSlicePitch = 0;

	//create index buffer
	result = device->CreateBuffer(&indexBufferDesc, &indexData, &indexBuffer);
	if (FAILED(result))
	{
		return false;
	}

	delete[] vertices;
	vertices = 0;

	delete[] indices;
	indices = 0;

	return true;
}
void ModelClass::ShutdownBuffers()
{
	if (indexBuffer)
	{
		indexBuffer->Release();
		indexBuffer = 0;
	}
	if (vertexBuffer)
	{
		vertexBuffer->Release();
		vertexBuffer = 0;
	}
}

void ModelClass::RenderBuffers(ID3D11DeviceContext *deviceContext)
{
	unsigned int stride;
	unsigned int offset;

	stride = sizeof(VertexType);
	offset = 0;
	//set the buffers to active in the input assembler, so they could be rendered
	deviceContext->IASetVertexBuffers(0, 1, &vertexBuffer, &stride, &offset);
	deviceContext->IASetIndexBuffer(indexBuffer, DXGI_FORMAT_R32_UINT, 0);
	deviceContext->IASetPrimitiveTopology(D3D11_PRIMITIVE_TOPOLOGY_TRIANGLELIST);
}

bool ModelClass::LoadTexture(ID3D11Device * device, ID3D11DeviceContext *deviceContext, char * filename)
{
	bool result;

	texture = new TextureClass();
	if (!texture)
	{
		return false;
	}

	result = texture->Initialize(device, deviceContext, filename);
	if (!result)
	{
		return false;
	}
	return true;
}


void ModelClass::ReleaseTexture()
{
	if (texture)
	{
		texture->Shutdown();
		delete texture;
		texture = 0;
	}
}

bool ModelClass::LoadModel(char *filename)
{
	std::ifstream fin;
	char input;
	int i;

	fin.open(filename);

	if (fin.fail())
	{
		return false;
	}

	fin.get(input);
	//Read up the vertex cout
	while (input != ':')
	{
		fin.get(input);
	}

	fin >> vertexCount;
	indexCount = vertexCount;

	model = new ModelType[vertexCount];
	if (!model)
	{
		return false;
	}

	//read the beggining of the data
	fin.get(input);
	while (input != ':')
	{
		fin.get(input);
	}
	//read empty lines
	fin.get(input);
	fin.get(input);

	//read the vertex data
	for (int i = 0; i < vertexCount; i++)
	{
		fin >> model[i].x >> model[i].y >> model[i].z;
		fin >> model[i].tu >> model[i].tv;
		fin >> model[i].nx >> model[i].ny >> model[i].nz;
	}
	fin.close();

	return true;
}

void ModelClass::ReleaseModel()
{
	if (model)
	{
		delete[] model;
		model = 0;
	}
}

ID3D11ShaderResourceView* ModelClass::GetTexture()
{
	return texture->GetTexture();
}
