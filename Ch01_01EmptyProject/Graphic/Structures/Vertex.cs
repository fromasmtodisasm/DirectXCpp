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

namespace Ch01_01EmptyProject
{
    //enum Vertex, enum Input Layout
    //structure
    
    //class VertexTypes and class VertexInputLayouts

    [StructLayout(LayoutKind.Sequential)]
    public struct Vertex
    {
        public Vector3 Position;
        public Vector4 Color;
    }

    public struct VertexInputLayouts
    {
        public static InputElement[] Vertex()
        {
            return new InputElement[]
        {
            new InputElement()
            {
            SemanticName = "POSITION",
            SemanticIndex = 0,
            Format = Format.R32G32B32A32_Float,
            Slot = 0,
            AlignedByteOffset = 0,
            Classification = InputClassification.PerVertexData,
            InstanceDataStepRate = 0
            },
           new InputElement()

            {
                SemanticName = "COLOR",
                SemanticIndex = 0,
                Format = Format.R32G32B32A32_Float,
                Slot = 0,
                AlignedByteOffset = 12,
                Classification = InputClassification.PerVertexData,
                InstanceDataStepRate = 0
            }
        };
        }
    }
}
