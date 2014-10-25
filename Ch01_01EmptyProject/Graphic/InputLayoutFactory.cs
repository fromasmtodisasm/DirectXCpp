using Ch01_01EmptyProject.Graphic.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Direct3D11;

namespace Ch01_01EmptyProject.Graphic
{
    static class InputLayoutFactory
    {
        private static InputElement[] inputElementDesc;

        public static InputElement[] Create(VertexType vertexType)
        {
            switch (vertexType)
            {
                case VertexType.ColorVertex:
                    inputElementDesc = VertexInputLayouts.ColorVertex();
                    break;
                //case VertexType.NormalVertex:
                // break;
                case VertexType.TextureVertex:
                    {
                        inputElementDesc = VertexInputLayouts.TextureVertex();
                    }
                    break;
                case VertexType.TextureNormalVertex:
                    {
                        inputElementDesc = VertexInputLayouts.TextureNormalVertex();
                    }
                    break;
                case VertexType.TextureNormalTangentBinormalVertex:
                    {
                        inputElementDesc = VertexInputLayouts.TextureNormalTangentBinormalVertex();
                    }
                    break;
                case VertexType.TextureNormalTangentVertex:
                    {
                        inputElementDesc = VertexInputLayouts.TextureNormalTangentVertex();
                    }
                    break;

                //case VertexType.ColorNormalVertex:
                // break;
                //default:
                // break;
            }
            return inputElementDesc;

        }
    }
}
