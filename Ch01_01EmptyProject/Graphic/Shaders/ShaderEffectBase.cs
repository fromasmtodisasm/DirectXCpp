using Ch01_01EmptyProject.Graphic.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch01_01EmptyProject.Graphic.Shaders
{
   public enum VsVersion
    {
        vs_5_0,
        vs_4_0,
        vs_3_0,
        vs_2_0
    }
   public enum PsVersion
    {
        ps_5_0,
        ps_4_0,
        ps_3_0,
        ps_2_0
    }

   public abstract class ShaderEffectBase : IShaderEffect
    {
        string effectShaderFileName;
        string vsFunctionName = "VertexShaderFunction";
        string psFunctionName = "PixelShaderFunction";
        PsVersion psVersion = PsVersion.ps_5_0;
        VsVersion vsVersion = VsVersion.vs_5_0;
        VertexType vertexType = VertexType.ColorVertex;

        public virtual VertexType VertexType
        {
            get { return vertexType; }
            //set { vertexType = value; }
        } 

        public virtual string PsFunctionName
        {
            get { return psFunctionName; }
            //set { psFunctionName = value; }
        }

        public virtual string VsFunctionName
        {
            get { return vsFunctionName; }
            //set { vsFunctionName = value; }
        }

        public virtual string EffectShaderFileName
        {
            get { return effectShaderFileName; }
            //set { effectShaderFileName = value; }
        }

        public virtual VsVersion VsVersion
        {
            get { return vsVersion; }
            //set { vsVersion = value; }
        }

        public virtual PsVersion PsVersion
        {
            get { return psVersion; }
            //set { psVersion = value; }
        }
   }
}
