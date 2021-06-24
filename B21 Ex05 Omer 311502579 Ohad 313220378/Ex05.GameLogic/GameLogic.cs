using System;
using System.Drawing;
using System.Runtime;
using System.Threading;

namespace Ex05.GameLogic
{
    public static class GameLogic
    {
        public const int k_NumCellsForFastCalculation = 9;
        public static Random s_Randomizer = new Random();

        // enum for holding the possible states of the game
        public enum eGameState
        {
            Draw, Lose, Continue, ReTry
        }

        // Returns the state of the game according to the current move that was played
        public static eGameState IsGameOver(Board i_GameBoard, Point i_CurrentPlay, char i_Symbol)
        {
            eGameState currentState = eGameState.Continue;
            if (CheckRowsState(i_GameBoard, i_CurrentPlay, i_Symbol) == eGameState.Lose ||
                CheckColumnsState(i_GameBoard, i_CurrentPlay, i_Symbol) == eGameState.Lose ||
                CheckLeftRightDiagonalState(i_GameBoard, i_CurrentPlay, i_Symbol) == eGameState.Lose ||
                CheckRightLeftDiagonalState(i_GameBoard, i_CurrentPlay, i_Symbol) == eGameState.Lose)
            {
                currentState = eGameState.Lose;
            }
            else if (i_GameBoard.IsFull)
            {
                currentState = eGameState.Draw;
            }

            return currentState;
        }

        // Checks for a sequence of matching chars in the any of the rows
        public static eGameState CheckRowsState(Board i_Board, Point i_CurrentPlay, char i_Symbol)
        {
            eGameState gameState = eGameState.Continue;
            int row = i_CurrentPlay.X - 1, symbolCount = 0;
            for (int i = 0; i < i_Board.Size; i++)
            {
                if (i_Board.GetValue(row, i) == i_Symbol)
                {
                    symbolCount++;
                }
            }

            if (symbolCount == i_Board.Size)
            {
                gameState = eGameState.Lose;
            }

            return gameState;
        }

        // Checks for a sequence of matching chars in the any of the columns
        public static eGameState CheckColumnsState(Board i_Board, Point i_CurrentPlay, char i_Symbol)
        {
            eGameState gameState = eGameState.Continue;
            int col = i_CurrentPlay.Y - 1, symbolCount = 0;

            for (int i = 0; i < i_Board.Size; i++)
            {
                if (i_Board.GetValue(i, col) == i_Symbol)
                {
                    symbolCount++;
                }
            }

            if (symbolCount == i_Board.Size)
            {
                gameState = eGameState.Lose;
            }

            return gameState;
        }

        // Checks for a sequence of matching chars in the left to right diagonal row
        public static eGameState CheckLeftRightDiagonalState(Board i_Board, Point i_CurrentPlay, char i_Symbol)
        {
            eGameState gameState = eGameState.Continue;
            int col = i_CurrentPlay.Y, row = i_CurrentPlay.X, symbolCount = 0;
            if (col == row)
            {
                for (int i = 0; i < i_Board.Size; i++)
                {
                    if (i_Board.GetValue(i, i) == i_Symbol)
                    {
                        symbolCount++;
                    }
                }
            }

            if (symbolCount == i_Board.Size)
            {
                gameState = eGameState.Lose;
            }

            return gameState;
        }

        // Checks for a sequence of matching chars in the right to left diagonal row
        public static eGameState CheckRightLeftDiagonalState(Board i_Board, Point i_CurrentPlay, char i_Symbol)
        {
            eGameState gameState = eGameState.Continue;
            int col = i_CurrentPlay.Y, row = i_CurrentPlay.X, symbolCount = 0;

            // if true- the point is on diagonal right left
            if (row + col == i_Board.Size + 1)
            {
                for (int i = 0; i < i_Board.Size; i++)
                {
                    // (i_Board.Size - 1 - i) is the column of row i in the diagonal
                    if (i_Board.GetValue(i, i_Board.Size - 1 - i) == i_Symbol)
                    {
                        symbolCount++;
                    }
                }
            }

            if (symbolCount == i_Board.Size)
            {
                gameState = eGameState.Lose;
            }

            return gameState;
        }

        public static bool MakeMove(Point? i_CurrentMove, Board i_GameBoard, char i_PlayerSign)
        {
            return i_CurrentMove != null &&
                            i_GameBoard.AddMove(
                                        i_CurrentMove.Value.X - 1,
                                        i_CurrentMove.Value.Y - 1,
                                        i_PlayerSign);
        }

        // Returns the winner of the game according to score
        //public static Player GetWinner(int i_Player1Score, int i_Player2Score)
        //{
        //   Player winner;
        //    if (i_Player1Score == i_Player2Score)
        //    {
        //        winner = null;
        //    }
        //    else if (i_Player1Score > i_Player2Score)
        //    {
        //        winner = UI.ePlayerNumber.One;
        //    }
        //    else
        //    {
        //        winner = UI.ePlayerNumber.Two;
        //    }

        //    return winner;
        //}

