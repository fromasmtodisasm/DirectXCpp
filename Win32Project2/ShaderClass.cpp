#include "ShaderClass.h"

ShaderClass::ShaderClass()
{
}


ShaderClass::~ShaderClass()
{
}

void ShaderClass::Init(ID3D11Device* device, HWND hwnd) //init pipeline
{
	ID3D10Blob *VertexShader, *PixelShader;
	//shaders are compiled (with HLSL4 and than stored as BLOB VS and PS)
	D3DX11CompileFromFile(L"shaders.shader", 0, 0, "VShader", "vs_4_0", 0, 0, 0, &VertexShader, 0, 0);
	D3DX11CompileFromFile(L"shaders.shader", 0, 0, "PShader", "ps_4_0", 0, 0, 0, &PixelShader, 0, 0);

	device->CreateVertexShader(VertexShader->GetBufferPointer(), VertexShader->GetBufferSize(), NULL, &vertexShader);
	device->CreatePixelShader(PixelShader->GetBufferPointer(), PixelShader->GetBufferSize(), NULL, &pixelShader);

	D3D11_INPUT_ELEMENT_DESC ied[] =
	{
		{ "POSITION", 0, DXGI_FORMAT_R32G32B32_FLOAT, 0, 0, D3D11_INPUT_PER_VERTEX_DATA, 0 },
		{ "COLOR", 0, DXGI_FORMAT_R32G32B32_FLOAT, 0, 12, D3D11_INPUT_PER_VERTEX_DATA }
	};

	device->CreateInputLayout(ied, 2, VertexShader->GetBufferPointer(), VertexShader->GetBufferSize(), &layout);
}
void ShaderClass::SetShader(ID3D11DeviceContext* deviceContext)
{
	//copy to buffer
	D3D11_MAPPED_SUBRESOURCE mappedSubresource;
	deviceContext->Map(vertexBuffer, NULL, D3D11_MAP_WRITE_DISCARD, NULL, &mappedSubresource);//previous buffer deleted...
	//memcpy(mappedSubresource.pData, OurVertices, sizeof(OurVertices));
	deviceContext->Unmap(vertexBuffer, NULL);
}
void ShaderClass::RenderShader(ID3D11DeviceContext* deviceContext)
{
	SetShader(deviceContext);
	
	deviceContext->IASetInputLayout(layout);

	deviceContext->VSSetShader(vertexShader, 0, 0); //rename to devcon
	deviceContext->PSSetShader(pixelShader, 0, 0);
	//drawit
}

void ShaderClass::Clean()
{
	layout->Release();
	vertexShader->Release();
	pixelShader->Release();
}