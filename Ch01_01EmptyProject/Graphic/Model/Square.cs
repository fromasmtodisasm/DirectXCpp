using Ch01_01EmptyProject.Graphic.Structures;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch01_01EmptyProject.Graphic
{
    
    struct Square : IShape 
    {
        public Vector3[] Vertexes
        {
            get
            {
                Vector3[] positions = new Vector3[]
            {
                        new Vector3(-1, -1, 0),
               new Vector3(0, 1 , 0),
               new Vector3(1, -1, 0),
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
      
                0, // Bottom left.
                    1, // Top middle.
                2,
            
                // Bottom right
                
            
      };
            }
        }
    }
}
