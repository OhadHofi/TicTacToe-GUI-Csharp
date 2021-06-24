using System;
using System.Drawing;
using System.Threading;

namespace Ex05.GameLogic
{
    public delegate Point PlayerMoveEventHandler(object sender, MoveEventArgs e);

    public class TicTacToe
    {
        public event PlayerMoveEventHandler PlayerMove;
        public event Action InvalidPlay;
        public event Action GameOver;

        private Board m_Board;
        private Player m_Player1, m_Player2, m_CurrentPlayer;
        private GameLogic.eGameState m_GameState;
        public TicTacToe(int i_BoardSize, bool i_IsPlayer2Human)
        {
            m_Board = new Board(i_BoardSize);
            m_Player1 = new Player(true, (char)ePlayerSign.One);
            m_Player2 = new Player(i_IsPlayer2Human, (char)ePlayerSign.Two);
            m_CurrentPlayer = m_Player1;
            m_GameState = GameLogic.eGameState.Continue;
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

        public GameLogic.eGameState GameState
        {
            get { return m_GameState; }
        }

        //public void Run()
        //{
        //    bool gameOver = false;
        //    GameLogic.eGameState currentState = GameLogic.eGameState.Continue;
        //    while (!gameOver)
        //    {
        //        // UI.ShowTurn(m_CurrentPlayer);
        //        // m_Board.PrintBoard();

        //        // human players
        //        if (m_CurrentPlayer == m_Player1 || !m_Player2.IsAI)
        //        {
        //            getMoveFromPlayer(ref currentState, ref gameOver);
        //            //PlayerMove.Invoke(ref currentState, ref gameOver, new MoveEventArgs()
        //        }
        //        else
        //        {
        //            // AI move
        //            GameLogic.getMoveFromAI(ePlayerSign.Two, m_Board, ref currentState, ref gameOver);
        //        }

        //        if (gameOver)
        //        {
        //            handleGameOver(ref currentState, ref gameOver);
        //        }

        //        // if we continue the game - switch between players
        //        if (!currentState.Equals(GameLogic.eGameState.ReTry))
        //        {
        //            m_CurrentPlayer = m_CurrentPlayer == m_Player1 ? m_Player2 : m_Player1;
        //        }
        //    }
        //}

        public void HandleGameOver()
        {
            if (!GameState.Equals(GameLogic.eGameState.Draw))
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
            if (!GameLogic.MakeMove(
                    i_MoveToMake,
                    m_Board,
                    CurrentPlayer == Player1 ? Player1.Symbol : Player2.Symbol))
            {
                InvalidPlay.Invoke();
                m_GameState = GameLogic.eGameState.ReTry;
            }
            else
            {
                m_GameState = GameLogic.IsGameOver(
                                         m_Board,
                                         i_MoveToMake,
                                         CurrentPlayer == Player1 ? Player1.Symbol : Player2.Symbol);
                madeMove = true;
            }

            return madeMove;
        }

        public void SwitchPlayer()
        {
            m_CurrentPlayer = CurrentPlayer == Player1 ? Player2 : Player1;
        }
    }
}