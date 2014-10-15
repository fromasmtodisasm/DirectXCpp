using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ch01_01EmptyProject.Graphic.Shaders
{
    class SpecularLighting : ShaderEffectBase
    {
        string effectShaderFileName = @"Graphic\Shaders\Effects\SpecularLighting.fx";

        public override string EffectShaderFileName
        {
            get
            {
                return effectShaderFileName;
            }
        }
    }
}