        public static void getMoveFromAI(TicTacToe.ePlayerSign i_Symbol, Board i_Board, ref eGameState io_CurrentState, ref bool io_GameOver)
        {
            int xCoordSave = 0;
            int yCoordSave = 0;
            eGameState currentState = eGameState.Continue;
            //UI.ShowAIMsg();

            /*  According to our checks as long as there are more than 9 empty cells, we can't properly calculate a good AI move in a decent time.
                So, if there are more than 9 empty cells we go with a semi-random play - get random board indices but those that won't lead
                to a loss. If checked in debugging mode, may take longer*/
            if (i_Board.NumberOfEmptyCells > k_NumCellsForFastCalculation)
            {
                int randomRow = s_Randomizer.Next(1, i_Board.Size + 1) - 1;
                int randomCol = s_Randomizer.Next(1, i_Board.Size + 1) - 1;
                bool gameOver = IsGameOver(i_Board, new Point(randomRow + 1, randomCol + 1), (char)i_Symbol).Equals(eGameState.Lose);

                // The AI chooses a move that is legal and doesn't lead to a loss
                while (!(i_Board.AddMove(randomRow, randomCol, (char)i_Symbol) && !gameOver))
                {
                    gameOver = IsGameOver(i_Board, new Point(randomRow + 1, randomCol + 1), (char)i_Symbol).Equals(eGameState.Lose);
                    if (gameOver)
                    {
                        i_Board.ClearSquare(randomRow, randomCol);
                    }

                    randomRow = s_Randomizer.Next(1, i_Board.Size + 1) - 1;
                    randomCol = s_Randomizer.Next(1, i_Board.Size + 1) - 1;
                }

                xCoordSave = randomRow + 1;
                yCoordSave = randomCol + 1;
            }
            else
            {
                /*  This method tries to find the best move to play according to the scenario in the board.
                    The recursion calculates the no. of possible wins from its current state and returns by reference 
                    the play which leads to the biggest no. of wins.
                    Assuming that the greater the number of my possible victories, the greater the chance of winning */
                getMoveFromAIRecursion(ref currentState, i_Symbol, i_Board, ref xCoordSave, ref yCoordSave);
                i_Board.AddMove(xCoordSave, yCoordSave, 'O');
                xCoordSave++;
                yCoordSave++;
            }

            // Reflect current game state change
            io_CurrentState = IsGameOver(
                                        i_Board,
                                        new Point(xCoordSave, yCoordSave),
                                        'O');
            io_GameOver = !io_CurrentState.Equals(eGameState.Continue);

            //Ex02.ConsoleUtils.Screen.Clear(); ---> clear forms screen
            i_Board.PrintBoard();
        }

        private static int getMoveFromAIRecursion(ref eGameState io_CurrentState, TicTacToe.ePlayerSign i_Symbol, Board i_Board, ref int io_ISave, ref int io_JSave)
        {
            int recursionResult = 0;

            // Base case - game state is a draw / loss
            if (io_CurrentState.Equals(eGameState.Draw) || io_CurrentState.Equals(eGameState.Lose))
            {
                if (io_CurrentState.Equals(eGameState.Draw))
                {
                    recursionResult = 0;
                }
                else
                {
                    // it means that player 2 will lose, so we call to recursion and change i_Symbol
                    if (i_Symbol.Equals(TicTacToe.ePlayerSign.One))
                    {
                        recursionResult = -1;
                    }
                    else
                    {
                        recursionResult = 1;
                    }
                }
            }
            else
            {
                //int maxWins = -2;
                //for (int i = 0; i < i_Board.Size; i++)
                //{
                //    for (int j = 0; j < i_Board.Size; j++)
                //    {
                //        if (!i_Board.AddMove(i, j, (char)i_Symbol))
                //        {
                //            continue;
                //        }

                //        io_CurrentState = IsGameOver(i_Board, new Point(i + 1, j + 1), (char)i_Symbol);
                //        TicTacToe.ePlayerSign oppositeSymbol = i_Symbol == TicTacToe.ePlayerSign.One ?
                //                                                TicTacToe.ePlayerSign.Two : TicTacToe.ePlayerSign.One;
                //        int wins = getMoveFromAIRecursion(ref io_CurrentState, oppositeSymbol, i_Board, ref io_ISave, ref io_JSave);
                //        i_Board.ClearSquare(i, j);
                //        recursionResult += wins;
                //        if (maxWins < wins)
                //        {
                //            io_ISave = i; // row
                //            io_JSave = j; // column
                //            maxWins = wins;
                //        }
                //    }
                getMoveFromAIRecursionHelper(ref io_CurrentState, i_Symbol, i_Board, ref io_ISave, ref io_JSave, ref recursionResult);
            }
            

            return recursionResult;
        }

        private static void getMoveFromAIRecursionHelper(ref eGameState io_CurrentState, TicTacToe.ePlayerSign i_Symbol, Board i_Board, ref int io_ISave, ref int io_JSave, ref int io_RecursionResult)
        {
            int maxWins = -2;
            for (int i = 0; i < i_Board.Size; i++)
            {
                for (int j = 0; j < i_Board.Size; j++)
                {
                    if (!i_Board.AddMove(i, j, (char)i_Symbol))
                    {
                        continue;
                    }

                    io_CurrentState = IsGameOver(i_Board, new Point(i + 1, j + 1), (char)i_Symbol);
                    TicTacToe.ePlayerSign oppositeSymbol = i_Symbol == TicTacToe.ePlayerSign.One ?
                                                            TicTacToe.ePlayerSign.Two : TicTacToe.ePlayerSign.One;
                    int wins = getMoveFromAIRecursion(ref io_CurrentState, oppositeSymbol, i_Board, ref io_ISave, ref io_JSave);
                    i_Board.ClearSquare(i, j);
                    io_RecursionResult += wins;
                    if (maxWins < wins)
                    {
                        io_ISave = i; // row
                        io_JSave = j; // column
                        maxWins = wins;
                    }
                }
            }
        }
    }
}
