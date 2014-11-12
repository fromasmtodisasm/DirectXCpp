using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch01_01EmptyProject.Graphic.Shaders
{
   public class DirectionalLightingParallax : ShaderEffectBase
    {
       //only at this effect is need to specify full path ?? BUG
       string effectShaderFileName = @"D:\GitHub\DirectXCpp\Ch01_01EmptyProject\Graphic\Shaders\Effects\DirectionalLighting.fx";

       string vsFunctionName = "LightVertexShader";
       string psFunctionName = "LightPixelShader"; 

       public override string EffectShaderFileName
        {
            get
            {
                return effectShaderFileName;
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

        public override Structures.VertexType VertexType
        {
            get
            {
                return Structures.VertexType.TextureNormalTangentVertex;
            }
        }
    }
}
