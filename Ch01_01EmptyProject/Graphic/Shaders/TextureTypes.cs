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
        TestNormalMap,
        TestHeightMap,
        TestColorMap,
        GenericRockNormalHeight,
        GenericRockColor,
        Stones_HeightMap,
    }

    public class TextureDefinitions : Dictionary<TextureType, string>
    {
        string path = @"D:\Github\DirectXCpp\Ch01_01EmptyProject\Graphic\Textures\";

        public TextureDefinitions()
        {
            this.Add(TextureType.Wall, path + "wall.dds");
            this.Add(TextureType.Seafloor, path + "seafloor.dds");
            this.Add(TextureType.Dirt, path + "dirt01.dds");

            this.Add(TextureType.Wall_NS, path + "wall_ns.dds");
            this.Add(TextureType.Wall_HS, path + "wall_hm.dds");

            this.Add(TextureType.Stones, path + "stone01.dds");
            this.Add(TextureType.Stones_NormalMap, path + "bump01.dds");
            this.Add(TextureType.Stones_HeightMap, path + "bump01_hm.dds");

            this.Add(TextureType.TestColorMap, path + "color_map.jpg");
            this.Add(TextureType.TestHeightMap, path + "height_map.jpg");
            this.Add(TextureType.TestNormalMap, path + "normal_map.jpg");

            this.Add(TextureType.GenericRockNormalHeight, path + "rock_normalmap.dds");
            this.Add(TextureType.GenericRockColor, path + "rock_colormap.dds");

        }

    }
}
