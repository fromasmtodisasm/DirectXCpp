using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.Windows;
using SharpDX.DXGI;
using SharpDX.Direct3D11;
using SharpDX.D3DCompiler;

namespace Ch01_01EmptyProject.Graphic
{
    interface IGraphic
    {
        void Frame();
        void Render();
    }
}
