using System;
using System.Collections.Generic;
using System.Text;

namespace TetrisGame
{
    class KeyboardController : IController
    {
        Dictionary<ConsoleKey, GameAction> keyMap;

        public KeyboardController()
        {
            keyMap = new Dictionary<ConsoleKey, GameAction>();
            keyMap.Add(ConsoleKey.LeftArrow, GameAction.MoveLeft);
            keyMap.Add(ConsoleKey.RightArrow, GameAction.MoveRight);
            keyMap.Add(ConsoleKey.DownArrow, GameAction.MoveDown);
            keyMap.Add(ConsoleKey.UpArrow, GameAction.Drop);
            keyMap.Add(ConsoleKey.Escape, GameAction.Exit);
            keyMap.Add(ConsoleKey.X, GameAction.RotateLeft);
            keyMap.Add(ConsoleKey.C, GameAction.RotateRight);
        }
        public KeyboardController(Dictionary<ConsoleKey, GameAction> keyMap)
        {
            this.keyMap = keyMap;
        }
        public GameAction GetInput()
        {
            while(Console.KeyAvailable)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                if (keyMap.ContainsKey(key)) return keyMap[key];
            }
            return GameAction.DoNothing;
        }
    }
}
