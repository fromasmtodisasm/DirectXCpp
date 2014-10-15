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
using SharpDX.Direct3D;


namespace Ch01_01EmptyProject
{
    class Model
    {
        private Buffer vertexBuffer;
        private Vertex[] vertices;
        private int[] indices;
        private Buffer indicesBuffer;

        public int IndexCount
        {
            get;
            private set;
        }

        public Model(Device device)
        {
            try
            {
                vertices = CreateVertices();
                indices = CreateIndices();

                IndexCount = indices.Length;

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
                int stride = Utilities.SizeOf<Vertex>();
                int offset = 0;

                //bound vertex buffer to an input slot of the device, in order to feed the vertices to the pipeline output
                //AND SET THEM AS ACTIVE
                deviceContext.InputAssembler.SetVertexBuffers(firstSlot, new VertexBufferBinding(vertexBuffer, stride, offset));

                deviceContext.InputAssembler.SetIndexBuffer(indicesBuffer, Format.R32_UInt, 0);

                deviceContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
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
            return new int[]
            {
                //frontFace
                0, 1, 2 ,
                0, 2, 3, 
                //backFace
                4, 6, 5,
                4, 1, 0,
                //leftFace
                4, 5, 1,
                4, 1, 0,
                //rightFace
                3, 2, 6,
                3, 6, 7,
                //topFace
                1, 5, 6,
                1, 6, 2,
                //bottomFace
                4, 0, 3,
                4, 3, 7,
            };
        }

        private Vertex[] CreateVertices()
        {
            return new Vertex[]
            {
                 new Vertex(){Position = new Vector3(-1, -1, -1),Color = (Vector4)Color.White},
                 new Vertex(){Position = new Vector3(-1, 1, -1), Color = (Vector4)Color.Black},
                 new Vertex(){Position = new Vector3(+1, +1, -1), Color = (Vector4)Color.Red},
                 new Vertex(){Position = new Vector3(+1, -1, -1), Color = (Vector4)Color.Green},
                 new Vertex(){Position = new Vector3(-1, -1, +1), Color = (Vector4)Color.Blue},
                 new Vertex(){Position = new Vector3(-1, +1, +1), Color = (Vector4)Color.Yellow},
                 new Vertex(){Position = new Vector3(+1, +1, +1), Color = (Vector4)Color.Cyan},
                 new Vertex(){Position = new Vector3(+1, -1, +1), Color = (Vector4)Color.Magenta},
            };
        }
    }
}
