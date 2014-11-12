using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ch01_01EmptyProject.Graphic.Shaders
{
    public static class BufferTypes
    {   [StructLayout(LayoutKind.Sequential)]
        public struct WorldViewProjComputed
        {
            public Matrix worldViewProj;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WorldViewProj
        {
            public Matrix worldViewProj;
            public Matrix worldMatrix;
            public Matrix worldInverseTranspose;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DiffuseLightBufferType
        {
            public Vector4 diffuseColor;
            public Vector3 lightDirection;
            // Added extra padding so structure is a multiple of 16 for CreateBuffer function requirements.
            public float padding;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct SpecularLightBufferType
        {
            public Vector4 ambientColor;
            public Vector4 diffuseColor;
            public Vector3 lightDirection;
            public float specularPower;
            public Vector4 specularColor;
            // Added extra padding so structure is a multiple of 16 for CreateBuffer function requirements.
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct AmbientLightBufferType
        {
            public Vector4 ambientColor;
            public Vector4 diffuseColor;
            public Vector3 lightDirection;
            // Added extra padding so structure is a multiple of 16 for CreateBuffer function requirements.
            public float padding;
        }

        [StructLayout(LayoutKind.Sequential)]
       public struct CameraBufferType
        {
            public Vector3 cameraPosition;
            public float padding;
        }


        public struct Material
        {
            public Vector4 ambient;
            public Vector4 diffuse;
            public Vector4 specular;
            public float shininess;
        }
                
               
    }
}
