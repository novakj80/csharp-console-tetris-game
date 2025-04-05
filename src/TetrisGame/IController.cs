using System;
using System.Collections.Generic;
using System.Text;

namespace TetrisGame
{
    interface IController
    {
        GameAction GetInput();
    }
}
