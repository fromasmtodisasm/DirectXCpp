using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch01_01EmptyProject.Graphic.Shaders
{
    class Texture
    {
        private ShaderResourceView textureResource;

        public static ShaderResourceView Load(Device device)
        {
            string textureFileName = @"D:\Github\DirectXCpp\Ch01_01EmptyProject\Graphic\Shaders\Textures\wall.dds";

            try
            {
             return ShaderResourceView.FromFile(device, textureFileName);
            }
            catch (Exception ex)
            {
                throw new Exception("textureLoad: " + ex);
            }
        }
        
    }
}
