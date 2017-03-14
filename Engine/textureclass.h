#pragma once
#include <d3d11.h>
#include <stdio.h>

class TextureClass
{
public:
	TextureClass();
	TextureClass(const TextureClass &other);
	~TextureClass();

	bool Initialize(ID3D11Device *device, ID3D11DeviceContext *deviceContext, char * filename);
	void Shutdown();
	ID3D11ShaderResourceView* GetTexture();

protected:
	
private:
	bool LoadTarga(char *filename, int &with, int &heght);
	ID3D11Texture2D *texture;
	ID3D11ShaderResourceView *textureView;
	unsigned char* targaData;

	struct TargaHeader
	{
		unsigned char data[12];
		unsigned short width;
		unsigned short height;
		unsigned char bpp;
		unsigned char data2;
	};
};
