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
    }
}
