using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ch01_01EmptyProject.Graphic.Shaders
{
    class SpecularLighting : ShaderEffectBase
    {
        string effectShaderFileName = @"Graphic\Shaders\Effects\SpecularLightingRastertek.fx";
        private string psFunctionName = "SpecularPixelShaderFunction";
        private string vsFunctionName = "SpecularVertexShaderFunction";

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
                return Structures.VertexType.TextureNormalVertex;
            }
        }
    }
}
