using Ch01_01EmptyProject.Graphic.Structures;
using System;
namespace Ch01_01EmptyProject.Graphic.Shaders
{
    public interface IShaderEffect
    {
        string EffectShaderFileName { get; }
        string PsFunctionName { get; }
        PsVersion PsVersion { get; }
        string VsFunctionName { get; }
        VsVersion VsVersion { get; }
        VertexType VertexType  { get; }
    }
}
