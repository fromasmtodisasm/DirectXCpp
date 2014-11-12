using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ch01_01EmptyProject.Graphic.Shaders
{
    class BumpMapping : ShaderEffectBase
    {
        string effectShaderFileName = @"Graphic\Shaders\Effects\bumpMappingRastertek.fx";
        private string psFunctionName = "BumpMapPixelShader";
        private string vsFunctionName = "BumpMapVertexShader"; 

        public override string EffectShaderFileName
        {
            get
            {
                return effectShaderFileName;
            }
        }

        public override string PsFunctionName
        {
            get
            {
                return psFunctionName;
            }
        }

        public override string VsFunctionName
        {
            get
            {
                return vsFunctionName;
            }
        }

        public override Structures.VertexType VertexType
        {
            get
            {
                return Structures.VertexType.TextureNormalTangentBinormalVertex;
            }
        }

        //public List<Type> GetPerObjectBufferType()
        //{
        //    List<Type> buffers = new List<Type>();
        //    buffers.Add(typeof(BufferTypes.DiffuseLightBufferType));
        //    buffers.Add(typeof(BufferTypes.CameraBufferType));

        //    return buffers;
        //}
    }
}
