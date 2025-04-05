using System;
using System.Collections.Generic;
using System.Text;

namespace TetrisGame
{
    class Block
    {
        ConsoleColor color;
        private Block(ConsoleColor color)
        {
            this.color = color;
        }
        public ConsoleColor GetColor() => color;
        public static readonly Block Yellow = new Block(ConsoleColor.Yellow);
        public static readonly Block Red = new Block(ConsoleColor.Red);
        public static readonly Block Blue = new Block(ConsoleColor.Blue);
        public static readonly Block Cyan = new Block(ConsoleColor.Cyan);
        public static readonly Block Magenta = new Block(ConsoleColor.Magenta);
        public static readonly Block Green = new Block(ConsoleColor.Green);
        public static readonly Block DarkYellow = new Block(ConsoleColor.DarkYellow);
        public static readonly Block White = new Block(ConsoleColor.White);
        public static readonly Block EMPTY = new Block(ConsoleColor.Black);
    }
}
