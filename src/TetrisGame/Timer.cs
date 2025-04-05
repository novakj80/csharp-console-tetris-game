using System;
using System.Collections.Generic;
using System.Text;

namespace TetrisGame
{
    class Timer
    {
        System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

        public void Start()
        {
            stopwatch.Start();
        }
        public void Stop()
        {
            stopwatch.Reset();
        }
        public void Reset()
        {
            stopwatch.Restart();
        }
        /// <summary>
        /// </summary>
        /// <param name="millis"></param>
        /// <returns>True if more than given time in milliseconds elapsed</returns>
        public bool IsOver(long millis)
        {
            return stopwatch.ElapsedMilliseconds >= millis;
        }
    }
}
