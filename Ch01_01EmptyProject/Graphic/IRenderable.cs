using System;
using SharpDX.Direct3D11;

namespace Ch01_01EmptyProject
{
    interface IRenderable
    {
        void Render(DeviceContext deviceContext);
    }
}
