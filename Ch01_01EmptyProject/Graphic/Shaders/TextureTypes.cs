using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch01_01EmptyProject.Graphic.Shaders
{
    public enum TextureType
    {
        Wall,
        Seafloor,
        Dirt,
        Wall_NS,
        Stones,
        Stones_NormalMap,
        Wall_HS,
    }

    public class TextureDefinitions : Dictionary<TextureType, string>
    {
        public TextureDefinitions()
        {
            this.Add(TextureType.Wall, @"D:\Github\DirectXCpp\Ch01_01EmptyProject\Graphic\Shaders\Textures\wall.dds");
            this.Add(TextureType.Seafloor, @"D:\Github\DirectXCpp\Ch01_01EmptyProject\Graphic\Shaders\Textures\seafloor.dds");
            this.Add(TextureType.Dirt, @"D:\Github\DirectXCpp\Ch01_01EmptyProject\Graphic\Shaders\Textures\dirt01.dds");

            this.Add(TextureType.Wall_NS, @"D:\Github\DirectXCpp\Ch01_01EmptyProject\Graphic\Shaders\Textures\wall_ns.dds");
            this.Add(TextureType.Wall_HS, @"D:\Github\DirectXCpp\Ch01_01EmptyProject\Graphic\Shaders\Textures\wall_hm.dds");

            this.Add(TextureType.Stones, @"D:\Github\DirectXCpp\Ch01_01EmptyProject\Graphic\Shaders\Textures\stone01.dds");
            this.Add(TextureType.Stones_NormalMap, @"D:\Github\DirectXCpp\Ch01_01EmptyProject\Graphic\Shaders\Textures\bump01.dds");

        }

    }
}
