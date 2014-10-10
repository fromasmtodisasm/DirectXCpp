using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.Windows;
using SharpDX.DXGI;
using SharpDX.Direct3D11;
using Device = SharpDX.Direct3D11.Device;
using SharpDX.Direct3D;
using System.Windows.Forms;

namespace Ch01_01EmptyProject
{
    struct Vertex2
    {
        Vector3 Position;
        Vector3 Normal;
        Vector2 Tex0;
        Vector2 Tex1;
    }
}
