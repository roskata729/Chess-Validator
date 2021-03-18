using System;
using System.Collections.Generic;
using System.Text;

namespace ChessValidator
{
    class Pawn : Figure
    {

        public Pawn(int rank, int file, bool side)
        {
            Rank = rank;
            File = file;
            Side = side;
            Label = side ? " WP " : " BP ";
        }
        
        public override Figure[,] Move(Figure[,] board, int startLocation, int endLocation) 
        {
            base.Move(board, startLocation, endLocation);
            
            //board[startLocation/10, startLocation % 10].IsValidMove(board, startLocation, endLocation, invalidMoves);

            return board;
        }
        public override List<string> IsValidMove(Figure[,] board, int startLocation, int endLocation, List<string> invalidMoves)
        {
            char[] letters = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };
            //Whites
            if (Side)
            {

                string temp = letters[startLocation % 10].ToString();
                string movement = temp + (board.GetLength(0) - startLocation / 10) + "-" + letters[endLocation % 10] + (board.GetLength(0) - endLocation / 10);
                // Double pawn movement
                if (startLocation / 10 - endLocation / 10 == 2)
                {
                    if ((board[startLocation / 10, startLocation % 10].HasMoved)) { invalidMoves.Add(movement); return invalidMoves; }
                    else
                    {
                        for (int i = 1; i <= startLocation / 10 - endLocation / 10; i++)
                        {
                            if (board[startLocation / 10 - i, startLocation % 10].GetType().Name == "Figure")
                            {
                                if(i==2) board[endLocation / 10 + 1, endLocation % 10].EnPassant = 11;
                                continue;
                            }
                            else { invalidMoves.Add(movement); return invalidMoves; }
                        }
                        
                    }
                    if (board[endLocation / 10, endLocation % 10].GetType().Name != "Figure") { invalidMoves.Add(movement); return invalidMoves; }
                }
                //Single pawn movement
                else if (startLocation / 10 - endLocation / 10 == 1)
                {
                    // if the pawn attacks
                    if (Math.Abs(startLocation % 10 - endLocation % 10) == 1)
                    {
                        if (board[endLocation / 10, endLocation % 10].GetType().Name != "Figure")
                        {
                            if (board[endLocation / 10, endLocation % 10].Side) { invalidMoves.Add(movement); return invalidMoves; } //checks if the pawn did attack something
                        }
                        else
                        {//En Passant
                            if (board[endLocation / 10, endLocation % 10].EnPassant == 10)
                            {
                              //  board[startLocation / 10, endLocation % 10] = board[startLocation / 10, startLocation % 10];
                                board[endLocation / 10 + 1, endLocation % 10] = new Figure();
                            }
                            else { invalidMoves.Add(movement); return invalidMoves; }
                        }

                    }
                    // if the pawn attempts to move over another figure in a straight line
                    if (board[startLocation / 10 - 1, startLocation % 10].GetType().Name != "Figure") { invalidMoves.Add(movement); return invalidMoves; }

                }
                else
                {
                    invalidMoves.Add(movement); return invalidMoves;
                }

            }
            //Blacks
            else
            {
                string temp = letters[startLocation % 10].ToString();
                string movement = temp + (board.GetLength(0) - startLocation / 10) + "-" + letters[endLocation % 10] + (board.GetLength(0) - endLocation / 10);
                // Double pawn movement
                if (endLocation / 10 - startLocation / 10 == 2)
                {
                    if ((board[startLocation / 10, startLocation % 10].HasMoved)) { invalidMoves.Add(movement); return invalidMoves; }
                    else
                    {
                        for (int i = 1; i <= endLocation / 10 - startLocation / 10; i++)
                        {
                            if (board[i + startLocation / 10, startLocation % 10].GetType().Name == "Figure")
                            {
                                if(i==2) board[endLocation / 10 - 1, endLocation % 10].EnPassant = 10;
                                continue;
                            }
                            else { invalidMoves.Add(movement); return invalidMoves; }
                        }
                        
                    }
                    if (board[endLocation / 10, endLocation % 10].GetType().Name != "Figure") { invalidMoves.Add(movement); return invalidMoves; }
                }
                //Single pawn movement
                else if (endLocation / 10 - startLocation / 10 == 1)
                {
                    // if the pawn attacks
                    if (Math.Abs(startLocation % 10 - endLocation % 10) == 1)
                    {
                        if (board[endLocation / 10, endLocation % 10].GetType().Name != "Figure")
                        {
                            if (!board[endLocation / 10, endLocation % 10].Side)
                            {
                                invalidMoves.Add(movement); return invalidMoves;//checks if the pawn did attack something
                            }
                        }
                        //En Passant
                        else
                        {
                            if (board[endLocation / 10, endLocation % 10].EnPassant == 11)
                            {
                                // board[endLocation / 10 + 1, endLocation % 10] = board[startLocation / 10, startLocation % 10];
                                board[endLocation / 10 - 1, endLocation % 10] = new Figure();
                            }
                        }
                        // if the pawn attempts to move over another figure in a straight line
                        
                    }
                    else if (board[startLocation / 10 + 1, startLocation % 10].GetType().Name != "Figure") { invalidMoves.Add(movement); return invalidMoves; }
                    else
                    {
                        invalidMoves.Add(movement); return invalidMoves;
                    }
                }
            }
                return invalidMoves;
            
        }
    }

}
