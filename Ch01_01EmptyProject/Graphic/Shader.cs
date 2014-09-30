using SharpDX.Direct3D11;
using SharpDX.DXGI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch01_01EmptyProject.Graphic
{
    public class Shader : IDisposable
    {
        public Shader()
        {
            SpecifyInputLayoutDescriptionForVertex();
        }

        internal void Render()
        {
            throw new NotImplementedException();
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

            //var inputLayoutDescriptionForVertex = SpecifyInputLayoutDescriptionForVertex();
            //  var vertexInputSignature = new ShaderBytecode();
            //  var il = new InputLayout(device, );
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
