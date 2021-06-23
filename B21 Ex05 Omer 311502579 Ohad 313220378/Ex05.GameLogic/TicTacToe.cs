using System.Drawing;
using System.Threading;

namespace B21_Ex02
{
    public class TicTacToe
    {
        private Board m_Board;
        private Player m_Player1, m_Player2;
        private UI.ePlayerNumber m_CurrentPlayer;

        public enum ePlayerSign
        {
            One = 'X', Two = 'O'
        }

        public TicTacToe()
        {
            m_Board = null;
            m_Player1 = new Player(true, (char)ePlayerSign.One);
            m_Player2 = null;
            m_CurrentPlayer = UI.ePlayerNumber.One;
        }

        public void Run()
        {
            bool gameOver = false;
            int boardSize = UI.GetBoardSize();
            m_Board = new Board(boardSize);
            bool gameMode = UI.ChooseGameMode();
            m_Player2 = new Player(gameMode, (char)ePlayerSign.Two);
            GameLogic.eGameState currentState = GameLogic.eGameState.Continue;
            while (!gameOver)
            {
                //Ex02.ConsoleUtils.Screen.Clear();
                UI.ShowTurn(m_CurrentPlayer);
                m_Board.PrintBoard();

                // human players
                if (m_CurrentPlayer == UI.ePlayerNumber.One || !m_Player2.IsAI)
                {
                    getMoveFromPlayer(ref currentState, ref gameOver);
                }
                else
                {
                    // AI move
                    GameLogic.getMoveFromAI(ePlayerSign.Two, m_Board, ref currentState, ref gameOver);
                }

                if (gameOver)
                {
                    handleGameOver(ref currentState, ref gameOver);
                }

                // if we continue the game - switch between players
                if (!currentState.Equals(GameLogic.eGameState.ReTry))
                {
                    m_CurrentPlayer = m_CurrentPlayer.Equals(UI.ePlayerNumber.One) ? UI.ePlayerNumber.Two : UI.ePlayerNumber.One;
                }
            }
        }

        private void handleGameOver(ref GameLogic.eGameState io_CurrentState, ref bool io_GameOver)
        {
            if (!io_CurrentState.Equals(GameLogic.eGameState.Draw))
            {
                if (m_CurrentPlayer == UI.ePlayerNumber.One)
                {
                    m_Player2.Score++;
                }
                else
                {
                    m_Player1.Score++;
                }

                UI.RoundWon(m_CurrentPlayer == UI.ePlayerNumber.One ? UI.ePlayerNumber.Two : UI.ePlayerNumber.One);
            }
            else
            {
                UI.Draw();
            }

            UI.ShowScore(m_Player1.Score, m_Player2.Score);

            if (UI.AnotherRoundQuery())
            {
                m_Board.ClearBoard();
                m_CurrentPlayer = UI.ePlayerNumber.One;
                io_GameOver = false;
                io_CurrentState = GameLogic.eGameState.ReTry;
                return;
            }

            UI.ePlayerNumber? winner = GameLogic.GetWinner(m_Player1.Score, m_Player2.Score);
            UI.ShowWinner(winner);
            Thread.Sleep(2000);
        }

        private void getMoveFromPlayer(ref GameLogic.eGameState io_CurrentState, ref bool io_GameOver)
        {
            Point? currentPlay = null;
            bool gameContinues = UI.ChooseNextMove(ref currentPlay);

            // Q was chosen
            if (!gameContinues)
            {
                if (UI.AnotherRoundQuery())
                {
                    m_Board.ClearBoard();
                    m_CurrentPlayer = UI.ePlayerNumber.One;
                    io_CurrentState = GameLogic.eGameState.ReTry;
                }
                else
                {
                    io_GameOver = true;
                }
            }
            else
            {
                if (!GameLogic.MakeMove(
                    currentPlay,
                    m_Board,
                    m_CurrentPlayer == UI.ePlayerNumber.One ? m_Player1.Symbol : m_Player2.Symbol))
                {
                    UI.InvalidPlay();
                    Thread.Sleep(2000);
                    io_CurrentState = GameLogic.eGameState.ReTry;
                    return;
                }

                Ex02.ConsoleUtils.Screen.Clear();
                m_Board.PrintBoard();
                io_CurrentState = GameLogic.IsGameOver(
                                         m_Board,
                                         currentPlay.Value,
                                         m_CurrentPlayer == UI.ePlayerNumber.One ? m_Player1.Symbol : m_Player2.Symbol);
                io_GameOver = !io_CurrentState.Equals(GameLogic.eGameState.Continue);
            }
        }
    }
}