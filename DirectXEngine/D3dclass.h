//#pragma once
//
//#ifndef _D3DCLASS_H_
//#define _D3DCLASS_H_
//
//#pragma comment(lib, "d3d10.lib")
//#pragma comment(lib, "d3dx10.lib")
//#pragma comment(lib, "dxgi.lib")
//
//#include <d3d10.h>
//#include <d3dx10.h>
//
//class D3DClass
//{
//public:
//	D3DClass();
//	D3DClass(const D3DClass&);
//	~D3DClass();
//
//	bool Initialize(int, int, bool, HWND, bool, float, float);
//	void Shutdown();
//
//	void BeginScene(float, float, float, float);
//	void EndScene();
//
//	ID310Device* GetDevice();
//
//	void GetProjectionMatrix(D3DMATRIX&);
//	void GetWorldMatrix(D3DMATRIX&);
//	void GetOrthoMatrix(D3DMATRIX&);
//
//	void GetVideoCardInfo(char*, int&);
//	
//private:
//	bool vsyncEnabled;
//	int videoCardMemory;
//	char videoCardDescription[128];
//	IDXGISwapChain* swapChain;
//	ID3D10Device* device;
//	ID3D10RenderTargetView* renderTargetView;
//	ID3D10Texture2D* depthStencilBuffer;
//	ID3D10DepthStencilState* depthStencilState;
//	ID3D10DepthStencilView* depthStencilView;
//	ID3D10RasterizerState* rasterState;
//	D3DXMATRIX projectionMatrix;
//	D3DXMATRIX worldMatrix;
//	D3DXMATRIX orthoMatrix;
//};
//
//#endif