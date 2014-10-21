using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Diagnostics;

namespace Ch01_01EmptyProject
{
    public class SystemTime
    {
        Stopwatch stopWatch;
        private long frameTime;

        public long FrameTime
        {
            get { return frameTime; }
            set { frameTime = value; }
        }

        public SystemTime()
        {
            if (IsHighPerformanceTimerSupported())
            {
                stopWatch = Stopwatch.StartNew();
            }
        }

        private bool IsHighPerformanceTimerSupported()
        {
            return !(Stopwatch.Frequency == 0);
        }

        //find difference between the loops, + execution time
        public void Frame()
        {
            stopWatch.Stop();
            frameTime = stopWatch.ElapsedMilliseconds;
            stopWatch.Restart();
        }
    }
}
