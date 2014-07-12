//#include "D3dclass.h"
//
//
//D3DClass::D3DClass()
//{
//	device = 0;
//	swapChain = 0;
//	renderTargetView = 0;
//	depthStencilBuffer = 0;
//	depthStencilState = 0;
//	depthStencilView = 0;
//	rasterState = 0;
//}
//
//
//D3DClass::~D3DClass()
//{
//}
//
//bool D3DClass::Initialize(int screenWidth, int screenHeight, bool vsync, HWND hwnd, bool fullscreen, float screenDepth, float screenNear)
//{
//	HRESULT result;
//	IDXGIFactory* factory;
//	IDXGIAdapter* adapter;
//	IDXGIOutput* adapterOutput;
//	unsigned int numModes, i, numerator, denominator, stringLength;
//	DXGI_MODE_DESC* displayModeList;
//	DXGI_ADAPTER_DESC adapterDesc;
//	int error;
//	DXGI_SWAP_CHAIN_DESC swapChainDesc;
//	ID3D10Texture2D* backBufferPtr;
//	D3D10_TEXTURE2D_DESC depthBufferDesc;
//	D3D10_DEPTH_STENCIL_DESC depthStencilDesc;
//	D3D10_DEPTH_STENCIL_VIEW_DESC depthStencilViewDesc;
//	D3D10_VIEWPORT viewport;
//	float fieldOfView, screenAspect;
//	D3D10_RASTERIZER_DESC rasterDesc;
//
//	vsyncEnabled = vsync;
//}