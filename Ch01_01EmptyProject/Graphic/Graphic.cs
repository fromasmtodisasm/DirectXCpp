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
using Ch01_01EmptyProject.Graphic.Shaders;

namespace Ch01_01EmptyProject.Graphic
{
    public class Graphic : IGraphic, IDisposable
    {
        private D3D11 d3d;
        private Shader shader;
        private Model model;
        private Camera camera;
        private WindowConfiguration windowConfig;
        
        public Graphic(WindowConfiguration windowConfig)
        {
            //try
            //{
                this.windowConfig = windowConfig;

                d3d = new D3D11(windowConfig);
                camera = new Camera();
                model = new Model(d3d.Device);
                shader = new Shader(d3d.Device);
            //}

            //catch (Exception ex)
            //{
            //    throw new Exception("Could not initialize Direct3D: " + ex.Message);
            //}
        }

        public void Frame()
        {
            Render();
        }

        public void Render()
        {
            d3d.DrawScene();

            camera.Render(d3d.DeviceContext);

            //WORLD VIEW PROJECTION COMPUATION
            //Initialize matrixes
            var worldMatrix = Matrix.Identity;

            var viewMatrix = camera.ViewMatrix;

            // Setup and create the projection matrix.
            var projectionMatrix = Matrix.PerspectiveFovLH((float)(Math.PI / 4), (float)(windowConfig.Width) / windowConfig.Height, 0.1f, 1000.0f);
            var wwp = new Shader.WorldViewProj();
            wwp.projectionMatrix = projectionMatrix;
            wwp.viewMatrix = viewMatrix;
            wwp.worldMatrix = worldMatrix;
            
            
            model.Render(d3d.DeviceContext);

            //var constantMatrixBuffer = shader.SetWorldViewMatrix(worldMatrix, viewMatrix, projectionMatrix);
            shader.Render(d3d.DeviceContext, wwp, model.IndexCount);
    
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
