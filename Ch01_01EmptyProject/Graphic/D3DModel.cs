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
using System.Reflection;
using Ch01_01EmptyProject.Graphic.Shaders;

namespace Ch01_01EmptyProject.Graphic
{
  //  public abstract class ShapeDecorator
  //  {
  //      public abstract T[] GetPart<T>();
  //  }
  //  //public abstract class 

  //  public class ColorDecorator : ShapeDecorator
  //  {
  //      private IShape shape;
  //      public ColorDecorator(IShape shape)
  //      {
  //          this.shape = shape;
  //      }

  //      public override T[] GetPart<T>()
  //       {
  //          Vector4[] colors = new Vector4[]
  //                      {
  //                      (Vector4)Color.White,
  //                      (Vector4)Color.Black,
  //                      (Vector4)Color.Red,
  //                      (Vector4)Color.Green,
  //                      (Vector4)Color.Blue,
  //                      (Vector4)Color.Yellow,
  //                      (Vector4)Color.Cyan,
  //                      (Vector4)Color.Magenta,
  //                      };
  //                  ColorVertex[] vertices = new ColorVertex[positions.Length];
  //          //from this array, make coresponding structure
  //                          for (int i = 0; i < shape.Vertexes.Length; i++)
  //                          {
  //                              ColorVertex a = new ColorVertex();
  //                              a.Position = shape.Vertexes[i];
  //                              a.Color = colors[i];
  //                              vertices[i] = a;
  //                          }

  //          shape.Indexes
  //          return colors;
  //    }
  //}
    class D3DModel : IGraphicComposite
    {
        private Buffer vertexBuffer;
        private Buffer indicesBuffer;

        private int[] indices;

        private DeviceContext deviceContext;
        private Device device;
        private object p1;
        private int[] p2;
        private Type vertexype;

        public D3DModel(Device device, object p1, int[] p2)
        {
            try
            {
                indices = p2;//shape.Indexes;
                vertexype = p1.GetType().GetElementType();
                //get method wih reflection
                //it allows me to create a buffer for any type..
                MethodInfo method = typeof(D3DModel).GetMethod("vertexBufferC");
                MethodInfo generic = method.MakeGenericMethod(vertexype);

                generic.Invoke(this, new object[] { device, p1 });

                indicesBuffer = Buffer.Create(device, BindFlags.IndexBuffer, indices);
            }
            catch (Exception ex)
            {
                throw new Exception("Model failed when tried to create buffers: " + ex.Message);
            }
        }

        //by adding where part T cant be nullable
        public void vertexBufferC<T>(Device device, T[] vertices) where T : struct
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

                MethodInfo sizeO = typeof(D3DModel).GetMethod("UtilitiesSizeOf");
                MethodInfo generic = sizeO.MakeGenericMethod(vertexype);
                int stride = (int)generic.Invoke(this, null);

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

        public int UtilitiesSizeOf<T>() where T : struct
        { 
            //this wrapper is here because of reflection invoke
            return Utilities.SizeOf<T>();
        }

        public void Dispose()
        {
            vertexBuffer.Dispose();
            indicesBuffer.Dispose();
        }
    }
}

