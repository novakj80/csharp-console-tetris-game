using System;
using System.Collections.Generic;
using System.Text;

namespace TetrisGame
{
    class Rnd
    {
        static readonly Random random = new Random();
        private Rnd() { }
        public static Random GetRandom()
        {
            return random;
        }
    }
}
