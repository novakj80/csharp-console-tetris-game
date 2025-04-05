using System;
using System.Collections.Generic;

namespace TetrisGame
{
    static class Program
    {
        /// <summary>
        /// Entry point of the Application  
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Game game = WelcomeScreen();
            while(true)
            {
                int score = game.Run();
                Console.Clear();
                Console.ResetColor();
                Console.WriteLine("GAME OVER");
                Console.WriteLine("Your score was " + score);
                Console.WriteLine("Play again? [y/n] ");
                ConsoleKey key = Console.ReadKey(true).Key;
                while (key != ConsoleKey.Y && key != ConsoleKey.N)
                    key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.N) break;
            }
        }
        /// <summary>
            /// Print a welcome screen. Asks user if he would like to customize controls.
            /// Returns a game object with default or customized KeyboardController.
        /// </summary>
        static Game WelcomeScreen()
        {
            Console.ResetColor();
            Console.Clear();
            // Print the title screen
            // Name of the game in cool font
            Console.WriteLine(@"
 _____     _        _     
/__   \___| |_ _ __(_)___ 
  / /\/ _ \ __| '__| / __|
 / / |  __/ |_| |  | \__ \
 \/   \___|\__|_|  |_|___/
                          
");
            // Short instructions
            Console.WriteLine("Move and rotate the pieces left and right. " +
                "Once game board is filled to the top, it's game over." + 
                "Complete lines to clear them and gain points." +
                "Line is worth 10 points, Each additional line you clear with one piece is worth 5 points more.");
            Console.WriteLine();
            // Default controls
            Console.WriteLine(@"Controls:
[→]   Move piece right
[←]   Move piece left
[↑]   Smash piece down
[↓]   Move piece down by 1
[C]   Rotate Clockwise
[X]   Rotate CounterClockwise
[ESC] Exit game");
            Console.WriteLine();
            // Ask user if he wants to customize controls
            Console.WriteLine("Press M to customize controls");
            Console.WriteLine("Press Enter to start TETRIS");

            KeyboardController controller;
            ConsoleKey key;
            do
                key = Console.ReadKey(true).Key;
            while (key != ConsoleKey.M && key != ConsoleKey.Enter);
            if (key == ConsoleKey.M) controller = CustomizeControls();
            else controller = new KeyboardController();         // Default key binding
            // Return a game object
            return new Game(controller);     
        }

        /// <summary>
        /// Create KeyboardController object with custom controls.
        /// User is prompted to press a key for each action.
        /// </summary>
        /// <returns>KeyboardController object with custom controls.</returns>
        static KeyboardController CustomizeControls()
        {
            Dictionary<ConsoleKey, GameAction> keyMap = new Dictionary<ConsoleKey, GameAction>();
            Console.Clear();
            Console.WriteLine("Press a key you want to use for the action");
            
            LoadUniqueKey(keyMap, "Move Left: ", GameAction.MoveLeft);
            LoadUniqueKey(keyMap, "Move right: ", GameAction.MoveRight);
            LoadUniqueKey(keyMap, "Smash down: ", GameAction.Drop);
            LoadUniqueKey(keyMap, "Move down by 1: ", GameAction.MoveDown);
            LoadUniqueKey(keyMap, "Rotate clockwise: ", GameAction.RotateRight);
            LoadUniqueKey(keyMap, "Rotate counterclockwise: ", GameAction.RotateLeft);
            LoadUniqueKey(keyMap, "Exiting game: ", GameAction.Exit);

            Console.WriteLine();
            Console.WriteLine("Game is ready. Press any key to start");
            Console.ReadKey(true);
            return new KeyboardController(keyMap);

        }
        static void LoadUniqueKey(Dictionary<ConsoleKey, GameAction> keyMap, string prompt, GameAction action)
        {
            Console.Write(prompt);
            ConsoleKey key = Console.ReadKey(true).Key;
            while(keyMap.ContainsKey(key))
            {
                Console.WriteLine();
                Console.WriteLine("{0} {1}", key, "is already used");
                Console.Write(prompt);
                key = Console.ReadKey(true).Key;
            }
            Console.WriteLine(key);
            keyMap.Add(key, action);
        }
    }
}
