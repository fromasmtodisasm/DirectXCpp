#ifndef _SYSTEMCLASS_H_
#define _SYSTEMCLASS_H_

#define WIN32_LEAN_AND_MEAN // include just necessary files ?

#include <windows.h>

#include "inputclass.h" //here i put the input handle code
#include "graphicsclass.h" //displaing stuff...

class SystemClass
{
private:
	bool Frame();
	void InitializeWindows(int&, int&);
	void ShutdownWindows();

private:
	LPCWSTR mapplicationName;
	HINSTANCE mHinstance;
	HWND mHwnd;

	InputClass* mInput;
	GraphicsClass* mGraphics;

public:
	SystemClass();
	SystemClass(const SystemClass&);
	~SystemClass();

	bool Initialize();
	void Shutdown();
	void Run();

	LRESULT CALLBACK MessageHandler(HWND, UINT, WPARAM, LPARAM);
};

//GLOBAL
static SystemClass* ApplicationHandle = 0;

//FUNCTION PROTOTYPE
static LRESULT CALLBACK WndProc(HWND, UINT, WPARAM, LPARAM);

#endif