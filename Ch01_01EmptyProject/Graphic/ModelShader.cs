using Ch01_01EmptyProject.Graphic.Shaders;
using Ch01_01EmptyProject.Graphic.Structures;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch01_01EmptyProject.Graphic
{
    //should be observer later on
    public static class ModelShader
    {
        private static object modelShader;
        private static int[] indices;
        private static IShaderEffect shaderEffect;
        //shader name , model type
        public static void Get(ShaderName shaderName, IShape shape)
        {
            //add to the base shapea additional stuff defined by Vector Structure
         
          indices = shape.Indexes;

            shaderEffect = ShaderFactory.Create(shaderName);
            modelShader = ModelShaderFactory.Create(shaderEffect.VertexType, shape.Vertexes);
            
            //var model = new Modelf.Model();
            //model.LoadModel("Cube");

            //modelShader = ModelShaderFactory.Create(shaderEffect.VertexType, model.Data);
         }

        public static object GetModelForRender { get { return modelShader; }}

        public static int GetIndexCount { get { return indices.Length; } }
        public static int[] GetIndexes { get { return indices; } }

        public static IShaderEffect GetShaderEffect { get {return shaderEffect; } }
    }
}
