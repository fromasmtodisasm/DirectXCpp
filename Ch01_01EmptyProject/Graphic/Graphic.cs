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
using System.Windows.Forms;

namespace Ch01_01EmptyProject.Graphic
{
    public class Graphic : IGraphic, IDisposable
    {
        private D3D d3d;
        private Shader shader;
        private Model model;
        public Graphic(WindowConfiguration windowConfig)
        {
            try
            {
                d3d = new D3D(windowConfig);
                model = new Model(d3d.Device);
                shader = new Shader(d3d.Device);
            }
            
            catch (Exception ex)
            {
                MessageBox.Show("Could not initialize Direct3D\n'" + ex.Message + "'");
                //throw new Exception("Could not initialize Direct3D: "+ ex.Message);
            }
        }

        public void Frame()
        {
            Render();
        }

        public void Render()
        {
            d3d.BeginScene();
            model.Render(d3d.DeviceContext);
            //shader.Render(d3d.DeviceContext);
            //d3d.PresentRenderedScene();
        }

        public void Dispose()
        {
            shader.Dispose();
            model.Dispose();
            d3d.Dispose();
        }
    }
}
