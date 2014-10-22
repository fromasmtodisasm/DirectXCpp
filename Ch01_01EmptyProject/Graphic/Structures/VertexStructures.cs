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
        TextureNormalVertex,
        ColorNormalVertex,
        TextureNormalTangentBinormalVertex,
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

    public struct TextureNormalTangentBinormalVertex : IVertex
    {
        private Vector3 position;
        private Vector2 texture;
        private Vector3 normal;
        private Vector3 binormal;
        private Vector3 tangent;

        public Vector3 Binormal
        {
            get { return binormal; }
            set { binormal = value; }
        }

        public Vector3 Tangent
        {
            get { return tangent; }
            set { tangent = value; }
        }

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

