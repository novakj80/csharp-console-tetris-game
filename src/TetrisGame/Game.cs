using System;
using System.Collections.Generic;
using System.Text;

namespace TetrisGame
{
    class Game
    {
        Board board;
        Piece nextPiece, descendingPiece;
        int score, lines;
        const int BOARD_WIDTH = 10;
        const int BOARD_HEIGHT = 20;
        const int INITIAL_DELAY = 800;
        int speed, delay;
        bool gameOver;
        Dictionary<GameAction, Action> methodMap;
        Position
            displayPosition,
            boardPosition,
            rightPanelPosition,
            scoreTextPosition,
            scorePosition,
            speedTextPosition,
            speedPosition,
            nextPiecePosition,
            nextTextPosition,
            descendingPiecePosition;
        IController controller;
        Timer timer;

        public Game(IController controller)
        {
            displayPosition = new Position(0, 0);
            boardPosition = new Position(5, 2, displayPosition);
            rightPanelPosition = new Position(0, BOARD_WIDTH + 5, boardPosition);
            scoreTextPosition = new Position(0, 0, rightPanelPosition);
            scorePosition = new Position(1, 0, rightPanelPosition);
            speedTextPosition = new Position(3, 0, rightPanelPosition);
            speedPosition = new Position(4, 0, rightPanelPosition);
            nextTextPosition = new Position(6, 0, rightPanelPosition);
            this.controller = controller;
            MapGameActionToMethod();
            timer = new Timer();
        }
        public int Run()
        {            
            board = new Board(BOARD_HEIGHT, BOARD_WIDTH);
            nextPiece = Piece.GetRandomPiece();
            descendingPiece = Piece.GetRandomPiece();
            score = 0;
            lines = 0;
            delay = INITIAL_DELAY;
            speed = 1;
            gameOver = false;
            descendingPiecePosition = InitialPosition(descendingPiece);
            nextPiecePosition = new Position(6, 0, rightPanelPosition);

            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.CursorVisible = false;
            Render();
            timer.Start();
            while (! gameOver)
            {
                GameAction action = controller.GetInput();
                methodMap[action]();
                if (timer.IsOver(delay)) MoveDown();
                //Keyboard.keyboard.WaitUntilKeyPress();
            }
            timer.Stop();
            Console.CursorVisible = true;
            return score;
        }
        Position InitialPosition(Piece piece)
        {
            return new Position(1, 1 + (BOARD_WIDTH - piece.GetBlocks().GetLength(0)) / 2, boardPosition);
        }
        void MapGameActionToMethod()
        {
            methodMap = new Dictionary<GameAction, Action>();
            methodMap.Add(GameAction.DoNothing, DoNothing);
            methodMap.Add(GameAction.Drop, Drop);
            methodMap.Add(GameAction.Exit, Exit);
            methodMap.Add(GameAction.MoveDown, MoveDown);
            methodMap.Add(GameAction.MoveLeft, MoveLeft);
            methodMap.Add(GameAction.MoveRight, MoveRight);
            methodMap.Add(GameAction.RotateLeft, RotateLeft);
            methodMap.Add(GameAction.RotateRight, RotateRight);
        }
        /// <summary>
        /// Method does literally nothing
        /// </summary>
        void DoNothing() { }
        void MoveRight() 
        {
            descendingPiecePosition.MoveRight();
            if (board.IsCollision(descendingPiece, descendingPiecePosition)) descendingPiecePosition.MoveLeft();
            Render();
        }
        void MoveLeft() 
        {
            descendingPiecePosition.MoveLeft();
            if (board.IsCollision(descendingPiece, descendingPiecePosition)) descendingPiecePosition.MoveRight();
            Render();
        }
        void MoveDown() 
        {
            descendingPiecePosition.MoveDown();
            if(board.IsCollision(descendingPiece, descendingPiecePosition))
            {
                descendingPiecePosition.MoveUp();
                Land();
            }
            timer.Reset();
            Render();
        }
        void Drop() 
        {
            while (!board.IsCollision(descendingPiece, descendingPiecePosition)) descendingPiecePosition.MoveDown();
            descendingPiecePosition.MoveUp();
            Land();
            timer.Reset();
            Render();
        }
        void Land()
        {
            board.AddPiece(descendingPiece, descendingPiecePosition);
            descendingPiece = nextPiece;
            int n = board.ClearLines();
            lines += n;
            for (int i = 0; i < n; i++) score += 10 + i * 5;
            nextPiece = Piece.GetRandomPiece();
            descendingPiecePosition = InitialPosition(descendingPiece);
            if(lines / 10 > speed && speed < 30)
            {
                speed = lines / 10;
                delay = INITIAL_DELAY - 20 * speed;
            }
            if (board.IsCollision(descendingPiece, descendingPiecePosition)) gameOver = true;
        }
        void RotateRight() 
        {
            descendingPiece.RotateR();
            if (board.IsCollision(descendingPiece, descendingPiecePosition))
            {
                WallKick();
                if (board.IsCollision(descendingPiece, descendingPiecePosition))
                {
                    descendingPiece.RotateL();
                    return;
                }
            }
            Render();
        }
        void RotateLeft() 
        {
            descendingPiece.RotateL();
            if (board.IsCollision(descendingPiece, descendingPiecePosition))
            {
                WallKick();
                if (board.IsCollision(descendingPiece, descendingPiecePosition))
                {
                    descendingPiece.RotateR();
                    return;
                }
            }                
            Render();
        }
        void Exit() 
        {
            gameOver = true;
        }

