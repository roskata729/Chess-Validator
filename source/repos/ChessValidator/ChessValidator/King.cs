using System;
using System.Collections.Generic;
using System.Text;

namespace ChessValidator
{
    class King : Figure
    {
        private int castleRookStart;
        private int castleRookEnd;

        public int CastleRookStart { get => castleRookStart; set => castleRookStart = value; }
        public int CastleRookEnd { get => castleRookEnd; set => castleRookEnd = value; }

        public King(int rank, int file, bool side)
        {
            Rank = rank;
            File = file;
            Side = side;
            Label = side ? " WK " : " BK ";
            isAKing = true;
        }
        public override Figure[,] Move(Figure[,] board, int startLocation, int endLocation)
        {
            base.Move(board, startLocation, endLocation);

            //board[startLocation/10, startLocation % 10].IsValidMove(board, startLocation, endLocation, invalidMoves);

            return board;
        }
        public override List<string> IsValidMove(Figure[,] board, int startLocation, int endLocation, List<string> invalidMoves)
        {
            King king = (King)board[startLocation / 10, startLocation % 10];
            char[] letters = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };
            string temp = letters[startLocation % 10].ToString();
            string movement = temp + (board.GetLength(0) - startLocation / 10) + "-" + letters[endLocation % 10] + (board.GetLength(0) - endLocation / 10);
            bool oppositeSides = board[startLocation / 10, startLocation % 10].Side != board[endLocation / 10, endLocation % 10].Side;
            //if the king moves more than 1 square
            if (Math.Abs(startLocation / 10 - endLocation / 10) >= 2 || (Math.Abs(startLocation % 10 - endLocation % 10) >= 2 &&
                !king.IsCastlePossible(board, startLocation, endLocation)))
                { invalidMoves.Add(movement); return invalidMoves; }
            //if the given spot is the same as the starter spot
            if (startLocation / 10 == endLocation / 10 && startLocation % 10 == endLocation % 10) { invalidMoves.Add(movement); return invalidMoves; }
            else if (Math.Abs(startLocation / 10 - endLocation / 10) == 0) {
                if (startLocation % 10 - endLocation % 10 < 0)
                {//Horizontal right
                   if (board[startLocation / 10, startLocation % 10 + 1].GetType().Name != "Figure" && !oppositeSides)
                       { invalidMoves.Add(movement); return invalidMoves; } 
                }
                else if (startLocation % 10 - endLocation % 10 > 0)
                {//Horizontal left
                    if (board[startLocation / 10, startLocation % 10 - 1].GetType().Name != "Figure" && !oppositeSides)
                    { invalidMoves.Add(movement); return invalidMoves; }
                }
            }
            else if (startLocation % 10 - endLocation % 10 == 0){
                if (startLocation / 10 - endLocation / 10 < 0)
                {
                    //Vertical down
                    if (board[startLocation / 10 + 1, startLocation % 10].GetType().Name != "Figure" && !oppositeSides)
                    { invalidMoves.Add(movement); return invalidMoves; }
                }
                else if (startLocation / 10 - endLocation / 10 > 0)
                {
                    //Vertical up
                    if (board[startLocation / 10 - 1, startLocation % 10].GetType().Name != "Figure" && !oppositeSides)
                    { invalidMoves.Add(movement); return invalidMoves; }
                }
            }
            else if (startLocation / 10 - endLocation / 10 == 1)
            {
                if (startLocation % 10 - endLocation % 10 == 1)
                {
                    //Up Left
                    if (board[startLocation / 10 - 1, startLocation % 10 - 1].GetType().Name != "Figure" && !oppositeSides)
                    { invalidMoves.Add(movement); return invalidMoves; }
                }
                else if (startLocation % 10 + endLocation % 10 == 1)
                {
                    //Up Right
                    if (board[startLocation / 10 - 1, startLocation % 10 + 1].GetType().Name != "Figure" && !oppositeSides)
                    { invalidMoves.Add(movement); return invalidMoves; }
                }
            }
            else if (endLocation / 10 - startLocation / 10 == 1)
            {
                if (startLocation % 10 - endLocation % 10 == 1)
                {
                    //Down Left
                    if (board[startLocation / 10 + 1, startLocation % 10 - 1].GetType().Name != "Figure" && !oppositeSides)
                    { invalidMoves.Add(movement); return invalidMoves; }
                }
                else if (startLocation % 10 + endLocation % 10 == 1)
                {
                    //Down Right
                    if (board[startLocation / 10 + 1, startLocation % 10 + 1].GetType().Name != "Figure" && !oppositeSides)
                    { invalidMoves.Add(movement); return invalidMoves; }
                }
            }
            return invalidMoves;
        }

