using Ch01_01EmptyProject.Graphic.Structures;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch01_01EmptyProject.Graphic
{
    struct Box : IShape
    {
        public Vector3[] Vertexes
        {
            get
            {
                Vector3[] positions = new Vector3[8]
            {
            new Vector3(-1, -1, -1),
            new Vector3(-1, 1, -1),
            new Vector3(+1, +1, -1),
            new Vector3(+1, -1, -1),
            new Vector3(-1, -1, +1),
            new Vector3(-1, +1, +1),
            new Vector3(+1, +1, +1),
            new Vector3(+1, -1, +1),
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
                //frontFace
                0, 1, 2 ,
                0, 2, 3, 
                //backFace
                4, 6, 5,
                4, 7, 6,
                //leftFace
                4, 5, 1,
                4, 1, 0,
                //rightFace
                3, 2, 6,
                3, 6, 7,
                //topFace
                1, 5, 6,
                1, 6, 2,
                //bottomFace
                4, 0, 3,
                4, 3, 7,
            };
            }
        }
    }
}
