using System;
using SharpDX.Direct3D11;

namespace Ch01_01EmptyProject.Graphic
{
    interface IRenderable
    {
        void Render(DeviceContext deviceContext);
    }
}
