using System;
namespace Ch01_01EmptyProject.Graphic
{
   public interface IShape
    {
        int[] Indexes { get; }
        SharpDX.Vector3[] Vertexes { get; }
    }
}
