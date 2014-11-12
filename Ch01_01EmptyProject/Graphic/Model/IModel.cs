using Ch01_01EmptyProject.Graphic.Shaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch01_01EmptyProject.Graphic
{
    interface IModel
    {
        //LoadTexture(TextureType texture);
        void LoadModel(string ModelName);
        
    }
}
