using System;
using System.Collections.Generic;
using System.Text;

namespace TetrisGame
{
    class Position
    {
        int top, left;
        Position relativeTo;
        public Position(int top, int left)
        {
            this.top = top;
            this.left = left;
            relativeTo = null;
        }
        public Position(int top, int left, Position relativeTo)
        {
            this.relativeTo = relativeTo;
            this.top = top;
            this.left = left;
        }

        public int GetTop() => top;
        public int GetLeft() => left;
        public void MoveUp()
        {
            top--;
        }
        public void MoveDown()
        {
            top++;
        }
        public void MoveLeft()
        {
            left--;
        }
        public void MoveRight()
        {
            left++;
        }
        public Position GetAbsolutePosition()
        {
            int absoluteTop = top;
            int absoluteLeft = left;
            
            for(Position position = relativeTo; position != null; position = position.relativeTo)
            {
                absoluteLeft += position.left;
                absoluteTop += position.top;
            }
            return new Position(absoluteTop, absoluteLeft);
        }
    }
}
