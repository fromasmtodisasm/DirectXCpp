using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D11;
using SharpDX.Direct3D;
using SharpDX.DXGI;
using SharpDX.Windows;

using Device = SharpDX.Direct3D11.Device;
using Buffer = SharpDX.Direct3D11.Buffer;
using Ch01_01EmptyProject.Graphic.Structures;

// Q1? - ShaderQ should be divided to renderer, shaderBase and concrete shaders (example : MW)

//Q2 - using a cPerObject and cPerFrame notation !
// reorg classes - better structure (examle: sharpdx tutorail)

namespace Ch01_01EmptyProject.Graphic.Shaders
{
    public class Bitmap : IGraphicComposite
    {
        private Buffer constantMatrixBuffer;
        private Buffer constantLightBuffer;

        private InputLayout inputLayout;
        private VertexShader vertexShader;

        private Device device;

        private PixelShader pixelShader;

        private CompilationResult vertexShaderByteCode;
        private CompilationResult pixelShaderByteCode;

        private int indexCount;
        private DeviceContext deviceContext;
        private SamplerState sampleState;
        private ShaderResourceView textureResource;

        private DataStream mappedResource;
        private ShaderName shader;
        private Buffer constantCameraBuffer;
        private Textures textures;
        private int screenHeight;
        private int screenWidth;
        private float bitmapWidth;
        private float bitmapHeight;
        private Vector2 previousLocation;
        private Buffer vertexBuffer;
        private Buffer indexBuffer;

        public Vector3 CameraPosition { get; set; }
        public ShaderResourceView[] TextureCollection { get; private set; }

        public Bitmap(Device device, IShaderEffect shaderEffect, ShaderName shader, SystemConfiguration winCfg, Vector2 bitmapDimensions)
        {
            screenHeight = winCfg.Height;
            screenWidth = winCfg.Width;
            bitmapWidth = bitmapDimensions.X;
            bitmapHeight = bitmapDimensions.Y;

            previousLocation.X = -1;
            previousLocation.Y = -1;

            this.device = device;

            InitializeBuffers();

            //loadTexture
            //new D3DShader();
        }

        private void InitializeBuffers()
        {
            vertexCount = 6;
            indexCount = 6;

           var vertices = new TextureVertex[vertexCount];

           foreach (var vertex in vertices)
           {
              //set to zero
           }

           int[] indices = new int[indexCount];
            for (int i = 0; i < indexCount; i++)
            {
                indices[i] = i;
            }

            var vertexBufferDesc = new BufferDescription()
            {
                Usage = ResourceUsage.Dynamic,
                //or size of constant buffer
                SizeInBytes = Utilities.SizeOf<TextureVertex>() * vertexCount,
                BindFlags = BindFlags.VertexBuffer,
                CpuAccessFlags = CpuAccessFlags.Write,
                OptionFlags = ResourceOptionFlags.None,
                StructureByteStride = 0
            };

            // Create the vertex buffer.
            vertexBuffer = Buffer.Create(device, vertices, vertexBufferDesc);

            // Create the index buffer.
             indexBuffer = Buffer.Create(device, BindFlags.IndexBuffer, indices);
        }

        private void UpdateBuffers(DeviceContext deviceContext, Vector2 positions)
        {
            if (PositionsDoesntChanged(ref positions))
                return;

            positions = UpdatePositions(positions);

            //calculate screen coordinates of the bitmap
            var left = (-(screenWidth >> 2)) + (float)positions.X;
            var right = left + bitmapWidth;
            var top = (screenWidth >> 2) - (float)positions.Y;
            var bottom = top - bitmapHeight;

            var vertices = new[]
            {
                   new TextureVertex()
                {
                    Position = new Vector3(left, top, 0),
                    Texture  = new Vector2(0, 0)
                },
                new TextureVertex()
                {
                    Position = new Vector3(right, bottom, 0),
                    Texture  = new Vector2(1, 1)
                },
                  new TextureVertex()
                {
                    Position = new Vector3(left, bottom, 0),
                    Texture  = new Vector2(0, 1)
                },
                new TextureVertex()
                {
                    Position = new Vector3(left, top, 0),
                    Texture  = new Vector2(0, 0)
                },
                 new TextureVertex()
                {
                    Position = new Vector3(right, top, 0),
                    Texture  = new Vector2(1, 0)
                },
                 new TextureVertex()
                {
                    Position = new Vector3(right, bottom, 0),
                    Texture  = new Vector2(1, 1)
                },
        };

            DataStream mappedResource;
            deviceContext.MapSubresource(vertexBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource);
            mappedResource.WriteRange<TextureVertex>(vertices);
            deviceContext.UnmapSubresource(vertexBuffer, 0);
        }
        private Vector2 UpdatePositions(Vector2 positions)
        {
            previousLocation.X = positions.X;
            previousLocation.Y = positions.Y;
            return positions;
        }

        private bool PositionsDoesntChanged(ref Vector2 positions)
        {
            return positions.X == previousLocation.X && positions.Y == previousLocation.Y;
        }
        public void WriteToSubresource<T>(Buffer constantBuffer, T writeTo) where T : struct
        {
            deviceContext.MapSubresource(constantBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource);

            mappedResource.Write(writeTo);

            // Unlock the constant buffer.
            deviceContext.UnmapSubresource(constantBuffer, 0);
        }

        public void SetShaderParameters(DeviceContext deviceContext, BufferTypes.WorldViewProj WWPComputed, int indexCount)
        {
            throw new NotImplementedException();
        }
     
        public void Render(DeviceContext deviceContext, Vector2 positions)
        {
            UpdateBuffers(deviceContext, positions);
            RenderBuffers(deviceContext);
        }

        private void RenderBuffers(DeviceContext deviceContext)
        {
            var stride = Utilities.SizeOf<TextureVertex>();
            var offset = 0;

            deviceContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(vertexBuffer, stride, offset));
            // Set the index buffer to active in the input assembler so it can be rendered.
            deviceContext.InputAssembler.SetIndexBuffer(indexBuffer, Format.R32_UInt, 0);
            // Set the type of the primitive that should be rendered from this vertex buffer, in this case triangles.
            deviceContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
        }

        
        public void Dispose()
        {
            if (constantLightBuffer != null)
            {
                constantLightBuffer.Dispose();
            }
            if (constantCameraBuffer != null)
            {
                constantCameraBuffer.Dispose();
            }
            if (textureResource != null)
            {
                textureResource.Dispose();
            }

            if (constantMatrixBuffer != null)
            {
                constantMatrixBuffer.Dispose();
            }

            sampleState.Dispose();

            inputLayout.Dispose();
            pixelShader.Dispose();
            vertexShader.Dispose();
        }
        public int vertexCount { get; set; }

        public void Render()
        {
            Render(deviceContext, new Vector2(100, 100));
        }

        internal void SetDeviceContent(DeviceContext deviceContext)
        {
            this.deviceContext = deviceContext;
        }
    }
}
