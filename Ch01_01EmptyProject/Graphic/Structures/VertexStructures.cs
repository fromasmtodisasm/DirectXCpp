using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.Windows;
using SharpDX.DXGI;
using SharpDX.Direct3D11;
using Device = SharpDX.Direct3D11.Device;
using SharpDX.Direct3D;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Ch01_01EmptyProject.Graphic.Structures
{
  public enum VertexType
    {
        ColorVertex,
        NormalVertex,
        TextureVertex,
        ColorNormalVertex,
    }


    //class VertexTypes and class VertexInputLayouts
    [StructLayout(LayoutKind.Sequential)]
    public struct ColorVertex : IVertex
    {
        private Vector3 position;
        private Vector4 color;

        public Vector4 Color
        {
            get { return color; }
            set { color = value; }
        }

        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NormalVertex : IVertex
    {
        private Vector3 position;
        private Vector3 normal;

        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }


        public Vector3 Normal
        {
            get { return normal; }
            set { normal = value; }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TextureVertex : IVertex
    {
        private Vector3 position;
        private Vector2 texture;
     
        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }
        public Vector2 Texture
        {
            get { return texture; }
            set { texture = value; }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TextureNormalVertex : IVertex
    {
        private Vector3 position;
        private Vector2 texture;
        private Vector3 normal;

        public Vector3 Normal
        {
            get { return normal; }
            set { normal = value; }
        }

        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }
        public Vector2 Texture
        {
            get { return texture; }
            set { texture = value; }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ColorNormalVertex : IVertex
    {
        private Vector3 position;
        private Vector4 color;
        private Vector3 normal;

        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }
        public Vector4 Color
        {
            get { return color; }
            set { color = value; }
        }

        public Vector3 Normal
        {
            get { return normal; }
            set { normal = value; }
        }
    }
}

