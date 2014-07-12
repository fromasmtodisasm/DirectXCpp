#include <windows.h>
#include <windowsx.h>

#include "GraphicsClass.h"


LRESULT CALLBACK WindowProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam);

int WINAPI WinMain(HINSTANCE hInstance,
	HINSTANCE hPrevInstance,
	LPSTR lpCmdLine,
	int nCmdShow)
{
	HWND hWnd;
	WNDCLASSEX windowsClass;
	GraphicsClass* graphicsClass;

	ZeroMemory(&windowsClass, sizeof(WNDCLASSEX));

	windowsClass.cbSize = sizeof(WNDCLASSEX);
	windowsClass.style = CS_HREDRAW | CS_VREDRAW;
	windowsClass.lpfnWndProc = WindowProc;
	//windowsClass.hInstance = hInstance;
	windowsClass.hCursor = LoadCursor(NULL, IDC_ARROW);
	//wc.hbrBackground = (HBRUSH)COLOR_WINDOW; //no color in fullscreen
	windowsClass.lpszClassName = L"WindowClass";

	RegisterClassEx(&windowsClass);

	RECT wr = { 0, 0, 800, 600 };
	AdjustWindowRect(&wr, WS_OVERLAPPEDWINDOW, FALSE);

	hWnd = CreateWindowEx(NULL,
		L"WindowClass",
		L"DirectX Test",
		WS_OVERLAPPEDWINDOW,
		300,
		300,
		800,
		600,
		NULL,
		NULL,
		hInstance,
		NULL);

	ShowWindow(hWnd, nCmdShow);

	graphicsClass = new GraphicsClass;
	// set up and initialize Direct3D  -> in graphicClass
	graphicsClass->Init(hWnd);

	
	MSG msg;

	// enter the main loop:
	while (TRUE)
	{
		if (PeekMessage(&msg, NULL, 0, 0, PM_REMOVE))
		{
			TranslateMessage(&msg);
			DispatchMessage(&msg);

			if (msg.message == WM_QUIT)
				break;
		}

		//GraphicClass->	//RenderFrame();
	}
	//GraphicClass->   //CleanD3D();

	return msg.wParam;
}


// this is the main message handler for the program
LRESULT CALLBACK WindowProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	switch (message)
	{
	case WM_DESTROY:
	{
					   PostQuitMessage(0);
					   return 0;
	} break;
	}

	return DefWindowProc(hWnd, message, wParam, lParam);
}
