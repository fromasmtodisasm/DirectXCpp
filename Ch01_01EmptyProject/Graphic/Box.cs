using Ch01_01EmptyProject.Graphic.Structures;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch01_01EmptyProject.Graphic
{
    struct Box2 : IShape
    {
        public Vector3[] Vertexes
        {
            get
            {
                Vector3[] positions = new Vector3[]
            {
         // Top Face
         new Vector3( -1.0f,  1.0f, -1.0f ), 
         new Vector3(  1.0f,  1.0f, -1.0f ), 
         new Vector3(  1.0f,  1.0f,  1.0f ),
         new Vector3( -1.0f,  1.0f,  1.0f ), 

		// Bottom Face
         new Vector3( -1.0f, -1.0f,  1.0f ), 
         new Vector3(  1.0f, -1.0f,  1.0f ), 
         new Vector3(  1.0f, -1.0f, -1.0f ), 
         new Vector3( -1.0f, -1.0f, -1.0f ), 

		// Left Face
         new Vector3( -1.0f, -1.0f,  1.0f ), 
         new Vector3( -1.0f, -1.0f, -1.0f ), 
         new Vector3( -1.0f,  1.0f, -1.0f ), 
         new Vector3( -1.0f,  1.0f,  1.0f ), 

		// Right Face
         new Vector3( 1.0f, -1.0f, -1.0f ), 
         new Vector3( 1.0f, -1.0f,  1.0f ),
         new Vector3( 1.0f,  1.0f,  1.0f ),
         new Vector3( 1.0f,  1.0f, -1.0f ), 

		// Back Face
         new Vector3( -1.0f, -1.0f, -1.0f ), 
         new Vector3(  1.0f, -1.0f, -1.0f ), 
         new Vector3(  1.0f,  1.0f, -1.0f ), 
         new Vector3( -1.0f,  1.0f, -1.0f ), 

		// Front Face
         new Vector3(  1.0f, -1.0f, 1.0f ), 
         new Vector3( -1.0f, -1.0f, 1.0f ),
         new Vector3( -1.0f,  1.0f, 1.0f ),
         new Vector3(  1.0f,  1.0f, 1.0f ), 
            };
                return positions;
            }
        }

        public int[] Indexes
        {
            get
            {
                return new int[]
            {
            // top
        3,1,0,
        2,1,3,

		// bottom
        7,5,4,
        6,5,7,

		// left
        11,9,8,
        10,9,11,

		// right
        15,13,12,
        14,13,15,

		// back
        19,17,16,
        18,17,19,

		// front
        23,21,20,
        22,21,23,
            };
            }
        }
    }
}
