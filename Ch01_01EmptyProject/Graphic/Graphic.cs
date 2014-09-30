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
using Ch01_01EmptyProject.System;

namespace Ch01_01EmptyProject.Graphic
{
    public class Graphic : IGraphic, IDisposable
    {
        private D3D d3d;
        private Shader shader;
        public Graphic(WindowConfiguration windowConfig)
       {
           try
           {
               d3d = new D3D();
               d3d.Initialize(windowConfig);
               //shader = new Shader();
           }
           catch (Exception e)
           {
               throw new Exception(("Could not initialize Direct3D: " + e.ToString()));
           }
       }

        public void Frame()
        {
            Render();
        }

        public void Render()
        {
            d3d.BeginScene();
            //shader.Render();
            d3d.PresentRenderedScene();
        }

        public void Dispose()
        {
            d3d.Dispose();
            //shader.Dispose();
        }

      
    }
}
