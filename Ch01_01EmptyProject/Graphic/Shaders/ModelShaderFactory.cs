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
                	new Vector2(0, 1),
                   new Vector2(.5f, 0),
                   new Vector2(1, 1),
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

//private T CreateVertex<T>(Device device, Vector3[] positions, Vector4[] colors, IVertex[] vertices) where T : struct, IVertex
//{
//    vertices = new T[positions.Length];
//    //from this array, make coresponding structure
//    for (int i = 0; i < positions.Length; i++)
//    {
//        T a = new T();
//        a.Position = positions[i];



//        if (typeof(ColorVertex).IsAssignableFrom(typeof(T)))
//        {
//            //this is code for specific factories 
//            ColorVertex b = (ColorVertex)Convert.ChangeType(a, typeof(ColorVertex));
//            b.Color = colors[i];

//        }


//        IVertex[] v = (IVertex[])Convert.ChangeType(vertices, typeof(IVertex[]));
//        v[i] = b;
//    }
//}

//by adding where part T cant be nullable