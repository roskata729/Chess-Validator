using System;
using System.Collections.Generic;
using System.Text;

namespace ChessValidator
{
    class Knight : Figure
    {
        public Knight(int rank, int file, bool side) 
        {
            Rank = rank;
            File = file;
            Side = side;
            Label = side ? " WN " : " BN ";
        }

        public override Figure[,] Move(Figure[,] board, int startLocation, int endLocation) {

            base.Move(board, startLocation, endLocation);

            return board;
        }
        public override List<string> IsValidMove(Figure[,] board, int startLocation, int endLocation, List<string> invalidMoves)
        {
            char[] letters = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };
            string temp = letters[startLocation % 10].ToString();
            string movement = temp + (board.GetLength(0) - startLocation / 10) + "-" + letters[endLocation % 10] + (board.GetLength(0) - endLocation / 10);

            //Checks if the desired spot is already occupied
            if (board[startLocation / 10, startLocation % 10].Side == board[endLocation / 10, endLocation % 10].Side
                && (!(board[endLocation / 10, endLocation % 10].GetType().Name == "Figure")))
                 { invalidMoves.Add(movement); return invalidMoves; }
            else
            {
                bool isValid = false;
                // Validating the L shape move of the knight
                if (endLocation / 10 - 1 == startLocation / 10 && endLocation % 10 - 2 == startLocation % 10) isValid = true;
                if (endLocation / 10 - 1 == startLocation / 10 && endLocation % 10 + 2 == startLocation % 10) isValid = true;
                if (endLocation / 10 + 1 == startLocation / 10 && endLocation % 10 - 2 == startLocation % 10) isValid = true;
                if (endLocation / 10 + 1 == startLocation / 10 && endLocation % 10 + 2 == startLocation % 10) isValid = true;

                if (endLocation / 10 - 2 == startLocation / 10 && endLocation % 10 - 1 == startLocation % 10) isValid = true;
                if (endLocation / 10 - 2 == startLocation / 10 && endLocation % 10 + 1 == startLocation % 10) isValid = true;
                if (endLocation / 10 + 2 == startLocation / 10 && endLocation % 10 - 1 == startLocation % 10) isValid = true;
                if (endLocation / 10 + 2 == startLocation / 10 && endLocation % 10 + 1 == startLocation % 10) isValid = true;


                if (!isValid) invalidMoves.Add(movement);
            }
             return invalidMoves;
        }
    }
}
