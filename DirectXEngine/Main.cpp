#include "systemclass.h"

int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPreviousInstance, PSTR pScrndLine, int iCmdshow)
{
	SystemClass* System;
	bool result;

	System = new SystemClass;
	if (!System)
	{
		return 0;
	}

	result = System->Initialize();
	if (result)
	{
		System->Run(); //aplication code
	}

	System->Shutdown();
	delete System;

	System = 0;

	return 0;
};
