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
                case VertexType.TextureNormalVertex:
                    {
                        specificVertexes = TextureNormalVertex<TextureNormalVertex>(positions);
                    }
                    break;
                case VertexType.TextureNormalTangentBinormalVertex:
                    {
                        specificVertexes = TextureNormalTangentBinormalVertex<TextureNormalTangentBinormalVertex>(positions);
                    }
                    break;
                case VertexType.TextureNormalTangentVertex:
                    {
                        specificVertexes = TextureNormalTangentVertex<TextureNormalTangentVertex>(positions);
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
           


            //Vector2[] textureCoord = new Vector2[]
            //    {
            //       new Vector2(0, 1),
            //       new Vector2(0.5f, 0),
            //       new Vector2(1, 1),
            //    };
               };
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

        private static Vector3[] GetNormalCoord()
        {
            Vector3[] textureCoord = new Vector3[]
                {
                  // Top Face
        new Vector3( 0.0f,  1.0f, 0.0f ),
        new Vector3( 0.0f,  1.0f, 0.0f ), 
        new Vector3( 0.0f,  1.0f, 0.0f ),
        new Vector3( 0.0f,  1.0f, 0.0f ), 

		// Bottom Face
        new Vector3( 0.0f,  -1.0f, 0.0f ), 
        new Vector3( 0.0f,  -1.0f, 0.0f ), 
        new Vector3( 0.0f,  -1.0f, 0.0f ), 
        new Vector3( 0.0f,  -1.0f, 0.0f ), 

		// Left Face
        new Vector3( -1.0f,  0.0f, 0.0f ), 
        new Vector3( -1.0f,  0.0f, 0.0f ), 
        new Vector3( -1.0f,  0.0f, 0.0f ), 
        new Vector3( -1.0f,  0.0f, 0.0f ), 

		// Right Face
       new Vector3( 1.0f,  0.0f, 0.0f ), 
       new Vector3( 1.0f,  0.0f, 0.0f ),
       new Vector3( 1.0f,  0.0f, 0.0f ),
       new Vector3( 1.0f,  0.0f, 0.0f ),

		// Back Face
        new Vector3( 0.0f,  0.0f, -1.0f ), 
        new Vector3( 0.0f,  0.0f, -1.0f ),
        new Vector3( 0.0f,  0.0f, -1.0f ),
        new Vector3( 0.0f,  0.0f, -1.0f ),

		// Front Face
       new Vector3( 0.0f,  0.0f, 1.0f ),
       new Vector3( 0.0f,  0.0f, 1.0f ),
       new Vector3( 0.0f,  0.0f, 1.0f ),
       new Vector3( 0.0f,  0.0f, 1.0f ),
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
                            (Vector4)Color.White,
                        (Vector4)Color.Black,
                        (Vector4)Color.Red,
                        (Vector4)Color.Green,
                        (Vector4)Color.Blue,
                        (Vector4)Color.Yellow,
                        (Vector4)Color.Cyan,
                        (Vector4)Color.Magenta,
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

        private static Vector4[] GetColorsForBook()
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
            Vector4[] colors = GetColorsForBook();
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

        private static Vector3[] GetBinormalCoord()
        {
            Vector3[] textureCoord = new Vector3[]
                {
           	// Top Face
         new Vector3( 0.0f,  0.0f, 1.0f ) ,
         new Vector3( 0.0f,  0.0f, 1.0f ) ,
         new Vector3( 0.0f,  0.0f, 1.0f ) ,
         new Vector3( 0.0f,  0.0f, 1.0f ) ,

		// Bottom Face
       new Vector3( 0.0f,  0.0f, -1.0f ) ,
       new Vector3( 0.0f,  0.0f, -1.0f ) ,
     new Vector3( 0.0f,  0.0f, -1.0f ) ,
      new Vector3( 0.0f,  0.0f, -1.0f ) ,

		// Left Face
    new Vector3( 0.0f,  1.0f, 0.0f ) ,
     new Vector3( 0.0f,  1.0f, 0.0f ) ,
      new Vector3( 0.0f,  1.0f, 0.0f ) ,
       new Vector3( 0.0f,  1.0f, 0.0f ) ,

		// Right Face
        new Vector3( 0.0f,  1.0f, 0.0f ) ,
        new Vector3( 0.0f,  1.0f, 0.0f ) ,
        new Vector3( 0.0f,  1.0f, 0.0f ) ,
        new Vector3( 0.0f,  1.0f, 0.0f ) ,

		// Back Face
        new Vector3( 0.0f,  1.0f, 0.0f ) ,
       new Vector3( 0.0f,  1.0f, 0.0f ) ,
        new Vector3( 0.0f,  1.0f, 0.0f ) ,
         new Vector3( 0.0f,  1.0f, 0.0f ) ,

		// Front Face
      new Vector3( 0.0f,  1.0f, 0.0f ) ,
    new Vector3( 0.0f,  1.0f, 0.0f ) ,
       new Vector3( 0.0f,  1.0f, 0.0f ) ,
     new Vector3( 0.0f,  1.0f, 0.0f ) ,
         };
            return textureCoord;
        }

        private static Vector3[] GetTangentCoord()
        {
            Vector3[] textureCoord = new Vector3[]
                {
        	// Top Face
        new Vector3( 1.0f,  0.0f, 0.0f ), 
        new Vector3( 1.0f,  0.0f, 0.0f ), 
        new Vector3( 1.0f,  0.0f, 0.0f ), 
        new Vector3( 1.0f,  0.0f, 0.0f ), 

		// Bottom Face
      new Vector3( 1.0f,  0.0f, 0.0f ),
      new Vector3( 1.0f,  0.0f, 0.0f ), 
    new Vector3( 1.0f,  0.0f, 0.0f ), 
     new Vector3( 1.0f,  0.0f, 0.0f ),

		// Left Face
      new Vector3( 0.0f,  0.0f, -1.0f ),
     new Vector3( 0.0f,  0.0f, -1.0f ),
        new Vector3( 0.0f,  0.0f, -1.0f ),
       new Vector3( 0.0f,  0.0f, -1.0f ), 

		// Right Face
       new Vector3( 0.0f,  0.0f, 1.0f ), 
       new Vector3( 0.0f,  0.0f, 1.0f ), 
       new Vector3( 0.0f,  0.0f, 1.0f ), 
       new Vector3( 0.0f,  0.0f, 1.0f ), 

		// Back Face
       new Vector3( 1.0f,  0.0f, 0.0f ), 
      new Vector3( 1.0f,  0.0f, 0.0f ), 
       new Vector3( 1.0f,  0.0f, 0.0f ), 
        new Vector3( 1.0f,  0.0f, 0.0f ), 

		// Front Face
       new Vector3( -1.0f,  0.0f, 0.0f ), 
       new Vector3( -1.0f,  0.0f, 0.0f ), 
       new Vector3( -1.0f,  0.0f, 0.0f ),
       new Vector3( -1.0f,  0.0f, 0.0f ),
              };

            return textureCoord;
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

        private static object TextureNormalTangentBinormalVertex<T>(Vector3[] positions) where T : struct
        {
            Vector2[] textureCoord = GetTextureCoord();
            Vector3[] normalCoord = GetNormalCoord();
            Vector3[] binormalCoord = GetBinormalCoord();
            Vector3[] tangentCoord = GetTangentCoord();

            TextureNormalTangentBinormalVertex[] vertices = new TextureNormalTangentBinormalVertex[positions.Length];
            //from this array, make coresponding structure
            for (int i = 0; i < positions.Length; i++)
            {
                TextureNormalTangentBinormalVertex a = new TextureNormalTangentBinormalVertex();
                a.Position = positions[i];
                a.Texture = textureCoord[i];
                a.Normal = normalCoord[i];
                a.Tangent = tangentCoord[i];
                a.Binormal = binormalCoord[i];

                vertices[i] = a;
            }

            var y = (T[])Convert.ChangeType(vertices, typeof(T[]));
            return y;


            //ector2[] textureCoord = GetTextureCoord();
            //Vector3[] normalCoord = GetNormalCoord();
            //Vector3[] binormalCoord = GetBinormalCoord();
            //Vector3[] tangentCoord = GetTangentCoord();

            //TextureNormalTangentBinormalVertex[] vertices = new TextureNormalTangentBinormalVertex[positions.Length];
            ////from this array, make coresponding structure
            //for (int i = 0; i < positions.Length; i++)
            //{
            //    TextureNormalTangentBinormalVertex a = new TextureNormalTangentBinormalVertex();
            //    a.Position = positions[i];
            //    a.Texture = textureCoord[i];
            //    a.Normal = normalCoord[i];
            //    a.Tangent = tangentCoord[i];
            //    a.Binormal = binormalCoord[i];

            //    vertices[i] = a;
            //}

            //var y = (T[])Convert.ChangeType(vertices, typeof(T[]));
            //return y;
        }

        private static object TextureNormalTangentVertex<T>(Vector3[] positions)
        {
            Vector2[] textureCoord = GetTextureCoord();
            Vector3[] normalCoord = GetNormalCoord();
            Vector3[] tangentCoord = GetTangentCoord();

            TextureNormalTangentVertex[] vertices = new TextureNormalTangentVertex[positions.Length];
            //from this array, make coresponding structure
            for (int i = 0; i < positions.Length; i++)
            {
                TextureNormalTangentVertex a = new TextureNormalTangentVertex();
                a.Position = positions[i];
                a.Texture = textureCoord[i];
                a.Normal = normalCoord[i];
                a.Tangent = tangentCoord[i];

                vertices[i] = a;
            }

            var y = (T[])Convert.ChangeType(vertices, typeof(T[]));
            return y;
        }

        public static T[] TextureNormalVertex<T>(Vector3[] positions) where T : struct
        {
            Vector2[] textureCoord = GetTextureCoord();
            Vector3[] normalCoord = GetNormalCoord();

            TextureNormalVertex[] vertices = new TextureNormalVertex[positions.Length];
            //from this array, make coresponding structure
            for (int i = 0; i < positions.Length; i++)
            {
                TextureNormalVertex a = new TextureNormalVertex();
                a.Position = positions[i];
                a.Texture = textureCoord[i];
                a.Normal = normalCoord[i];
                vertices[i] = a;
            }

            var y = (T[])Convert.ChangeType(vertices, typeof(T[]));
            return y;
        }

        //internal static object Create(VertexType vertexType, Model.Model.ModelFormat[] modelFormat)
        //{
        //    Create(vertexType, modelFormat[]);
        //}
    }
}
