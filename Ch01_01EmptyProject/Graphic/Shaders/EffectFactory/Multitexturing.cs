using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ch01_01EmptyProject.Graphic.Shaders
{
    class Multitexturing : ShaderEffectBase
    {
        string effectShaderFileName = @"Graphic\Shaders\Effects\MultitexturingRasterek.fx";
        private string psFunctionName = "MultiTexturePixelShader";
        private string vsFunctionName = "MultiTextureVertexShader"; 

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

        public override PsVersion PsVersion
        {
            get
            {
                return PsVersion.ps_5_0;
            }
        }
        public override VsVersion VsVersion
        {
            get
            {
                return VsVersion.vs_5_0;
            }
        }

        public override Structures.VertexType VertexType
        {
            get
            {
                return Structures.VertexType.TextureVertex;
            }
        }
    }
}
