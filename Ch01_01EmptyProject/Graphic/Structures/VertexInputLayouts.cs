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

namespace Ch01_01EmptyProject
{
    public struct VertexInputLayouts
    {
        public static InputElement[] ColorNormalVertex()
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
            },
            new InputElement()

            {
                SemanticName = "NORMAL",
                SemanticIndex = 0,
                Format = Format.R32G32B32A32_Float,
                Slot = 0,
                AlignedByteOffset = 24,
                Classification = InputClassification.PerVertexData,
                InstanceDataStepRate = 0
            }
        };
        }
        public static InputElement[] ColorVertex()
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

        public static InputElement[] NormalVertex()
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
                SemanticName = "NORMAL",
                SemanticIndex = 0,
                Format = Format.R32G32B32A32_Float,
                Slot = 0,
                AlignedByteOffset = 12,
                Classification = InputClassification.PerVertexData,
                InstanceDataStepRate = 0
            }
        };
        }

        public static InputElement[] TextureVertex()
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
                SemanticName = "TEXCOORD",
                SemanticIndex = 0,
                Format = Format.R32G32_Float,
                Slot = 0,
                AlignedByteOffset = 12,
                Classification = InputClassification.PerVertexData,
                InstanceDataStepRate = 0
            }
        };
        }
    }
}


