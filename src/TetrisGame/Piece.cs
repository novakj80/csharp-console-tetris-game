using System;
using System.Collections.Generic;
using System.Text;

namespace TetrisGame
{
    class Piece
    {
        Block[,] blocks;   // Blocks in grid

        private Piece(Block[,] blocks)
        {
            this.blocks = blocks;
        }
        public Block[,] GetBlocks() => blocks;

        public void RotateR()
        {
            Block[,] rotatedBlocks = new Block[blocks.GetLength(0), blocks.GetLength(1)];
            for (int i = 0; i < blocks.GetLength(0); i++)
            {
                for (int j = 0; j < blocks.GetLength(0); j++)
                {
                    rotatedBlocks[j, blocks.GetLength(0) - 1 - i] = blocks[i, j];
                }
            }
            blocks = rotatedBlocks;
        }
        public void RotateL()
        {
            Block[,] rotatedBlocks = new Block[blocks.GetLength(0), blocks.GetLength(1)];
            for (int i = 0; i < blocks.GetLength(0); i++)
            {
                for (int j = 0; j < blocks.GetLength(0); j++)
                {
                    rotatedBlocks[blocks.GetLength(0) - 1 - j, i] = blocks[i, j];
                }
            }
            blocks = rotatedBlocks;
        }
        static Block[][,] blocksAllPieces =
        {
            new Block[2,2]
            {
                {Block.Yellow, Block.Yellow },  // ■■
                {Block.Yellow, Block.Yellow }   // ■■
            }, new Block[3,3]
            {
                { null, Block.Magenta, null },                    //  ■ 
                { Block.Magenta, Block.Magenta, Block.Magenta},   // ■■■
                { null, null, null},                              //    
            }, new Block[3,3]
            {
                { Block.Red, Block.Red, null},    // ■■
                { null, Block.Red, Block.Red},    //  ■■
                { null, null, null},              //
            }, new Block[3,3]
            {
                { null, Block.Green, Block.Green},    //  ■■
                { Block.Green, Block.Green, null},    // ■■
                { null, null, null},                  //
            }, new Block[3,3]
            {
                { Block.Blue, null, null },              // ■  
                { Block.Blue, Block.Blue, Block.Blue},   // ■■■
                { null, null, null},                     //    
            }, new Block[3,3]
            {
                { null, null, Block.DarkYellow },                          //   ■ 
                { Block.DarkYellow, Block.DarkYellow, Block.DarkYellow},   // ■■■
                { null, null, null},
            }, new Block[4,4]
            {
                { null, null, null, null },                          //
                { Block.Cyan, Block.Cyan, Block.Cyan, Block.Cyan },  // ■■■■
                { null, null, null, null },                          //
                { null, null, null, null },                          //
            }
        };
        public static Piece GetRandomPiece()
        {
            return new Piece(blocksAllPieces[Rnd.GetRandom().Next(blocksAllPieces.Length)]);
        }
        public static readonly Piece BLANK = new Piece(new Block[4, 4] { { Block.EMPTY, Block.EMPTY, Block.EMPTY, Block.EMPTY }, { Block.EMPTY, Block.EMPTY, Block.EMPTY, Block.EMPTY }, { Block.EMPTY, Block.EMPTY, Block.EMPTY, Block.EMPTY }, { Block.EMPTY, Block.EMPTY, Block.EMPTY, Block.EMPTY } });
    }
}
