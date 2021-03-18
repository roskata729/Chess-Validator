using System;
using System.Collections.Generic;
using System.Text;

namespace ChessValidator
{
    class Bishop : Figure
    {
        public Bishop(int rank, int file, bool side)
        {
            Rank = rank;
            File = file;
            Side = side;
            Label = side ? " WB " : " BB ";
        }
        public override Figure[,] Move(Figure[,] board, int startLocation, int endLocation)
        {

            base.Move(board, startLocation, endLocation);

            return board;
        }
        public override List<string> IsValidMove(Figure[,] board, int startLocation, int endLocation, List<string> invalidMoves)
        {

            char[] letters = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };
            string temp = letters[startLocation % 10].ToString();
            string movement = temp + (board.GetLength(0) - startLocation / 10) + "-" + letters[endLocation % 10] + (board.GetLength(0) - endLocation / 10);

            bool isMovingUp = startLocation / 10 > endLocation / 10;
            bool isMovingLeft = startLocation % 10 > endLocation % 10;
            //Checks if the bishop is moving diagonally
            if (Math.Abs(startLocation / 10 - endLocation / 10) != Math.Abs(startLocation % 10 - endLocation % 10)) { invalidMoves.Add(movement); return invalidMoves; }
            //Checks if the desired spot is already occupied
            else if (board[endLocation / 10, endLocation % 10].Side == board[startLocation / 10, startLocation % 10].Side
                && board[endLocation / 10, endLocation % 10].GetType().Name != "Figure") { invalidMoves.Add(movement); return invalidMoves; }
            else
            {
                for (int i = 1; i <= Math.Abs(startLocation / 10 - endLocation / 10); i++)
                {   //upper-left diagonal
                    if (isMovingLeft && isMovingUp)
                    {
                        if (board[startLocation / 10 - i, startLocation % 10 - i].GetType().Name != "Figure") { invalidMoves.Add(movement); return invalidMoves; }
                        continue;
                    }//down-left diagonal
                    if (isMovingLeft && !isMovingUp)
                    {
                        if (board[startLocation / 10 + i, startLocation % 10 - i].GetType().Name != "Figure") { invalidMoves.Add(movement); return invalidMoves; }
                        continue;
                    }//upper-right diagonal
                    else if (!isMovingLeft && isMovingUp)
                    {
                        if (board[startLocation / 10 - i, startLocation % 10 + i].GetType().Name != "Figure") { invalidMoves.Add(movement); return invalidMoves; }
                        continue;
                    }//down-right diagonal
                    else if (!isMovingLeft && !isMovingUp)
                    {
                        if (board[startLocation / 10 + i, startLocation % 10 + i].GetType().Name != "Figure") { invalidMoves.Add(movement); return invalidMoves; }
                        continue;
                    }
                    else { invalidMoves.Add(movement); return invalidMoves; }
                }

                return invalidMoves;
            }
        }
    }
}
