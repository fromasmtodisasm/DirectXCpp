using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ch01_01EmptyProject.Graphic.Shaders
{
    class ParallaxMapping : ShaderEffectBase
    {
        string effectShaderFileName = @"Graphic\Shaders\Effects\parallaxMapping.fx";
        //string effectShaderFileName = @"Graphic\Shaders\Effects\parallaxMappingTBNMatrix.fx";
        //string effectShaderFileName = @"Graphic\Shaders\Effects\parallaxOclussionMapping.fx";

        private string psFunctionName = "ParallaxMapPixelShader";
        private string vsFunctionName = "ParallaxMapVertexShader"; 

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
    }
}
