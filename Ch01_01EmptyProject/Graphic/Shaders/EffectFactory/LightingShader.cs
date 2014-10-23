using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ch01_01EmptyProject.Graphic.Shaders
{
   public class LightingShader : ShaderEffectBase
    {
        string effectShaderFileName = @"Graphic\Shaders\Effects\DirectionalLightingBook.fx";
        private string vsFunctionName = "VertexShaderFunction";
        private string psFunctionName = "PixelShaderFunction";

        public override string EffectShaderFileName
        {
            get
            {
                return effectShaderFileName;
            }
        }
        public override Structures.VertexType VertexType
        {
            get
            {
                return Structures.VertexType.TextureNormalVertex;
            }
        }
        public override string VsFunctionName
        {
            get
            {
                return vsFunctionName;
            }
        }
        public override string PsFunctionName
        {
            get
            {
              return psFunctionName;
            }
        }
   }
}
