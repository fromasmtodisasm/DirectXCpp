using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch01_01EmptyProject.System
{
   public class FPS
    {
       private int count;

       private int value;

       public int Value
       {
           get { return this.value; }
           set { this.value = value; }
       }
       private TimeSpan startTime;
       private int elapsedTime;

        public FPS()
        {
            startTime = DateTime.Now.TimeOfDay;
        }

        public void Frame()
        {
            {
                count++;
                elapsedTime = (DateTime.Now.TimeOfDay - startTime).Seconds;

                if (IsSecondElapsed())
                {
                    value = count;
                    count = 0;
                    startTime = DateTime.Now.TimeOfDay;
                }
            }
        }

        private bool IsSecondElapsed()
        {
            return elapsedTime > 1;
        }

      
    }
}
