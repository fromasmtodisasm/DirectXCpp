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
    public class InputElementBase
    {
        string SemanticName = "POSITION";
        int SemanticIndex = 0;
        Format Format = Format.R32G32B32A32_Float;
        int Slot = 0;
        int AlignedByteOffset = 0;
    }

    public struct VertexInputLayouts
    {
        public static InputElement Position
        {
            get
            {
                var position = new InputElement();
                position.SemanticName = "POSITION";
                position.Format = Format.R32G32B32A32_Float;
                return position;
            }
          

        }

        public static InputElement Color
        {
            get
            {
                var color = new InputElement();
                color.SemanticName = "COLOR";
                color.Format = Format.R32G32B32A32_Float;
                color.AlignedByteOffset = 12;
                return color;
            }
          
        }

        public static InputElement Normal
        {
            get
            {
                var normal = new InputElement();
                normal.SemanticName = "NORMAL";
                normal.Format = Format.R32G32B32A32_Float;
                normal.AlignedByteOffset = 24;
                return normal;
            }
          
        }

        public static InputElement TexCoord
        {
            get
            {
                var texcoord = new InputElement();
                texcoord.SemanticName = "TEXCOORD";
                texcoord.SemanticIndex = 0;
                texcoord.Format = Format.R32G32_Float;
                texcoord.Slot = 0;
                texcoord.AlignedByteOffset = 12;
                return texcoord;
            }
          
        }


        public static InputElement[] ColorNormalVertex()
        {
            return new InputElement[] { Position, Color, Normal };
        }

        public static InputElement[] ColorVertex()
        {
            return new InputElement[] { Position, Color };
        }

        public static InputElement[] NormalVertex()
        {
            return new InputElement[] { Position, Normal };
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
            },
            new InputElement()
            {
                SemanticName = "TEXCOORD",
                SemanticIndex = 0,
                Format = Format.R32G32_Float,
                Slot = 0,
                AlignedByteOffset = 12,
            }
        };
        }
    }
}