        public override bool IsKingThreatened(Figure[,] board, int kingPosX, int kingPosY, bool side)
        {
            //Vertical Up
            for (int i = 1; i < Math.Abs(board.GetLength(0) - kingPosX); i++)
            {
                if (kingPosX - i < 0) break;
                if (i == 1)
                {
                    if (board[kingPosX - i, kingPosY].isAKing) return true;
                }
                if (board[kingPosX - i, kingPosY].GetType().Name != "Figure" && board[kingPosX, kingPosY].Side == board[kingPosX - i, kingPosY].Side) break;
                else if (board[kingPosX - i, kingPosY].GetType().Name != "Figure" && board[kingPosX - i, kingPosY].Side != board[kingPosX, kingPosY].Side)
                {
                    if (board[kingPosX - i, kingPosY].GetType().Name == "Queen" || board[kingPosX - i, kingPosY].GetType().Name == "Rook") return true;
                    else break;
                }
            }
            //Vertical Down
            for (int i = 1; i + kingPosX < board.GetLength(0); i++)
            {
                if (kingPosX - i == board.GetLength(0)) break;
                if (i == 1)
                {
                    if (board[kingPosX + i, kingPosY].isAKing) return true;
                }
                if (board[kingPosX + i, kingPosY].GetType().Name != "Figure" && board[kingPosX, kingPosY].Side == board[kingPosX + i, kingPosY].Side) break;
                else if (board[kingPosX + i, kingPosY].GetType().Name != "Figure" && board[kingPosX + i, kingPosY].Side != board[kingPosX, kingPosY].Side)
                {
                    if (board[kingPosX + i, kingPosY].GetType().Name == "Queen" || board[kingPosX + i, kingPosY].GetType().Name == "Rook") return true;
                    else break;
                }
            }
            //Horizontal right
            for (int i = 1; i + kingPosY < board.GetLength(1); i++)
            {
                if (kingPosY + i == board.GetLength(1)) break;
                if (i == 1)
                {
                    if (board[kingPosX, kingPosY + i].isAKing) return true;
                }
                if (board[kingPosX, kingPosY + i].GetType().Name != "Figure" && board[kingPosX, kingPosY].Side == board[kingPosX, kingPosY + i].Side) break;
                else if (board[kingPosX, kingPosY + i].GetType().Name != "Figure" && board[kingPosX, kingPosY + i].Side != board[kingPosX, kingPosY].Side)
                {
                    if (board[kingPosX, kingPosY + i].GetType().Name == "Queen" || board[kingPosX, kingPosY + i].GetType().Name == "Rook") return true;
                    else break;
                }
            }
            //Horizontal left
            for (int i = 1; 0 <= kingPosY - i; i++)
            {
                if (kingPosY - i < 0) break;
                if (i == 1)
                {
                    if (board[kingPosX, kingPosY - i].isAKing) return true;
                }
                if (board[kingPosX, kingPosY - i].GetType().Name != "Figure" && board[kingPosX, kingPosY].Side == board[kingPosX, kingPosY - i].Side) break;
                else if (board[kingPosX, kingPosY - i].GetType().Name != "Figure" && board[kingPosX, kingPosY - i].Side != board[kingPosX, kingPosY].Side)
                {
                    if (board[kingPosX, kingPosY - i].GetType().Name == "Queen" || board[kingPosX, kingPosY - i].GetType().Name == "Rook") return true;
                    else break;
                }

            }
            // Down Right
            int count = 1;
            do
            {
                if (count + kingPosX == board.GetLength(0) || count + kingPosY == board.GetLength(1)) break;
                if (count == 1)
                {
                    if (board[kingPosX + count, kingPosY + count].isAKing) return true;
                    if (board[kingPosX + count, kingPosY + count].GetType().Name == "Pawn" && board[kingPosX + count, kingPosY + count].Side != board[kingPosX,kingPosY].Side) return true;
                }
                if (board[kingPosX + count, kingPosY + count].GetType().Name != "Figure" && board[kingPosX, kingPosY].Side == board[kingPosX + count, kingPosY + count].Side) break;
                else if (board[kingPosX + count, kingPosY + count].GetType().Name != "Figure" && board[kingPosX + count, kingPosY + count].Side != board[kingPosX, kingPosY].Side)
                {
                    if (board[kingPosX + count, kingPosY + count].GetType().Name == "Queen" || board[kingPosX + count, kingPosY + count].GetType().Name == "Bishop") return true;
                    else break;
                }
                count++;
            } while (count + kingPosX < board.GetLength(0) && count + kingPosY < board.GetLength(1));
            //Up Right
            count = 1;
            do
            {
                if (kingPosX - count < 0 || count + kingPosY == board.GetLength(1)) break;
                if (count == 1)
                {
                    if (board[kingPosX - count, kingPosY + count].isAKing) return true;
                    if (board[kingPosX - count, kingPosY + count].GetType().Name == "Pawn" && board[kingPosX - count, kingPosY + count].Side != board[kingPosX, kingPosY].Side) return true;
                }
                if (board[kingPosX - count, kingPosY + count].GetType().Name != "Figure" && board[kingPosX, kingPosY].Side == board[kingPosX - count, kingPosY + count].Side) break;
                else if (board[kingPosX - count, kingPosY + count].GetType().Name != "Figure" && board[kingPosX - count, kingPosY + count].Side != board[kingPosX, kingPosY].Side)
                {
                    if (board[kingPosX - count, kingPosY + count].GetType().Name == "Queen" || board[kingPosX - count, kingPosY + count].GetType().Name == "Bishop") return true;
                    else break;
                }
                count++;
            } while (kingPosX - count >= 0 && count + kingPosY < board.GetLength(1));
            //Up Left
            count = 1;
            do
            {
                if (kingPosX - count < 0 || kingPosY - count < 0) break;
                if (count == 1)
                {
                    if (board[kingPosX - count, kingPosY - count].isAKing) return true;
                    if (board[kingPosX - count, kingPosY - count].GetType().Name == "Pawn" && board[kingPosX - count, kingPosY - count].Side != board[kingPosX, kingPosY].Side) 
                        return true;
                }
                if (board[kingPosX - count, kingPosY - count].GetType().Name != "Figure" && board[kingPosX, kingPosY].Side == board[kingPosX - count, kingPosY - count].Side) break;
                else if (board[kingPosX - count, kingPosY - count].GetType().Name != "Figure" && board[kingPosX - count, kingPosY - count].Side != board[kingPosX, kingPosY].Side)
                {
                    if (board[kingPosX - count, kingPosY - count].GetType().Name == "Queen" || board[kingPosX - count, kingPosY - count].GetType().Name == "Bishop") return true;
                    else break;
                }
                count++;
            } while (kingPosX - count >= 0 && kingPosY - count >= 0);
            //Down Left
            count = 1;
            do
            {
                if (count + kingPosX == board.GetLength(0) || kingPosY - count < 0) break;
                if (count == 1)
                {
                    if (board[kingPosX + count, kingPosY - count].isAKing) return true;
                    if (board[kingPosX + count, kingPosY - count].GetType().Name == "Pawn" && board[kingPosX + count, kingPosY - count].Side != board[kingPosX, kingPosY].Side) 
                        return true;
                }
                if (board[kingPosX + count, kingPosY - count].GetType().Name != "Figure" && board[kingPosX, kingPosY].Side == board[kingPosX + count, kingPosY - count].Side) break;
                else if (board[kingPosX + count, kingPosY - count].GetType().Name != "Figure" && board[kingPosX + count, kingPosY - count].Side != board[kingPosX, kingPosY].Side)
                {
                    if (board[kingPosX + count, kingPosY - count].GetType().Name == "Queen" || board[kingPosX + count, kingPosY - count].GetType().Name == "Bishop") return true;
                    else break;
                }
                count++;
            } while (kingPosX + count < board.GetLength(0) && kingPosY - count >= 0);

            //Checks if there is a knight attacking the king 
            if (kingPosX - 1 >= 0 && kingPosY - 2 >= 0) { if (board[kingPosX - 1, kingPosY - 2].Side != side && board[kingPosX - 1, kingPosY - 2].GetType().Name == "Knight") return true; }
            if (kingPosX - 1 >= 0 && kingPosY + 2 < board.GetLength(1)) { if (board[kingPosX - 1, kingPosY + 2].Side != side && board[kingPosX - 1, kingPosY + 2].GetType().Name == "Knight") return true; }
            if (kingPosX + 1 < board.GetLength(0) && kingPosY - 2 >= 0) { if (board[kingPosX + 1, kingPosY - 2].Side != side && board[kingPosX + 1, kingPosY - 2].GetType().Name == "Knight") return true; }
            if (kingPosX + 1 < board.GetLength(0) && kingPosY + 2 < board.GetLength(1))
            { if (board[kingPosX + 1, kingPosY + 2].Side != side && board[kingPosX + 1, kingPosY + 2].GetType().Name == "Knight") return true; }

            if (kingPosX - 2 >= 0 && kingPosY - 1 >= 0) { if (board[kingPosX - 2, kingPosY - 1].Side != side && board[kingPosX - 2, kingPosY - 1].GetType().Name == "Knight") return true; }
            if (kingPosX - 2 >= 0 && kingPosY + 1 < board.GetLength(1)) { if (board[kingPosX - 2, kingPosY + 1].Side != side && board[kingPosX - 2, kingPosY + 1].GetType().Name == "Knight") return true; }
            if (kingPosX + 2 < board.GetLength(0) && kingPosY - 1 >= 0) { if (board[kingPosX + 2, kingPosY - 1].Side != side && board[kingPosX + 2, kingPosY - 1].GetType().Name == "Knight") return true; }
            if (kingPosX + 2 < board.GetLength(0) && kingPosY + 1 < board.GetLength(1)) { if (board[kingPosX + 2, kingPosY + 1].Side != side && board[kingPosX + 2, kingPosY + 1].GetType().Name == "Knight") return true; }

            return false;
        }
        protected bool IsSpotPinned(Figure[,] board,int x, int y, bool side) {
            if (board[x,y].IsKingThreatened(board, x, y, side)) return true;
            return false;
        }
        public bool IsCastlePossible(Figure[,] board, int startLocation, int endLocation)
        {
            int count = 0;

            //white queen side castle
            if (endLocation == 72)
            {
                if (board[startLocation / 10, startLocation % 10].isAKing && board[7, 0].GetType().Name == "Rook")
                {
                    if (board[startLocation / 10, endLocation % 10].Side == board[7, 0].Side)
                    {
                        if (!board[startLocation / 10, startLocation % 10].HasMoved && !board[7, 0].HasMoved)
                        {

                            King king = (King)board[7, 4];
                            do
                            {
                                //Checks if the spots between the king and the rook are occupied
                                if (board[7, 4 - count - 1].GetType().Name != "Figure")
                                {
                                    return false;
                                }//Checks if the spots the king moves on are pinned
                                else if (king.IsSpotPinned(board, 7, 4 - count, true))
                                {
                                    return false;
                                }
                                count++;
                            } while (count <= 2);
                            this.CastleRookStart = 70;
                            this.CastleRookEnd = 73;
                            return true;
                        }
                    }
                }
            }
            //white king side castle
            if (endLocation == 76)
            {
                if (board[startLocation / 10, startLocation % 10].isAKing && board[7, 7].GetType().Name == "Rook")
                {
                    if (board[startLocation / 10, startLocation % 10].Side == board[7, 7].Side)
                    {
                        if (!board[startLocation / 10, startLocation % 10].HasMoved && !board[7, 7].HasMoved)
                        {

                            King king = (King)board[7, 4];
                            count = 0;
                            do
                            {
                                //Checks if the spots between the king and the rook are occupied
                                if (board[7, 4 + count + 1].GetType().Name != "Figure")
                                {
                                    return false;
                                }//Checks if the spots the king moves on are pinned
                                else if (king.IsSpotPinned(board, 7, 4 + count, true))
                                {
                                    return false;
                                }
                                count++;
                            } while (count < 2);
                            this.CastleRookStart = 77;
                            this.CastleRookEnd = 75;
                            return true;
                        }
                    }
                }
            }
            //black king side castle
            if (endLocation == 6)
            {
                if (board[startLocation / 10, startLocation % 10].isAKing && board[0, 7].GetType().Name == "Rook")
                {
                    if (board[startLocation / 10, endLocation % 10].Side == board[0, 7].Side)
                    {
                        if (!board[startLocation / 10, startLocation % 10].HasMoved && !board[0, 7].HasMoved)
                        {

                            King king = (King)board[0, 4];
                            count = 0;
                            do
                            {
                                //Checks if the spots between the king and the rook are occupied
                                if (board[0, 4 + count + 1].GetType().Name != "Figure")
                                {
                                    return false;
                                }//Checks if the spots the king moves on are pinned
                                else if (king.IsSpotPinned(board, 0, 4 + count, false))
                                {
                                    return false;
                                }
                                count++;
                            } while (count < 2);
                            this.CastleRookStart = 7;
                            this.CastleRookEnd = 5;
                            return true;
                        }
                    }
                }
            }
            //black queen side castle
            if (endLocation == 2)
            {
                if (board[startLocation / 10, startLocation % 10].isAKing && board[0, 0].GetType().Name == "Rook")
                {
                    if (board[startLocation/10,endLocation%10].Side == board[0, 0].Side) { 
                        if (!board[startLocation / 10, startLocation % 10].HasMoved && !board[0, 0].HasMoved)
                        {
                            King king = (King)board[0, 4];
                            count = 0;
                            do
                            {
                                //Checks if the spots between the king and the rook are occupied
                                if (board[0, 4 - count - 1].GetType().Name != "Figure")
                                {
                                    return false;
                                }//Checks if the spots the king moves on are pinned
                                else if (king.IsSpotPinned(board, 0, 4 - count, false))
                                {
                                    return false;
                                }
                                count++;
                            } while (count <= 2);
                            this.CastleRookStart = 0;
                            this.CastleRookEnd = 3;
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
