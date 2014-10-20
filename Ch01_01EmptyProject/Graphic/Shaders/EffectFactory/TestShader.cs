using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ch01_01EmptyProject.Graphic.Shaders
{
    class TestShader : ShaderEffectBase
    {
        string effectShaderFileName = @"Graphic\Shaders\Effects\TestShader.fx";
        string vsFunctionName = "VS";
        string psFunctionName = "PS";

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

        public override VsVersion VsVersion
        {
            get
            {
                return VsVersion.vs_4_0;
            }
        }

        public override PsVersion PsVersion
        {
            get
            {
                return PsVersion.ps_4_0;
            }
        }
    }
}
