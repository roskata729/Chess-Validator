using System;
using System.Collections.Generic;
using System.Text;

namespace ChessValidator
{
    class Queen : Figure
    {
        public Queen(int rank, int file, bool side)
        {
            Rank = rank;
            File = file;
            Side = side;
            Label = side ? " WQ " : " BQ ";
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
            bool oppositeSides = board[startLocation / 10, startLocation % 10].Side != board[endLocation / 10, endLocation % 10].Side;

            //Checks if the queen is moving anywhere
            if (startLocation / 10 == endLocation / 10 && startLocation % 10 == endLocation % 10) { invalidMoves.Add(movement); return invalidMoves; }
            //Checks if the given position is occupied by a friendly figure
            else if(!oppositeSides && board[endLocation / 10, endLocation % 10].GetType().Name != "Figure") { invalidMoves.Add(movement); return invalidMoves; }
            else
            {
                //Checks if the queen is moving vertically
                if (startLocation % 10 == endLocation % 10)
                {
                    for (int i = 1; i <= Math.Abs(startLocation / 10 - endLocation / 10); i++)
                    {
                        //Checks if the queen is moving up vertically
                        if (isMovingUp)
                        {
                            if (board[startLocation / 10 - i, startLocation % 10].GetType().Name != "Figure" && !oppositeSides) 
                                { invalidMoves.Add(movement); return invalidMoves; }
                            continue;
                        }
                        //Moving down vertically
                        else
                        {
                            if (board[startLocation / 10 + i, startLocation % 10].GetType().Name != "Figure" && !oppositeSides)  
                                { invalidMoves.Add(movement); return invalidMoves; }
                        }

                    }
                }//Checks if the queen is moving horizontally
                else if (startLocation / 10 == endLocation / 10)
                {
                    for (int i = 1; i <= Math.Abs(startLocation % 10 - endLocation % 10); i++)
                    {
                        //Checks if the queen is moving left 
                        if (isMovingLeft)
                        {
                            if (board[startLocation / 10, startLocation % 10 - i].GetType().Name != "Figure" && !oppositeSides) 
                                  { invalidMoves.Add(movement); return invalidMoves; }
                        }
                        //Checks if the queen is moving right
                        else
                        {
                            if (board[startLocation / 10, startLocation % 10 + i].GetType().Name != "Figure" && !oppositeSides) 
                                    { invalidMoves.Add(movement); return invalidMoves; }
                        }
                    }

                }
                //Diagonally
                else if (Math.Abs(startLocation/10-endLocation/10)==Math.Abs(startLocation%10-endLocation%10)) {
                    for (int i = 1; i <= Math.Abs(startLocation / 10 - endLocation / 10); i++)
                    {   //upper-left diagonal
                        if (isMovingLeft && isMovingUp)
                        {
                            if (board[startLocation / 10 - i, startLocation % 10 - i].GetType().Name != "Figure" && !oppositeSides) 
                                    { invalidMoves.Add(movement); return invalidMoves; }
                            continue;
                        }//down-left diagonal
                        else if (isMovingLeft && !isMovingUp)
                        {
                            if (board[startLocation / 10 + i, startLocation % 10 - i].GetType().Name != "Figure" && !oppositeSides) {   
                                invalidMoves.Add(movement); return invalidMoves; }
                            continue;
                        }//upper-right diagonal
                        else if (!isMovingLeft && isMovingUp)
                        {
                            if (board[startLocation / 10 - i, startLocation % 10 + i].GetType().Name != "Figure" && !oppositeSides) 
                                { invalidMoves.Add(movement); return invalidMoves; }
                            continue;
                        }//down-right diagonal
                        else if (!isMovingLeft && !isMovingUp)
                        {
                            if (board[startLocation / 10 + i, startLocation % 10 + i].GetType().Name != "Figure" && !oppositeSides) 
                                { invalidMoves.Add(movement); return invalidMoves; }
                            continue;
                        }
                        else { invalidMoves.Add(movement); return invalidMoves; }
                    }
                }
                else { invalidMoves.Add(movement); return invalidMoves; }
            }

            return invalidMoves;

        }
    }
}
