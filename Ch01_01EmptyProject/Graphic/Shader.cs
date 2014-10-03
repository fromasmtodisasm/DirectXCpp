﻿using System;
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
//using SharpDX.Direct3D10.E;

using Effect = SharpDX.Direct3D11.Effect;

using Device = SharpDX.Direct3D11.Device;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace Ch01_01EmptyProject
{
    public class Shader : IDisposable, IRenderable
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
        private CompilationResult vertexShaderBytecode;
        private CompilationResult pixelShaderBytecode;
        private Buffer constantMatrixBuffer;

        public Shader(Device device)
        {
            this.device = device;

            //string vertexShaderFileName = @"C:\Users\jamesss\Documents\GitHub\DirectXCpp\Ch01_01EmptyProject\Graphic\Shaders\VertexShader.fx";

            string vertexShaderFileName = @"C:\Users\jamesss\Documents\GitHub\DirectXCpp\Ch01_01EmptyProject\Graphic\Shaders\Color.vs";
            string pixelShaderFileName = @"C:\Users\jamesss\Documents\GitHub\DirectXCpp\Ch01_01EmptyProject\Graphic\Shaders\Color.ps";
                
            //vertexShaderBytecode = GetCompiledShader(vertexShaderFileName);
            //pixelShaderBytecode = GetCompiledShader(pixelShaderFileName);

             vertexShaderBytecode = ShaderBytecode.CompileFromFile(vertexShaderFileName, "ColorVertexShader", "vs_4_0", ShaderFlags.None, EffectFlags.None);
            // Compile the pixel shader code.
             pixelShaderBytecode = ShaderBytecode.CompileFromFile(pixelShaderFileName, "ColorPixelShader", "ps_4_0", ShaderFlags.None, EffectFlags.None);

            //effect = CreateEffect(device);

            //update constant matrix
            //Vector4 x = new Vector4(1, 1, 1, 1);
            //EffectMatrixVariable fxGWorldViewProjection = effect.GetVariableByName("gWorldViewProj").AsMatrix();
            //fxGWorldViewProjection.SetMatrix(x);

            try
            {
                vertexShader = new VertexShader(device, vertexShaderBytecode.Bytecode.Data);
            }
            catch (Exception ex)
            {

                throw new Exception("D3D couldnt create vertex shader: " + ex);
            }

            try
            {
                pixelShader = new PixelShader(device, pixelShaderBytecode.Bytecode.Data);
            }
            catch (Exception ex)
            {

                throw new Exception("D3D couldnt create pixel shader: " + ex);
            }
        
           InputElement[] inputElementDesc = SpecifyInputLayoutDescriptionForVertex();

            CreateInputLayout(inputElementDesc);

            vertexShaderBytecode.Dispose();
            pixelShaderBytecode.Dispose();

            // Setup the description of the dynamic matrix constant buffer that is in the vertex shader.
            var matrixBufferDesc = new BufferDescription()
            {
                Usage = ResourceUsage.Dynamic,
                SizeInBytes = Utilities.SizeOf<Matrix>(),
                BindFlags = BindFlags.ConstantBuffer,
                CpuAccessFlags = CpuAccessFlags.Write,
                OptionFlags = ResourceOptionFlags.None,
                StructureByteStride = 0
            };
             constantMatrixBuffer = new Buffer(device, matrixBufferDesc);
        }

        public void Render(DeviceContext deviceContext)
        {
            try
            {
                deviceContext.InputAssembler.InputLayout = inputLayout;
                deviceContext.VertexShader.Set(vertexShader);
                deviceContext.PixelShader.Set(pixelShader);

                deviceContext.DrawIndexed(indexCount, 0, 0);
                
                //EffectTechnique technique = effect.GetTechniqueByName("P0");
                //EffectTechniqueDescription techDesc = technique.Description;

                //for (int i = 0; i < techDesc.PassCount; i++)
                //{
                //    //if constant buffers needs to change, do it here(?)

                //    int flags = 0;

                //    //on current technique update constant buffers bind the shader program to pipeline and apply render stats and pass sets
                //    technique.GetPassByIndex(i).Apply(deviceContext, flags);

                //    //ImmediateContext.DrawIndexed(indices.Length, 0, 0);   
                //}

                //deviceContext.Draw(vertices.Length, 0);
                //deviceContext.DrawIndexed(indices.Length, 0, 0);        
            }
            catch (Exception ex)
            {

                throw new Exception("D3D failed to render shaders: " + ex);
            }
        }

        public void Dispose()
        {
            constantMatrixBuffer.Dispose();
            inputLayout.Dispose();
            pixelShader.Dispose();
            vertexShader.Dispose();
        }

        private void CreateInputLayout(InputElement[] inputElementDesc)
        {
            try
            {
                var inputLayout = new InputLayout(device, ShaderSignature.GetInputSignature(vertexShaderBytecode), inputElementDesc);
                //var inputLayout = new InputLayout(device, vertexShaderBytecode.Data, inputElementDesc);
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
                return new Effect(device, vertexShaderBytecode, EffectFlags.None);

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
