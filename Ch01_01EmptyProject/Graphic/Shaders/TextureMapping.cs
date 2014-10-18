using Ch01_01EmptyProject.Graphic.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ch01_01EmptyProject.Graphic.Shaders
{
   public class TextureMapping : ShaderEffectBase
    {
       string effectShaderFileName = @"Graphic\Shaders\Effects\TextureShader.fx";

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
                return "TexturePixelShader";
            }
        }

        public override string VsFunctionName
        {
            get
            {
                return "TextureVertexShader";
            }
        }

        public override VertexType VertexType
        {
            get
            {
                return VertexType.TextureVertex;
            }
        }
   }
}
