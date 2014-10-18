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
using Ch01_01EmptyProject.Graphic.Structures;

namespace Ch01_01EmptyProject.Graphic
{
    class D3DModel : IGraphicComposite
    {
        private Buffer vertexBuffer;
        private Buffer indicesBuffer;

        private int[] indices;

        private DeviceContext deviceContext;

        public int IndexCount
        {
            get;
            private set;
        }

        public D3DModel(Device device)
        {
            try
            {
                Vector4[] colors = new Vector4[]
                        {
                        (Vector4)Color.White,
                        (Vector4)Color.Black,
                        (Vector4)Color.Red,
                        (Vector4)Color.Green,
                        (Vector4)Color.Blue,
                        (Vector4)Color.Yellow,
                        (Vector4)Color.Cyan,
                        (Vector4)Color.Magenta,
                        };

                Vector2[] textureCoord = new Vector2[]
                {
                	new Vector2(0, 1),
                   new Vector2(.5f, 0),
                   new Vector2(1, 1),
                };



                var vertexType = VertexType.ColorVertex;
                IShape shape = new Triangle();
                indices = shape.Indexes;

                IVertex[] vertices;
                
                //add to the base shapea additional stuff defined by Vector Structure
                Vector3[] positions = shape.Vertexes;
                //switch (vertexType)
                //{
                //    case VertexType.ColorVertex:
                //        {
                //            ColorVertex[] vertices = new ColorVertex[positions.Length];
                //            //from this array, make coresponding structure
                //            for (int i = 0; i < positions.Length; i++)
                //            {
                //                ColorVertex a = new ColorVertex();
                //                a.Position = positions[i];
                //                a.Color = colors[i];
                //                vertices[i] = a;
                //            }
                //        }
                //        break;
                //    case VertexType.NormalVertex:
                //        break;
                //    case VertexType.TextureVertex:
                //        {
                //            TextureVertex[] vertices = new TextureVertex[positions.Length];
                //            //from this array, make coresponding structure
                //            for (int i = 0; i < positions.Length; i++)
                //            {
                //                var a = new TextureVertex();
                //                a.Position = positions[i];
                //                a.Texture = textureCoord[i];
                //                vertices[i] = a;
                //            }
                           
                //        }
                //        break;
                //    case VertexType.ColorNormalVertex:
                //        break;
                //    default:

                //        break;
                //}



                vertexBufferC<TextureVertex>(device, vertices);
                IndexCount = indices.Length;


                indicesBuffer = Buffer.Create(device, BindFlags.IndexBuffer, indices);
            }
            catch (Exception ex)
            {
                throw new Exception("Model failed when tried to create buffers: " + ex.Message);
            }
        }

        private void CreateVertex<T>(Device device, T[] vertices) where T : struct
        { 
        
        }

        //by adding where part T cant be nullable
        private void vertexBufferC<T>(Device device, T[] vertices) where T : struct
        {
            try
            {
                vertexBuffer = Buffer.Create(device, BindFlags.VertexBuffer, vertices);
            }
            catch (Exception ex)
            {
                throw new Exception("Verrtx Bufdfder: " + ex.Message);
            }
        }

        public void SetDeviceContent(DeviceContext deviceContext)
        {
            this.deviceContext = deviceContext;
        }

        public void Render()
        {
            try
            {
                int firstSlot = 0;
                int stride = Utilities.SizeOf<ColorVertex>();
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
    }
}

