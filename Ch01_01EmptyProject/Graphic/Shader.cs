using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using Device = SharpDX.Direct3D11.Device;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace Ch01_01EmptyProject.Graphic
{
    ///// <summary>
    ///// Describes a custom vertex format structure that contains position, normal, texture and tangent information.
    ///// </summary>
    //[StructLayout(LayoutKind.Sequential)]
    //public struct VertexPositionNormalTangentTexture : IEquatable<VertexPositionNormalTangentTexture>
    //{
    //    /// <summary>
    //    /// Initializes a new VertexPositionNormalTangentTexture instance.
    //    /// </summary>
    //    /// <param name="position">The position of this vertex.</param>
    //    /// <param name="normal">The vertex normal.</param>
    //    /// <param name="tangent">The vertex tangent.</param>
    //    /// <param name="textureCoordinate">UV texture coordinates.</param>
    //    public VertexPositionNormalTangentTexture(Vector3 position, Vector3 normal, Vector3 tangent, Vector2 textureCoordinate)
    //        : this()
    //    {
    //        Position = position;
    //        Normal = normal;
    //        Tangent = tangent;
    //        TextureCoordinate = textureCoordinate;
    //    }

    //    /// <summary>
    //    /// Initializes a new VertexPositionNormalTextureTangent instance.
    //    /// </summary>
    //    /// <param name="px"></param>
    //    /// <param name="py"></param>
    //    /// <param name="pz"></param>
    //    /// <param name="nx"></param>
    //    /// <param name="ny"></param>
    //    /// <param name="nz"></param>
    //    /// <param name="tx"></param>
    //    /// <param name="ty"></param>
    //    /// <param name="tz"></param>
    //    /// <param name="u"></param>
    //    /// <param name="v"></param>
    //    public VertexPositionNormalTangentTexture(float px, float py, float pz, float nx, float ny, float nz, float tx, float ty, float tz, float u, float v)
    //    {
    //        Position = new Vector3(px, py, pz);
    //        Normal = new Vector3(nx, ny, nz);
    //        Tangent = new Vector3(tx, ty, tz);
    //        TextureCoordinate = new Vector2(u, v);
    //    }

    //    /// <summary>
    //    /// XYZ position.
    //    /// </summary>
    //    [VertexElement("SV_Position")]
    //    public Vector3 Position;

    //    /// <summary>
    //    /// The vertex normal.
    //    /// </summary>
    //    [VertexElement("NORMAL")]
    //    public Vector3 Normal;

    //    /// <summary>
    //    /// The vertex tangent.
    //    /// </summary>
    //    [VertexElement("TANGENT")]
    //    public Vector3 Tangent;

    //    /// <summary>
    //    /// UV texture coordinates.
    //    /// </summary>
    //    [VertexElement("TEXCOORD0")]
    //    public Vector2 TextureCoordinate;


    //    /// <summary>
    //    /// Defines structure byte size.
    //    /// </summary>
    //    public static readonly int Size = 44;

    //    public bool Equals(VertexPositionNormalTangentTexture other)
    //    {
    //        return Position.Equals(other.Position) && Normal.Equals(other.Normal) && TextureCoordinate.Equals(other.TextureCoordinate) && Tangent.Equals(other.Tangent);
    //    }

    //    public override bool Equals(object obj)
    //    {
    //        if (ReferenceEquals(null, obj)) return false;
    //        return obj is VertexPositionNormalTangentTexture && Equals((VertexPositionNormalTangentTexture)obj);
    //    }

    //    public override int GetHashCode()
    //    {
    //        unchecked
    //        {
    //            int hashCode = Position.GetHashCode();
    //            hashCode = (hashCode * 397) ^ Normal.GetHashCode();
    //            hashCode = (hashCode * 397) ^ TextureCoordinate.GetHashCode();
    //            hashCode = (hashCode * 397) ^ Tangent.GetHashCode();
    //            return hashCode;
    //        }
    //    }

    //    public static bool operator ==(VertexPositionNormalTangentTexture left, VertexPositionNormalTangentTexture right)
    //    {
    //        return left.Equals(right);
    //    }

    //    public static bool operator !=(VertexPositionNormalTangentTexture left, VertexPositionNormalTangentTexture right)
    //    {
    //        return !left.Equals(right);
    //    }

    //    public override string ToString()
    //    {
    //        return string.Format("Position: {0}, Normal: {1}, Texcoord: {2}, Tangent: {3}", Position, Normal, TextureCoordinate, Tangent);
    //    }
    //}






    public class Shader : IDisposable
    {
        private Buffer vertexBuffer;
        private Vertex[] vertices;
        private Buffer indicesBuffer;
        private int[] indices;
        //Vertex VSMain(Vertex input)
        //{
        //    return output;
        //}

        public Shader(Device device)
        {

            InputElement[] inputElementsDesc = SpecifyInputLayoutDescriptionForVertex();

            string SpriteFX = @"Texture2D SpriteTex;

struct VertexIn {
float3 PosNdc : POSITION;
float2 Tex : TEXCOORD;
float4 Color : COLOR;
};
struct VertexOut {
float4 PosNdc : SV_POSITION;
float2 Tex : TEXCOORD;
float4 Color : COLOR;
};
";
            //byte[] shaderByteCode = ShaderByteCode.;

            var il = new InputLayout(device, shaderByteCode, inputElementsDesc);
          
            vertices = new Vertex[]
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

            //BufferDescription bufferDescription;
            //bufferDescription.Usage = ResourceUsage.Immutable;
            //bufferDescription.SizeInBytes = sizeof(vertices) * 8;

            vertexBuffer = Buffer.Create(device, BindFlags.VertexBuffer, vertices);



            indices = new int[24]
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

            indicesBuffer = Buffer.Create(device, BindFlags.IndexBuffer, indices);


                //"Simple.hlsl", "VSMain", "vs_5_0", shaderFlags)
           //var vertexShaderBytecode = ShaderBytecode.Compile()
            //var inputLayoutDescriptionForVertex = SpecifyInputLayoutDescriptionForVertex();

            //vertexShader = new VertexShader(device, vertexShaderBytecode);



        }

        internal void Render(DeviceContext deviceContext)
        {
            int firstSlot = 0;
            int numBuffers = 1;

            Buffer[] vertexBuffers = new Buffer[]{vertexBuffer};
            int[] stride = new int[] {Utilities.SizeOf<Vertex>()};
            int[] offset = new int[] { 0 };

            //var vertexBufferBinding = new VertexBufferBinding(vertexBuffer, Utilities.SizeOf<Vertex>(), 0);

            //bound vertex buffer to an input slot of the device, in order to feed the vertices to the pipeline output
            deviceContext.InputAssembler.SetVertexBuffers(firstSlot, vertexBuffers, stride, offset);
            deviceContext.InputAssembler.SetIndexBuffer(indicesBuffer, Format.R32_UInt, 0);
            
            deviceContext.InputAssembler.PrimitiveTopology = deviceContext.InputAssembler.PrimitiveTopology;
            
            deviceContext.Draw(vertices.Length, 0);
            deviceContext.DrawIndexed(indices.Length, 0, 0);

        }

        private static InputElement[] SpecifyInputLayoutDescriptionForVertex()
        {
            var vertexDescription = new InputElement[]
            {
            new InputElement()
            {
            SemanticName = "POSITION",
            SemanticIndex = 0,
            Format = Format.R32G32B32_Float,
            Slot = 0,
            AlignedByteOffset = 0,
            Classification = InputClassification.PerVertexData,
            InstanceDataStepRate = 0,
            },
          new InputElement()
          {
            SemanticName = "NORMAL",
            SemanticIndex = 0,
            Format = Format.R32G32B32_Float,
            Slot = 0,
            AlignedByteOffset = 12,
            Classification = InputClassification.PerVertexData,
            InstanceDataStepRate = 0,
          },
            new InputElement()
            {
            SemanticName = "TEXCOORD",
            SemanticIndex = 0,
            Format = Format.R32G32B32_Float,
            Slot = 0,
            AlignedByteOffset = 24,
            Classification = InputClassification.PerVertexData,
            InstanceDataStepRate = 0,
            },
            new InputElement()
            {
            SemanticName = "TEXCOORD",
            SemanticIndex = 0,
            Format = Format.R32G32B32_Float,
            Slot = 0,
            AlignedByteOffset = 32,
            Classification = InputClassification.PerVertexData,
            InstanceDataStepRate = 0,
            }
        };
            return vertexDescription;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
