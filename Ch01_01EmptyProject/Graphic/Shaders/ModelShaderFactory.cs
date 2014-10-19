using Ch01_01EmptyProject.Graphic.Structures;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch01_01EmptyProject.Graphic.Shaders
{
    public static class ModelShaderFactory
    {
        public static object Create(VertexType type, Vector3[] positions)
        {
            object specificVertexes = new object();
            switch (type)
            {
                case VertexType.ColorVertex:
                    {
                        specificVertexes = ColorVertex<ColorVertex>(positions);
                    }
                    break;
                case VertexType.TextureVertex:
                    {
                        specificVertexes = TextureVertex<TextureVertex>(positions);
                    }
                    break;
            }

            return specificVertexes;
        }

        private static Vector2[] GetTextureCoord()
        {
            Vector2[] textureCoord = new Vector2[]
           {	
          new Vector2( 0.0f, 0.0f ),
         new Vector2( 1.0f, 0.0f ),
         new Vector2( 1.0f, 1.0f ),
         new Vector2( 0.0f, 1.0f ),

		// Bottom Face
         new Vector2( 0.0f, 0.0f ),
         new Vector2( 1.0f, 0.0f ),
         new Vector2( 1.0f, 1.0f ),
         new Vector2( 0.0f, 1.0f ),

		// Left Face
         new Vector2( 0.0f, 0.0f ),
         new Vector2( 1.0f, 0.0f ),
         new Vector2( 1.0f, 1.0f ),
         new Vector2( 0.0f, 1.0f ),

		// Right Face
        new Vector2( 0.0f, 0.0f ),
        new Vector2( 1.0f, 0.0f ),
        new Vector2( 1.0f, 1.0f ),
        new Vector2( 0.0f, 1.0f ),

		// Back Face
         new Vector2( 0.0f, 0.0f ),
         new Vector2( 1.0f, 0.0f ),
         new Vector2( 1.0f, 1.0f ),
         new Vector2( 0.0f, 1.0f ),

		// Front Face
        new Vector2( 0.0f, 0.0f ),
        new Vector2( 1.0f, 0.0f ),
        new Vector2( 1.0f, 1.0f ),
        new Vector2( 0.0f, 1.0f ),
   };


            //Vector2[] textureCoord = new Vector2[]
            //    {
            //       new Vector2(0, 1),
            //       new Vector2(0.5f, 0),
            //       new Vector2(1, 1),
            //    };
            return textureCoord;
        }


        private static Vector3[] GetTextureCoord3D()
        {
            Vector3[] textureCoord = new Vector3[]
                {
                   //new Vector3(0, 1),
                   //new Vector3(0.5f, 0),
                   //new Vector3(1, 1),
                };
            return textureCoord;
        }

        private static Vector4[] GetColors()
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
            return colors;
        }



        public static T[] ColorVertex<T>(Vector3[] positions) where T : struct
        {
            Vector4[] colors = GetColors();
            //Vector2[] textureCoord = GetTextureCoord();

            ColorVertex[] vertices = new ColorVertex[positions.Length];
            //from this array, make coresponding structure
            for (int i = 0; i < positions.Length; i++)
            {
                ColorVertex a = new ColorVertex();
                a.Position = positions[i];
                a.Color = colors[i];
                vertices[i] = a;
            }

            var y = (T[])Convert.ChangeType(vertices, typeof(T[]));
            return y;
        }


        public static T[] TextureVertex<T>(Vector3[] positions) where T : struct
        {
            Vector2[] textureCoord = GetTextureCoord();

            TextureVertex[] vertices = new TextureVertex[positions.Length];
            //from this array, make coresponding structure
            for (int i = 0; i < positions.Length; i++)
            {
                TextureVertex a = new TextureVertex();
                a.Position = positions[i];
                a.Texture = textureCoord[i];
                vertices[i] = a;
            }

            var y = (T[])Convert.ChangeType(vertices, typeof(T[]));
            return y;
        }

    }
}
