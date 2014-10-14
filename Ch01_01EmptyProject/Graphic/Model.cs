using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Device = SharpDX.Direct3D11.Device;
using Buffer = SharpDX.Direct3D11.Buffer;
using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.DXGI;

namespace Ch01_01EmptyProject
{
    class Model : IDisposable, IRenderable
    {
        private Buffer vertexBuffer;
        private Vertex[] vertices;
        private int[] indices;
        private Buffer indicesBuffer;

        public Model(Device device)
        {
            try
            {
                vertices = CreateVertices();
                indices = CreateIndices();

                vertexBuffer = Buffer.Create(device, BindFlags.VertexBuffer, vertices);
                indicesBuffer = Buffer.Create(device, BindFlags.IndexBuffer, indices);
            }
            catch (Exception ex)
            {
                throw new Exception("Model failed when tried to create buffers: " + ex.Message);
            }
        }

        public void Render(DeviceContext deviceContext)
        {
            try
            {
                int firstSlot = 0;
                Buffer[] vertexBuffers = new Buffer[] { vertexBuffer };
                int[] stride = new int[] { Utilities.SizeOf<Vertex>() };
                int[] offset = new int[] { 0 };

                //bound vertex buffer to an input slot of the device, in order to feed the vertices to the pipeline output
                //AND SET THEM AS ACTIVE
                deviceContext.InputAssembler.SetVertexBuffers(firstSlot, vertexBuffers, stride, offset);
                deviceContext.InputAssembler.SetIndexBuffer(indicesBuffer, Format.R32_UInt, 0);

                deviceContext.InputAssembler.PrimitiveTopology = deviceContext.InputAssembler.PrimitiveTopology;
            }
            catch (Exception ex)
            {
                throw new Exception("D3D11 failed to render model: " + ex);
            }
        }

        public void Dispose()
        {
            vertexBuffer.Dispose();
            indicesBuffer.Dispose();
        }

        private int[] CreateIndices()
        {
            return new int[24]
            {
                0, 1, 2 ,//Triangle 0
                0, 2, 3, //Triangle 1
                0, 3, 4,//Triangle 2
                0, 4, 5,//Triangle 3
                0, 5, 6,//Triangle 4
                0, 6, 7,//Triangle 5
                0, 7, 8,//Triangle 6
                0, 8, 1//Triangle 7
            };
        }

        private Vertex[] CreateVertices()
        {
            return new Vertex[]
            {
                 new Vertex(){Position = new Vector3(-10, -10, -10),Color = (Vector4)Color.White},
                 new Vertex(){Position = new Vector3(-10, 10, -10), Color = (Vector4)Color.Black},
                 new Vertex(){Position = new Vector3(+10, +10, -10), Color = (Vector4)Color.Red},
                 new Vertex(){Position = new Vector3(+10, -10, -10), Color = (Vector4)Color.Green},
                 new Vertex(){Position = new Vector3(-10, -10, +10), Color = (Vector4)Color.Blue},
                 new Vertex(){Position = new Vector3(-10, +10, +10), Color = (Vector4)Color.Yellow},
                 new Vertex(){Position = new Vector3(+10, +10, +10), Color = (Vector4)Color.Cyan},
                 new Vertex(){Position = new Vector3(+10, -10, +10), Color = (Vector4)Color.Magenta},
            };
        }
    }
}
