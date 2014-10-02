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

//using Effect = SharpDX.Direct3D11.Effect;
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


    public class Shader : IDisposable, IRenderable
    {
        private Buffer vertexBuffer;
        private Vertex[] vertices;
        private Buffer indicesBuffer;
        private int[] indices;
        private CompilationResult vertexShaderBytecode;
        private CompilationResult pixelShaderBytecode;
        private InputLayout inputLayout;
        private VertexShader vertexShader;

        public Shader(Device device)
        {
            string vertexShaderFileName = @"C:\Users\jamesss\Documents\GitHub\DirectXCpp\Ch01_01EmptyProject\Graphic\Shaders\VertexShader.fx";
            //string vertexShaderFileName = @"./Shaders/VertexShader.fx";

            vertexShaderBytecode = GetCompiledShader(vertexShaderFileName);

            //effect = new Effect(device, vertexShaderBytecode.Bytecode.Data, EffectFlags.None);

            //vertexShader = new VertexShader(device, vertexShaderBytecode);

            //var inputElementDesc = SpecifyInputLayoutDescriptionForVertex();

            //CreateInputLayout(device, inputElementDesc);

            //vertexShaderBytecode.Dispose();
        }

        public CompilationResult GetCompiledShader(string vertexShaderFileName)
        {
            try
            {
                var vertexShaderBytes = ShaderBytecode.CompileFromFile(vertexShaderFileName, "fx_5_0", ShaderFlags.None);
                return vertexShaderBytes;
            }
            catch (Exception ex)
            {
                throw new Exception("Vertex shader compile failed :" + ex.Message);
            }
        }

        public void Render(DeviceContext deviceContext)
        {
            try
            {
                deviceContext.InputAssembler.InputLayout = inputLayout;
                deviceContext.VertexShader.Set(vertexShader);

                //deviceContext.Draw(vertices.Length, 0);
                deviceContext.DrawIndexed(indices.Length, 0, 0);
            }
            catch (Exception ex)
            {

                throw new Exception("D3D failed to render shaders" + ex);
            }
        }

        private void CreateInputLayout(Device device, InputElement[] inputElementDesc)
        {
            try
            {
                inputLayout = new InputLayout(device, vertexShaderBytecode, inputElementDesc);
            }
            catch (Exception ex)
            {

                throw new Exception("D3D could not create input Layout: " + ex);
            }
        }

        // Now setup the layout of the data that goes into the shader.
        // It needs to match the VertexType structure in the Model and in the shader.
        private static InputElement[] SpecifyInputLayoutDescriptionForVertex()
        {
            try
            {
                return new InputElement[]
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
            SemanticName = "COLOR",
            SemanticIndex = 0,
            Format = Format.R32G32B32_Float,
            Slot = 0,
            AlignedByteOffset = 0,
            Classification = InputClassification.PerVertexData,
            InstanceDataStepRate = 0,
            }
          //new InputElement()
          //{
          //  SemanticName = "NORMAL",
          //  SemanticIndex = 0,
          //  Format = Format.R32G32B32_Float,
          //  Slot = 0,
          //  AlignedByteOffset = 12,
          //  Classification = InputClassification.PerVertexData,
          //  InstanceDataStepRate = 0,
          //},
          //  new InputElement()
          //  {
          //  SemanticName = "TEXCOORD",
          //  SemanticIndex = 0,
          //  Format = Format.R32G32B32_Float,
          //  Slot = 0,
          //  AlignedByteOffset = 24,
          //  Classification = InputClassification.PerVertexData,
          //  InstanceDataStepRate = 0,
          //  },
          //  new InputElement()
          //  {
          //  SemanticName = "TEXCOORD",
          //  SemanticIndex = 0,
          //  Format = Format.R32G32B32_Float,
          //  Slot = 0,
          //  AlignedByteOffset = 32,
          //  Classification = InputClassification.PerVertexData,
          //  InstanceDataStepRate = 0,
          //  }
        };
            }
            catch (Exception ex)
            {
                throw new Exception("D3D couldnt create input elements" + ex);
            }

        }

        public void Dispose()
        {
            inputLayout.Dispose();
            vertexShader.Dispose();
        }
    }
}
