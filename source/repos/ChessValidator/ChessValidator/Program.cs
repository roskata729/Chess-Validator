using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessValidator
{
    class Program
    {
        /* 
            Figure Codes:

                Empty Spot - 0

                White's:
                    King - 1
                    Queen - 2
                    Pawn - 3
                    Rook - 4
                    Knight - 5
                    Bishop - 6
                Black's:
                    King - 7
                    Queen - 8
                    Pawn - 9
                    Rook - 10
                    Knight - 11
                    Bishop - 12
         */
        static Figure[,] GenerateBoard()
        {
            Figure[,] board = new Figure[8, 8];
            for (int rank = board.GetLength(0) - 1; rank >= 0; rank--)
            {
                for (int file = 0; file < board.GetLength(1); file++)
                {

                    if (rank == 7 || rank == 0)
                    {
                        //Creating Bishops
                        if (file == 2 || file == 5)
                        {
                            board[rank, file] = new Bishop(rank, file, rank == 7);
                        }
                        //Creating Knights
                        if (file == 1 || file == 6)
                        {
                            board[rank, file] = new Knight(rank, file, rank == 7);
                        }
                        //Creating Rooks
                        if (file == 0 || file == 7)
                        {
                            board[rank, file] = new Rook(rank, file, rank == 7);
                        }
                        //Creating Queens
                        if (file == 3)
                        {
                            board[rank, file] = new Queen(rank, file, rank == 7);
                        }
                        //Creating Kings
                        if (file == 4)
                        {
                            board[rank, file] = new King(rank, file, rank == 7);
                        }
                    }

                    //Creating Pawns
                    else if (rank == 1 || rank == 6)
                    {
                        board[rank, file] = new Pawn(rank, file, rank == 6);
                    }
                    //Filling up the free spots
                    else
                    {
                        board[rank, file] = new Figure();
                    }
                }
            }
            return board;
        }

        static void VisualiseBoard(Figure[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    Console.Write("|");
                    Console.Write(board[i, j].Label);
                }
                Console.Write("|");
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            for (int i = 0; i < 41; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }

        static int[] FormatInputString(string inputString)
        {
            string[] moves = inputString.Split(' ', '-');
            int[] outputString = new int[moves.Length];
            for (int i = 0; i < moves.Length; i++)
            {
                char temp = (char)((char)moves[i][0] - 49);
                
                moves[i] =  moves[i][1] + temp.ToString();     // !
                int a = int.Parse(moves[i]);
                int b = a % 10;
                a = Math.Abs(a/10 - 8); // 
                a = a*10 + b;
                outputString[moves.Length-1-i] = a;
            }

            return outputString;
        }
        
        static DoubleInt LocateThe2Kings(Figure[,] board) {
            DoubleInt location = new DoubleInt(0,0);
            for (int p = 0; p < board.GetLength(0); p++)
            {
                for (int n = 0; n < board.GetLength(1); n++)
                {
                    if (board[p, n].isAKing)
                    {
                        if (board[p, n].Side) location.First = p * 10 + n; //white king
                        else location.Second = p * 10 + n; //black king
                    }
                }
            }
            return location;
        }


        public struct DoubleInt {
            public DoubleInt(int first, int second)
            {
                First = first;
                Second = second;
            }
            public int First { get; set; }
            public int Second { get; set; }
        }
        //Disables the EnPassant if it's not used the next move by the enemy side
        static Figure[,] clearEnPassantMove(Figure[,] board,int side) {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j].EnPassant == side) {
                        board[i, j].EnPassant = 0;
                    }
                }

            }
            return board;
        }


        static void Main()
        {
            Console.Write("Enter amount of moves: ");
            int moves = int.Parse(Console.ReadLine());
            Figure[,] board = GenerateBoard();
            List<string> invalidMoves = new List<string>();
            VisualiseBoard(board);

            
            
            for (int i = 1; i <= moves; i++)
            {

                Console.WriteLine("Make your move: ");
                string inputLine = Console.ReadLine();
                //Simplifies the input and passes it as indexes of the board array
                int[] clientInput = FormatInputString(inputLine);
                int rank = clientInput[3] / 10;
                int file = clientInput[3] % 10;
                int startLocation = clientInput[3];
                int endLocation = clientInput[2];

                //Saves the current position of the 2 kings
                King whiteKing = (King)board[LocateThe2Kings(board).First / 10, LocateThe2Kings(board).First % 10];
                King blackKing = (King)board[LocateThe2Kings(board).Second / 10, LocateThe2Kings(board).Second % 10];
                //
                char[] letters = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };
                string temp = letters[startLocation % 10].ToString();
                string movement = temp + (board.GetLength(0) - startLocation / 10) + "-" + letters[endLocation % 10] + (board.GetLength(0) - endLocation / 10);

                
                //if the king isn't already threatened, validate the movement.
                if (!whiteKing.IsKingThreatened(board, whiteKing.Rank, whiteKing.File, true))
                {
                    if (whiteKing.IsCastlePossible(board, startLocation, endLocation))
                    {
                        board[rank, file].IsValidMove(board, startLocation, endLocation, invalidMoves);//Validates the white's move
                        board[rank, file].Move(board, startLocation, endLocation); //Makes the move
                        
                        board[rank, file].Move(board, whiteKing.CastleRookStart, whiteKing.CastleRookEnd);
                        whiteKing = (King)board[LocateThe2Kings(board).First / 10, LocateThe2Kings(board).First % 10];
                    }
                    else
                    {
                        board[rank, file].IsValidMove(board, startLocation, endLocation, invalidMoves);//Validates the white's move
                        board[rank, file].Move(board, startLocation, endLocation); //Makes the move
                        whiteKing = (King)board[LocateThe2Kings(board).First / 10, LocateThe2Kings(board).First % 10];
                    }
                    if (whiteKing.IsKingThreatened(board, whiteKing.Rank, whiteKing.File, true)) invalidMoves.Add(movement); //if the king gets threatened after the move
                }
                //if the king is threatened already
                else {
                    
                    board[rank, file].Move(board, startLocation, endLocation); //Makes the move
                    board[rank, file].IsValidMove(board, endLocation, startLocation, invalidMoves);//Validates the white's move
                    whiteKing = (King)board[LocateThe2Kings(board).First / 10, LocateThe2Kings(board).First % 10];
                    if (whiteKing.IsKingThreatened(board, whiteKing.Rank, whiteKing.File, false)) invalidMoves.Add(movement);//if the king remains threatened
                }
                board = clearEnPassantMove(board, 10); //clears the white's EnPassant possibility
                
                //Simplifies the input and passes it as indexes of the board array
                rank = clientInput[1] / 10;
                file = clientInput[1] % 10;
                startLocation = clientInput[1];
                endLocation = clientInput[0];

                temp = letters[startLocation % 10].ToString();
                movement = temp + (board.GetLength(0) - startLocation / 10) + "-" + letters[endLocation % 10] + (board.GetLength(0) - endLocation / 10);

                //if the king isn't already threatened, validate the movement.
                if (!blackKing.IsKingThreatened(board, blackKing.Rank, blackKing.File, false))
                {
                    if (blackKing.IsCastlePossible(board, startLocation, endLocation))
                    {
                        board[rank, file].IsValidMove(board, startLocation, endLocation, invalidMoves);//Validates the black's move 
                        board[rank, file].Move(board, startLocation, endLocation); //Makes the move
                        board[rank, file].Move(board, blackKing.CastleRookStart, blackKing.CastleRookEnd);
                        blackKing = (King)board[LocateThe2Kings(board).Second / 10, LocateThe2Kings(board).Second % 10];
                    }
                    else
                    {
                        board[rank, file].IsValidMove(board, startLocation, endLocation, invalidMoves);//Validates the black's move 
                        board[rank, file].Move(board, startLocation, endLocation); //Makes the move
                        blackKing = (King)board[LocateThe2Kings(board).Second / 10, LocateThe2Kings(board).Second % 10];
                    }
                    if (blackKing.IsKingThreatened(board, blackKing.Rank, blackKing.File, false)) invalidMoves.Add(movement);
                }
                //if the king is threatened already
                else
                {
                    board[rank, file].Move(board, startLocation, endLocation); //Makes the move
                    board[rank, file].IsValidMove(board, endLocation, startLocation, invalidMoves);//Validates the black's move
                    blackKing = (King)board[LocateThe2Kings(board).Second / 10, LocateThe2Kings(board).Second % 10];
                    if (blackKing.IsKingThreatened(board, blackKing.Rank, blackKing.File, false)) invalidMoves.Add(movement);//if the king remains threatened
                }
                board = clearEnPassantMove(board, 11); //clears the black's EnPassant possibility

                VisualiseBoard(board);

           }//Outputs the wrong moves to the console
            if (!invalidMoves.Any()) Console.WriteLine("All moves are valid");
            else
            {
                Console.Write("Wrong move: ");
                Console.WriteLine(String.Join("\nWrong move: ", invalidMoves));
            }
            
        }
    }
}
