﻿using System;
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

        public D3DGraphic(WindowConfiguration windowConfig)
        {
            this.windowConfig = windowConfig;

            ShaderName shaderName = ShaderName.Multitexture;
            IShape shape = new Box2();
            ModelShader.Get(shaderName, shape);

            d3d = new D3D11(windowConfig);
            camera = new Camera();
            model = new D3DModel(d3d.Device, ModelShader.GetModelForRender, ModelShader.GetIndexes);
            shader = new D3DShader(d3d.Device, ModelShader.GetShaderEffect, shaderName, camera.Position);

            graph.Add(d3d);
            graph.Add(model);
            graph.Add(shader);
        }

        public void Frame()
        {
            //camera.Position = new Vector3(0, 0, -10.0f);
            //camera.SetRotation(0, 0, 0); //(float)Math.PI * 20
            
            // process a graphic with fps or what?
#if DEBUG
            //Debug.WriteLine("Graphic.Frame :: FPS = " + FPS + "and FrameTime = " + FrameTime);
#endif
        }

        public void Render()
        {
            d3d.Render();
            camera.Render();

            var wwp = ComputeWorldViewProjectionMatrix();

            model.SetDeviceContent(d3d.DeviceContext);
            model.Render();

            shader.SetShaderParameters(d3d.DeviceContext, wwp, ModelShader.GetIndexCount);
            shader.Render();

            d3d.PresentRenderedScene();
        }

        public void Dispose()
        {
            foreach (IGraphicComposite item in graph)
                item.Dispose();
        }

       
        private D3DShader.WorldViewProj ComputeWorldViewProjectionMatrix()
        {
            var worldMatrix = Matrix.Identity;
            var viewMatrix = camera.ViewMatrix;
            // Setup and create the projection matrix.
            var projectionMatrix = Matrix.PerspectiveFovLH((float)(Math.PI / 4), (float)(windowConfig.Width) / windowConfig.Height, 0.1f, 1000.0f);

            var wwp = new D3DShader.WorldViewProj();
            wwp.projectionMatrix = projectionMatrix;
            wwp.viewMatrix = viewMatrix;
            wwp.worldMatrix = worldMatrix;
            return wwp;
        }

        public Inputs.Position Position { get; set; }
    }
}
