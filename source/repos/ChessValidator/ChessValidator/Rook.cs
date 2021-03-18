using System;
using System.Collections.Generic;
using System.Text;

namespace ChessValidator
{
    class Rook : Figure
    {
        public Rook(int rank, int file, bool side)
        {
            Rank = rank;
            File = file;
            Side = side;
            Label = side ? " WR " : " BR ";
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

            //Checks if the desired spot is already occupied
            if (board[endLocation / 10, endLocation % 10].Side == board[startLocation / 10, startLocation % 10].Side
                && board[endLocation/10,endLocation%10].GetType().Name != "Figure") { invalidMoves.Add(movement); return invalidMoves; }
            else {
                int condition = startLocation / 10 == endLocation / 10 ? Math.Abs(startLocation % 10 - endLocation % 10) : Math.Abs(startLocation / 10 - endLocation / 10);
                bool isMovingLeft = startLocation % 10 > endLocation % 10;
                bool isMovingUp = startLocation / 10 > endLocation / 10;
                
                //Checks if the spots on the route to the desired spots are not occupied
                for (int i = 1; i < condition ; i++)
                {
                    //if the rook is moving left
                    if (isMovingLeft && startLocation / 10 == endLocation / 10)
                    {
                        if (board[startLocation / 10, startLocation % 10 - i].GetType().Name != "Figure") { invalidMoves.Add(movement); return invalidMoves; }
                    }
                    //if the rook is moving right
                    else if (!isMovingLeft && startLocation / 10 == endLocation / 10)
                    {
                        if (board[startLocation / 10, startLocation % 10 + i].GetType().Name != "Figure") { invalidMoves.Add(movement); return invalidMoves; }
                    }
                    //if the rook is moving up
                    else if (isMovingUp & startLocation % 10 == endLocation % 10)
                    {
                        if (board[startLocation / 10 - i, startLocation % 10].GetType().Name != "Figure") { invalidMoves.Add(movement); return invalidMoves; }
                    }
                    //if the rook is moving down
                    else if (!isMovingUp & startLocation % 10 == endLocation % 10)
                    {
                        if (board[startLocation / 10 + i, startLocation % 10].GetType().Name != "Figure") { invalidMoves.Add(movement); return invalidMoves; }
                    }
                    else { invalidMoves.Add(movement); return invalidMoves; };

                }
            }

            return invalidMoves;
        }
    }
}
