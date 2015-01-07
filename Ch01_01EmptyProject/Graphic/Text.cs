using Ch01_01EmptyProject.Graphic.Shaders;
using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch01_01EmptyProject.Graphic
{
    class Text
    {
        public Text(Device device)
        {
          var font = new Font(device, "fontdata.txt", "font.dds");

          var fontShader = new FontShader();       
        
        }
    
    }
}
