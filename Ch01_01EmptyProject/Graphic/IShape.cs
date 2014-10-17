using System;
namespace Ch01_01EmptyProject.Graphic
{
    interface IShape
    {
        int[] Indexes { get; }
        SharpDX.Vector3[] Vertexes { get; }
    }
}
