using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch01_01EmptyProject.Graphic.Shaders
{
    class Texture : IDisposable
    {
        public ShaderResourceView TextueResource { get; private set; }

        public Texture(Device device, string textureFileName)
        {
            TextueResource = ShaderResourceView.FromFile(device, textureFileName);
        }

        public void Dispose()
        {
            TextueResource.Dispose();
        }
     }
}
