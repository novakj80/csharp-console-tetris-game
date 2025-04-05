using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace TetrisGame
{
    class Board
    {
        Block[,] blocks;
        int height, width;
        public Board(int height, int width)
        {
            this.height = height;
            this.width = width;
            blocks = new Block[height + 2, width + 2];
            for(int i = 0; i < height + 2; i++)
            {
                for(int j = 0; j < width + 2; j++)
                {
                    blocks[i, j] = Block.EMPTY;
                }
            }
            for(int i = 0; i < height + 2; i++)
            {
                blocks[i, 0] = Block.White;
                blocks[i, width + 1] = Block.White;
            }
            for(int i = 0; i < width + 2; i++)
            {
                blocks[0, i] = Block.White;
                blocks[height + 1, i] = Block.White;
            }
        }
        public Block[,] GetBlocks() => blocks;

        /// <summary>
        /// True if piece collides with any block. on board.
        /// </summary>
        /// <param name="piece"></param>
        public bool IsCollision(Piece piece, Position position)
        {
            Block[,] pieceBlocks = piece.GetBlocks();
            for(int i = 0; i < pieceBlocks.GetLength(0); i++)
            {
                for(int j = 0; j < pieceBlocks.GetLength(1); j++)
                {
                    if (pieceBlocks[i, j] == null) continue;
                    if (position.GetTop() + i < 0 || position.GetLeft() + j < 0) return true;
                    if (position.GetTop() + i > blocks.GetLength(0)) return true;
                    if (position.GetLeft() + j > blocks.GetLength(1)) return true;
                    if (blocks[position.GetTop() + i, position.GetLeft() + j] == Block.EMPTY) continue;
                    return true;
                }
            }
            return false;
        }
        public void AddPiece(Piece piece, Position position)
        {
            Block[,] pieceBlocks = piece.GetBlocks();
            for(int i = 0; i < pieceBlocks.GetLength(0); i++)
            {
                for(int j = 0; j < pieceBlocks.GetLength(1); j++)
                {
                    if (pieceBlocks[i, j] == null) continue;
                    blocks[position.GetTop() + i, position.GetLeft() + j] = pieceBlocks[i, j];
                }
            }
        }
        /// <summary>
        /// Clear full lines and return number of lines cleared
        /// </summary>
        /// <returns></returns>
        public int ClearLines()
        {
            int lines = 0;
            for(int i = height; i > 0; i--)
            {
                while(IsFullLine(i))
                {
                    ClearLine(i);
                    Fall(i);
                    lines++;
                }
            }
            return lines;
        }
        bool IsFullLine(int line)
        {
            for(int i = 1; i < width + 1; i++)
            {
                if (blocks[line, i] == Block.EMPTY) return false;
            }
            return true;
        }
        void ClearLine(int line)
        {
            for(int i = 1; i < width + 1; i++)
            {
                blocks[line, i] = Block.EMPTY;
            }
        }
        void Fall(int toLine)
        {
            for(int i = toLine; i > 1; i--)
            {
                for(int j = 1; j < width + 1; j++)
                {
                    blocks[i, j] = blocks[i - 1, j];
                }
            }
            for (int j = 1; j < width + 1; j++) blocks[1, j] = Block.EMPTY;
        }
    }
}
