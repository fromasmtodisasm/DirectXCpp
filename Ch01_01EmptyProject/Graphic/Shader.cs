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

using Effect = SharpDX.Direct3D11.Effect;

using Device = SharpDX.Direct3D11.Device;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace Ch01_01EmptyProject
{
    public class Shader : IDisposable//, IRenderable
    {
        private Buffer vertexBuffer;
        private Vertex[] vertices;
        private Buffer indicesBuffer;
        private int[] indices;

        private InputLayout inputLayout;
        private VertexShader vertexShader;
        private Effect effect;
        private Device device;
        private PixelShader pixelShader;
        private CompilationResult vertexShaderByteCode;
        private CompilationResult pixelShaderByteCode;
        private Buffer constantMatrixBuffer;
        private EffectMatrixVariable fxWorldViewProjection;

        struct SHADER_GLOBALS
        {
            Matrix worldViewProjection;
            public SHADER_GLOBALS(Matrix worldViewProjection)
            {
                this.worldViewProjection = worldViewProjection;
            }
        }

        public Shader(Device device)
        {
            this.device = device;

            string vertexShaderFileName = @"Graphic\Shaders\VertexShader.fx";
        
            vertexShaderByteCode = ShaderBytecode.CompileFromFile(vertexShaderFileName, "VS", "vs_5_0");
            pixelShaderByteCode = ShaderBytecode.CompileFromFile(vertexShaderFileName, "PS", "ps_5_0");

            vertexShader = new VertexShader(device, vertexShaderByteCode);
            pixelShader = new PixelShader(device, pixelShaderByteCode);

            //vertexShaderByteCode = GetCompiledShader(vertexShaderFileName);
            //effect = CreateEffect(device);

            //Get data from effect

            //try
            //{
            //    fxWorldViewProjection = effect.GetVariableByName("gWorldViewProj").AsMatrix();
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception("D3D failed to retrieve data from effect: " + ex);
            //}

            InputElement[] inputElementDesc = SpecifyInputLayoutDescriptionForVertex();

            CreateInputLayout(inputElementDesc);



            // Setup the description of the dynamic matrix constant buffer that is in the vertex shader.
            var matrixBufferDesc = new BufferDescription()
            {
                Usage = ResourceUsage.Dynamic,
                //or size of constant buffer
                SizeInBytes = Utilities.SizeOf<Matrix>(), 
                BindFlags = BindFlags.ConstantBuffer,
                CpuAccessFlags = CpuAccessFlags.Write,
                OptionFlags = ResourceOptionFlags.None,
                StructureByteStride = 0
            };
            constantMatrixBuffer = new Buffer(device, matrixBufferDesc);
        }

        public void Render(DeviceContext deviceContext, Matrix worldViewProj)
        {
            try
            {
                deviceContext.InputAssembler.InputLayout = inputLayout;

                try
                {
                    deviceContext.VertexShader.SetConstantBuffer(0, constantMatrixBuffer);
                }
                catch (Exception ex)
                {
                     throw new Exception("D3D failed to set constant buffer" + ex);
                }

                try
                {
                    deviceContext.VertexShader.Set(vertexShader);
                    deviceContext.PixelShader.Set(pixelShader);
                }
                catch (Exception ex)
                {
                    
                    throw new Exception("D3D failed to set Vertex and Pixel shader: " + ex);
                }

                try
                {
                    //Matrix shaderGlobals = worldViewProj;
                    SHADER_GLOBALS shaderGlobals = new SHADER_GLOBALS(worldViewProj);
                    deviceContext.UpdateSubresource(ref shaderGlobals, constantMatrixBuffer);

                }
                catch (Exception ex)
                {
                    
                    throw new Exception("D3D failed to pass data to shader" + ex);
                }

                try
                {
                    deviceContext.Draw(indices.Length, 0);
                }
                catch (Exception ex)
                {
                    throw new Exception("D3D failed to draw scene: " + ex);
                }

                //fxWorldViewProjection.SetMatrix(worldViewProj);

                //EffectTechnique technique = effect.GetTechniqueByName("P0");
                //EffectTechniqueDescription techDesc = technique.Description;

                //for (int i = 0; i < techDesc.PassCount; i++)
                //{
                //    //if constant buffers needs to change, do it here(?)
                //    int flags = 0;
                //    //on current technique update constant buffers bind the shader program to pipeline and apply render stats and pass sets
                //    technique.GetPassByIndex(i).Apply(deviceContext, flags);

                //    deviceContext.DrawIndexed(indices.Length, 0, 0);
                //}
            }
            catch (Exception ex)
            {
                throw new Exception("D3D failed to render shaders: " + ex);
            }
        }

        public void Dispose()
        {
            vertexShaderByteCode.Dispose();
            pixelShaderByteCode.Dispose();
     
            constantMatrixBuffer.Dispose();
            inputLayout.Dispose();
            pixelShader.Dispose();
            vertexShader.Dispose();
        }

        private void CreateInputLayout(InputElement[] inputElementDesc)
        {
            try
            {
                var signature = ShaderSignature.GetInputSignature(vertexShaderByteCode);
                inputLayout = new InputLayout(device, signature, inputElementDesc);
                signature.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("D3D could not create input Layout: " + ex);
            }
        }

        // Now setup the layout of the data that goes into the shader.
        // It needs to match the VertexType structure in the Model and in the shader.
        private InputElement[] SpecifyInputLayoutDescriptionForVertex()
        {
            return new InputElement[]
            {
            new InputElement()
            {
            SemanticName = "POSITION",
            SemanticIndex = 0,
            Format = Format.R32G32B32A32_Float,
            Slot = 0,
            AlignedByteOffset = 0,
            Classification = InputClassification.PerVertexData,
            InstanceDataStepRate = 0
            },
             new InputElement()
            {
            SemanticName = "COLOR",
            SemanticIndex = 0,
            Format = Format.R32G32B32A32_Float,
            Slot = 0,
            AlignedByteOffset = 12,
            Classification = InputClassification.PerVertexData,
            InstanceDataStepRate = 0
            }
            };
        }

        private Effect CreateEffect(Device device)
        {
            try
            {
                return new Effect(device, vertexShaderByteCode, EffectFlags.None);

            }
            catch (Exception ex)
            {
                throw new Exception("D3D effect deploy failed: " + ex);
            }
        }

        public CompilationResult GetCompiledShader(string vertexShaderFileName)
        {
            try
            {
                return ShaderBytecode.CompileFromFile(vertexShaderFileName, "fx_5_0", ShaderFlags.None);
            }
            catch (Exception ex)
            {
                throw new Exception("Vertex shader compile failed :" + ex.Message);
            }
        }
    }
}