        void WallKick()
        {
            descendingPiecePosition.MoveLeft();
            if (!board.IsCollision(descendingPiece, descendingPiecePosition)) return;
            descendingPiecePosition.MoveRight();
            descendingPiecePosition.MoveRight();
            if (!board.IsCollision(descendingPiece, descendingPiecePosition)) return;
            descendingPiecePosition.MoveLeft();
        }
        void Render()
        {
            DrawBlocks(board.GetBlocks(), boardPosition, '█');
            DrawPreview('▒');
            DrawBlocks(descendingPiece.GetBlocks(), descendingPiecePosition, '█');
            
            DrawText("Score", scoreTextPosition);
            DrawText("Speed", speedTextPosition);
            DrawText("Next", nextTextPosition);
            DrawBlocks(Piece.BLANK.GetBlocks(), nextPiecePosition, '█');
            DrawBlocks(nextPiece.GetBlocks(), nextPiecePosition, '█');
            DrawText(score.ToString(), scorePosition);
            DrawText(speed.ToString(), speedPosition);

        }
        void DrawBlocks(Block[,] blocks, Position position, char symbol)
        {
            Position absolute = position.GetAbsolutePosition();
            for (int i = 0; i < blocks.GetLength(0); i++)
            {
                for (int j = 0; j < blocks.GetLength(1); j++)
                {
                    if (blocks[i, j] != null)
                    {
                        int top = i + absolute.GetTop();
                        int left = j + absolute.GetLeft();
                        Console.SetCursorPosition(left, top);
                        Console.BackgroundColor = Block.EMPTY.GetColor();
                        Console.ForegroundColor = blocks[i, j].GetColor();
                        Console.Write(symbol);
                    }
                }
            }
        }
        void DrawPreview(char symbol)
        {
            // Find position, where piece would land
            Position previewPosition = new Position(descendingPiecePosition.GetTop(), descendingPiecePosition.GetLeft(), boardPosition);
            while (!board.IsCollision(descendingPiece, previewPosition)) previewPosition.MoveDown();
            previewPosition.MoveUp();
            // Draw the piece partially transparent
            DrawBlocks(descendingPiece.GetBlocks(), previewPosition, symbol);

        }
        void DrawText(string text, Position position)
        {
            Position absolute = position.GetAbsolutePosition();
            Console.SetCursorPosition(absolute.GetLeft(), absolute.GetTop());
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(text);
        }
        
    }
}
