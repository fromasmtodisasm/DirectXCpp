using System;
namespace Ch01_01EmptyProject.Graphic.Cameras
{
    interface ICamera
    {
        global::SharpDX.Vector3 Position { get; set; }
        void Render();
        global::SharpDX.Vector3 Rotation { get; set; }
        global::SharpDX.Matrix ViewMatrix { get; set; }
    }
}
