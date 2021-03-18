using System;
using System.Collections.Generic;
using System.Text;

namespace ChessValidator
{

    class Figure
    {
        private int rank; // rows
        private int file; //columns
        private bool side; // 1 - White, 0 - Black
        private bool hasMoved = false; // if the figure has ever moved
        private string label = "    "; // the output in the console
        private int enPassant = 0; 
            
        public bool isAKing = false;

        public int Rank { get => rank; set => rank = value; }
        public int File { get => file; set => file = value; }
        public bool Side { get => side; set => side = value; }
        public bool HasMoved { get => hasMoved; set => hasMoved = value; }
        public string Label { get => label; set => label = value; }
        public int EnPassant { get => enPassant; set => enPassant = value; }

        public virtual Figure[,] Move(Figure[,] board, int startLocation, int endLocation)
        {
            board[endLocation / 10, endLocation % 10] = board[startLocation / 10, startLocation % 10];
            board[startLocation / 10, startLocation % 10] = new Figure();
            board[endLocation / 10, endLocation % 10].HasMoved = true;
            Rank = endLocation / 10;
            File = endLocation % 10;
            return board;
        }
        public virtual List<string> IsValidMove(Figure[,] board, int startLocation, int endLocation, List<string> invalidMoves) {
            return invalidMoves;
        }

        public virtual bool IsKingThreatened(Figure[,] board, int kingPosX, int kingPosY, bool side) {
            return false;
        }
    }
    
}


