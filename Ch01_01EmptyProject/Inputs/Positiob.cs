using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch01_01EmptyProject.Inputs
{
    public class Position
    {
        public long FrameTime;
        private float leftTurnSpeed;

        public void TurnLeft()
        {
            leftTurnSpeed += FrameTime * 0.01f;
            if (leftTurnSpeed > FrameTime * 0.15)
                leftTurnSpeed = FrameTime * 0.15f;

            RotationY -= leftTurnSpeed;
            if (RotationY < 0)
                RotationY += 360;
        }


    
public  float RotationY { get; set; }}
}
