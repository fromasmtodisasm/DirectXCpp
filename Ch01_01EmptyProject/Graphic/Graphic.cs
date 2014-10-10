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
using Ch01_01EmptyProject;
using System.Windows.Forms;
using System.Threading;

namespace Ch01_01EmptyProject
{
    public class Graphic : IGraphic, IDisposable
    {
        private D3D d3d;
        private Shader shader;
        private Model model;
        private Camera camera;
        public Graphic(WindowConfiguration windowConfig)
        {
            try
            {
                d3d = new D3D(windowConfig);
                camera = new Camera();
                model = new Model(d3d.Device);
                shader = new Shader(d3d.Device);
            }

            catch (Exception ex)
            {
                throw new Exception("Could not initialize Direct3D: " + ex.Message);
            }
        }

        public void Frame()
        {
            Render();
        }

        public void Render()
        {
            d3d.BeginScene();
            
            camera.Render(d3d.DeviceContext);

            // Get the world, view, and projection matrices from camera and d3d objects.
            var worldMatrix = d3d.WorldMatrix;
            var viewMatrix = camera.ViewMatrix;
            var projectionMatrix = d3d.ProjectionMatrix;

            var worldViewProj = worldMatrix * viewMatrix * projectionMatrix;

            model.Render(d3d.DeviceContext);
            shader.Render(d3d.DeviceContext, worldViewProj);
            
            d3d.PresentRenderedScene();
        }

        public void Dispose()
        {
            shader.Dispose();
            model.Dispose();
            d3d.Dispose();
        }
    }
}
