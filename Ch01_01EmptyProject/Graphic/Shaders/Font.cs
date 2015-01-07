using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch01_01EmptyProject.Graphic.Shaders
{
    class Font
    {
        public struct Character
        {
            public float left;
            public float right;
            public int size;

            public Character(string fontData)
            {
                var data = fontData.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                left = float.Parse(data[data.Length - 3]);
                right = float.Parse(data[data.Length - 2]);
                size = int.Parse(data[data.Length - 1]);
            }
        }

        public Font(Device device, string fontFileName, string textureFileName)
        {
            LoadFontData(fontFileName);
            LoadTexture(device, textureFileName);
        }

        public List<Character> FontCharacters { get; private set; }
        public Texture Texture { get; private set; }

        private void LoadTexture(Device device, string textureFileName)
        {
            textureFileName = SystemConfiguration.DataFilePath + textureFileName;
            Texture = new Texture(device, textureFileName);
        }

        private void LoadFontData(string fontFileName)
        {
            try
            {
                fontFileName = SystemConfiguration.ModelFilePath + fontFileName;
                var fontDataLines = File.ReadAllLines(fontFileName);

                FontCharacters = new List<Character>();
                foreach (var line in fontDataLines)
                {
                    FontCharacters.Add(new Character(line));
                }
            }
            catch (Exception ex)
            {
                
                throw new Exception("Font data load error: " + ex.Message);
            }
        }
    }
}
