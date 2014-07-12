#include "inputclass.h"

InputClass::InputClass()
{
}

InputClass::InputClass(const InputClass& other)
{
}

InputClass::~InputClass()
{
}

void InputClass::Initialize()
{
	int i;

	for (i = 0; i < 256; i++) //set all the keys to default(not pressed) value)
	{
		mKeys[i] = false;
	}

	return;
}

void InputClass::KeyUp(unsigned int input)
{
	mKeys[input] = false;
	return;
}

bool InputClass::IsKeyDown(unsigned int key)
{
	return mKeys[key]; //return state (pressed/notPressed)
}
