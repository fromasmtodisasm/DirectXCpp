#include <d3d11.h>
#include <d3dx11.h>
#include <d3dx10.h>

#pragma comment (lib, "d3d11.lib")
#pragma comment (lib, "d3dx11.lib")
#pragma comment (lib, "d3dx10.lib")

#pragma once
class D3DClass
{
public:
	D3DClass();
	~D3DClass();

	void CleanD3D(void);
	void InitD3D(HWND hWnd);
	
	void StartRender(void);
	void RenderComplete(void);

	ID3D11Device* GetDevice();
	ID3D11DeviceContext* GetDeviceContext();
	
private:
	IDXGISwapChain *swapchain;             //  global pointers
	ID3D11RenderTargetView *backbuffer;
	ID3D11Device *device;                     // Direct3D device interface
	ID3D11DeviceContext *deviceContext;           // Direct3D device context - virtual video adapter
};

