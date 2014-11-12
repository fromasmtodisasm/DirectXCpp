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
using System.Diagnostics;
using Ch01_01EmptyProject.Graphic.Cameras;

namespace Ch01_01EmptyProject.Graphic
{
    //this class should be a composite, i just have to handle methods which differ
    public class D3DGraphic : IGraphicComposite
    {
        private List<IGraphicComposite> graph = new List<IGraphicComposite>();
        private WindowConfiguration windowConfig;
        private D3D11 d3d;
        private Camera camera;
        private D3DModel model;
        private D3DShader shader;

        public int FPS { get; set; }
        public float FrameTime { get; set; }
        public Inputs.Position Position { get; set; }

        public D3DGraphic(WindowConfiguration windowConfig)
        {
            this.windowConfig = windowConfig;

            ShaderName shaderName = ShaderName.ParallaxMapping;
            IShape shape = new Box2();
            ModelShader.Get(shaderName, shape);

            d3d = new D3D11(windowConfig);
            camera = new Camera();
            model = new D3DModel(d3d.Device, ModelShader.GetModelForRender, ModelShader.GetIndexes);
            shader = new D3DShader(d3d.Device, ModelShader.GetShaderEffect, shaderName);

            graph.Add(d3d);
            graph.Add(model);
            graph.Add(shader);

            camera.Position = new Vector3(-2, 1, -3);
        }

        public void Frame()
        {
            Rotate();

            Render();
            //camera.SetRotation(0, 0, 0); //(float)Math.PI * 20
            // process a graphic with fps or what?
#if DEBUG
            //Debug.WriteLine("Graphic.Frame :: FPS = " + FPS + "and FrameTime = " + FrameTime);
#endif
        }

        static void Rotate()
        {
            Rotation += (float)Math.PI * 0.015f;
            if (Rotation > 360)
                Rotation -= 360;
        }

        public void Render()
        {
            d3d.Render();

            camera.Render();

            var wwp = ComputeWorldViewProjectionMatrix();

            model.SetDeviceContent(d3d.DeviceContext);
            model.Render();

            shader.CameraPosition = camera.Position;

            shader.SetShaderParameters(d3d.DeviceContext, wwp, ModelShader.GetIndexCount);
            shader.Render();

            d3d.PresentRenderedScene();
        }

        public void Dispose()
        {
            foreach (IGraphicComposite item in graph)
                item.Dispose();
        }

        private Ch01_01EmptyProject.Graphic.Shaders.BufferTypes.WorldViewProj ComputeWorldViewProjectionMatrix()
        {
            var worldMatrix = Matrix.Identity;
            var projectionMatrix = Matrix.Identity;
            var viewMatrix = Matrix.Identity;

            Matrix.RotationY(Rotation, out worldMatrix);
            viewMatrix = camera.ViewMatrix;
            // Setup and create the projection matrix.
            projectionMatrix = Matrix.PerspectiveFovLH((float)(Math.PI / 4), (float)windowConfig.Width / (float)windowConfig.Height, 1.0f, 1000.0f);

            var WWPComputed = new Ch01_01EmptyProject.Graphic.Shaders.BufferTypes.WorldViewProj();
            WWPComputed.worldMatrix = worldMatrix;

            WWPComputed.worldViewProj = worldMatrix * viewMatrix * projectionMatrix;

            WWPComputed.worldInverseTranspose = Matrix.Transpose(Matrix.Invert(WWPComputed.worldMatrix));

            return WWPComputed;
        }

        public static float Rotation { get; set; }
    }
}
