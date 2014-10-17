using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch01_01EmptyProject.Graphic.Shaders
{
   public class DiffuseLighting : ShaderEffectBase
    {
       string effectShaderFileName = @"Graphic\Shaders\Effects\DiffuseLightingRastertek.fx";
       //string effectShaderFileName = @"Graphic\Shaders\Effects\DiffuseLighting.fx";
       string vsFunctionName = "LightVertexShader";
       string psFunctionName = "LightPixelShader"; 

       //string vsFunctionName = "VertexShaderFunction";
       // string psFunctionName = "PixelShaderFunction";
        
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
    }
}
