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
    }

    public class TextureDefinitions : Dictionary<TextureType, string>
    {
        public TextureDefinitions()
        {
            this.Add(TextureType.Wall, @"D:\Github\DirectXCpp\Ch01_01EmptyProject\Graphic\Shaders\Textures\wall.dds");
            this.Add(TextureType.Seafloor, @"D:\Github\DirectXCpp\Ch01_01EmptyProject\Graphic\Shaders\Textures\seafloor.dds");
            this.Add(TextureType.Dirt, @"D:\Github\DirectXCpp\Ch01_01EmptyProject\Graphic\Shaders\Textures\dirt01.dds"); 
       }

    }
}
