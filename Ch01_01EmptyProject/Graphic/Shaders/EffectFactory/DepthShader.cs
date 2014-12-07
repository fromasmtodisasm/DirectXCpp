using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ch01_01EmptyProject.Graphic.Shaders
{
    class DepthShader : ShaderEffectBase
    {
        string effectShaderFileName = @"Graphic\Shaders\Effects\DepthShader.fx";
        private string psFunctionName = "PixelDepthShaderFunction";
        private string vsFunctionName = "VertexDepthShaderFunction"; 

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
                return Structures.VertexType.ColorVertex;
            }
        }
   }
}
