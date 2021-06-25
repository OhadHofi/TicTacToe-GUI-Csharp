using System;
using System.Drawing;
using System.Threading;

namespace Ex05.GameLogic
{
    public class TicTacToe
    {
        public event Action InvalidPlay;

        public event Action GameOver;

        public event Action PlayerSwitch;

        private Board m_Board;
        private Player m_Player1, m_Player2, m_CurrentPlayer;
        private TicTacToeLogic.eGameState m_GameState;

        public TicTacToe(int i_BoardSize, bool i_IsPlayer2Human)
        {
            m_Board = new Board(i_BoardSize);
            m_Player1 = new Player(true, (char)ePlayerSign.One);
            m_Player2 = new Player(i_IsPlayer2Human, (char)ePlayerSign.Two);
            m_CurrentPlayer = m_Player1;
            m_GameState = TicTacToeLogic.eGameState.Continue;
        }

        public enum ePlayerSign
        {
            One = 'X', Two = 'O'
        }

        public Player Player1
        {
            get { return m_Player1; }
        }

        public Player Player2
        {
            get { return m_Player2; }
        }

        public Player CurrentPlayer
        {
            get { return m_CurrentPlayer; }
            set { m_CurrentPlayer = value; }
        }

        public Board GameBoard
        {
            get { return m_Board; }
        }

        public TicTacToeLogic.eGameState GameState
        {
            get { return m_GameState; }
            set { m_GameState = value; }
        }

        public void HandleGameOver()
        {
            if (!GameState.Equals(TicTacToeLogic.eGameState.Draw))
            {
                if (m_CurrentPlayer == m_Player1)
                {
                    m_Player2.Score++;
                }
                else
                {
                    m_Player1.Score++;
                }
            }

            GameOver.Invoke();
        }

        public bool GetMoveFromPlayer(Point i_MoveToMake)
        {
            bool madeMove = false;
            if (!TicTacToeLogic.MakeMove(
                    i_MoveToMake,
                    m_Board,
                    CurrentPlayer == Player1 ? Player1.Symbol : Player2.Symbol))
            {
                InvalidPlay.Invoke();
                m_GameState = TicTacToeLogic.eGameState.ReTry;
            }
            else
            {
                m_GameState = TicTacToeLogic.IsGameOver(
                                         m_Board,
                                         i_MoveToMake,
                                         CurrentPlayer == Player1 ? Player1.Symbol : Player2.Symbol);
                madeMove = true;
            }

            return madeMove;
        }

        public void SwitchPlayer()
        {
            if(!m_Player2.IsAI)
            {
                PlayerSwitch.Invoke();
            }

            m_CurrentPlayer = CurrentPlayer == Player1 ? Player2 : Player1;
        }
    }
}