#include "D3DClass.h"

#define SCREEN_WIDTH 800
#define SCREEN_HEIGHT 600

D3DClass::D3DClass()
{
}


D3DClass::~D3DClass()
{
}

//Getters
ID3D11Device* D3DClass::GetDevice()
{
	return device;
}


ID3D11DeviceContext* D3DClass::GetDeviceContext()
{
	return deviceContext;
}

void D3DClass::CleanD3D(void) //cleanup
{
	swapchain->SetFullscreenState(FALSE, NULL); //in order to close window, need to switch to windowed mode

	swapchain->Release();
	backbuffer->Release();
	device->Release();
	deviceContext->Release();
}

void D3DClass::InitD3D(HWND hWnd)
{
	// create a struct to hold information about the swap chain
	DXGI_SWAP_CHAIN_DESC screenchainData;

	// clear the struct
	ZeroMemory(&screenchainData, sizeof(DXGI_SWAP_CHAIN_DESC));

	screenchainData.BufferCount = 1;
	screenchainData.BufferDesc.Width = SCREEN_WIDTH;
	screenchainData.BufferDesc.Height = SCREEN_HEIGHT;
	screenchainData.BufferDesc.Format = DXGI_FORMAT_R8G8B8A8_UNORM;     // 32-bit color
	screenchainData.BufferUsage = DXGI_USAGE_RENDER_TARGET_OUTPUT;
	screenchainData.OutputWindow = hWnd;                                // window to be used
	screenchainData.SampleDesc.Count = 1;                               // how many multisamples (antialising)
	screenchainData.SampleDesc.Quality = 0;                             // multisample quality level
	screenchainData.Windowed = TRUE;
	screenchainData.Flags = DXGI_SWAP_CHAIN_FLAG_ALLOW_MODE_SWITCH;

	D3D11CreateDeviceAndSwapChain(NULL,
		D3D_DRIVER_TYPE_HARDWARE,
		NULL,
		NULL,
		NULL,
		NULL,
		D3D11_SDK_VERSION,
		&screenchainData,
		&swapchain,
		&device,
		NULL,
		&deviceContext);

	ID3D11Texture2D *pBackBuffer;
	swapchain->GetBuffer(0, __uuidof(ID3D11Texture2D), (LPVOID*)&pBackBuffer);

	device->CreateRenderTargetView(pBackBuffer, NULL, &backbuffer); // use the back buffer address to create the render target
	pBackBuffer->Release();

	deviceContext->OMSetRenderTargets(1, &backbuffer, NULL);// render target as the back buffer

	D3D11_VIEWPORT viewport;
	ZeroMemory(&viewport, sizeof(D3D11_VIEWPORT));

	viewport.TopLeftX = 0;
	viewport.TopLeftY = 0;
	viewport.Width = SCREEN_WIDTH;
	viewport.Height = SCREEN_HEIGHT;

	deviceContext->RSSetViewports(1, &viewport);
}

void D3DClass::StartRender()
{
	// "clear" the back buffer to a blue
	deviceContext->ClearRenderTargetView(backbuffer, D3DXCOLOR(0.0f, 0.2f, 0.4f, 1.0f));
}

void D3DClass::RenderComplete()
{
	swapchain->Present(0, 0); // switch the back buffer and the front buffer
}


