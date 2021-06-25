using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Ex05.GameLogic;

namespace Ex05.GameInterface
{
    public partial class GameForm : Form
    {
        private const int k_ButtonSize = 50;
        private readonly Color k_Player1Color = Color.PeachPuff;
        private readonly Color k_Player2Color = Color.LightSeaGreen;
        private readonly string r_Player1Name, r_Player2Name;
        private readonly bool r_IsPlayer2AI;
        private readonly int r_BoardSize;
        private TicTacToeButton[,] m_ButtonsBoard = null;
        private TicTacToe m_TicTacToeGame = null;

        public GameForm(int i_BoardSize, string i_Player1Name, string i_Player2Name, bool i_IsAI)
        {
            r_Player1Name = i_Player1Name;
            r_Player2Name = i_Player2Name;
            r_BoardSize = i_BoardSize;
            r_IsPlayer2AI = i_IsAI;
            InitializeComponent();
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            initGameBoard();
            m_TicTacToeGame = new TicTacToe(r_BoardSize, r_IsPlayer2AI);
            m_TicTacToeGame.InvalidPlay += TicTacToeGame_InvalidPlay;
            m_TicTacToeGame.GameOver += TicTacToeGame_GameOver;
            m_TicTacToeGame.PlayerSwitch += changeBoldText;
        }

        private void TicTacToeGame_GameOver()
        {
            string roundWinner = getRoundWinnerAndUpdateScore();
            string finalWinner = getFinalWinnerName();
            Opacity = 0.8;
            if(endOfRoundMessageBox(roundWinner))
            {
                m_TicTacToeGame.GameBoard.ClearBoard();
                clearGameBoard();
                m_TicTacToeGame.CurrentPlayer = m_TicTacToeGame.Player1;
            }
            else
            {
                MessageBox.Show(string.Format(
                               "{0}",
                               finalWinner != string.Empty ? string.Format("The winner is {0}", finalWinner) : "A draw between the players!"));
                Close();
            }

            Opacity = 1;
        }

        private string getRoundWinnerAndUpdateScore()
        {
            string winnerName = string.Empty;
            if (m_TicTacToeGame.GameState != GameLogic.TicTacToeLogic.eGameState.Draw)
            {
                winnerName = m_TicTacToeGame.CurrentPlayer == m_TicTacToeGame.Player1 ?
                             r_Player2Name : r_Player1Name;
                if(m_TicTacToeGame.CurrentPlayer == m_TicTacToeGame.Player1)
                {
                    Player2Score.Text = (int.Parse(Player2Score.Text) + 1).ToString();
                }
                else
                {
                    Player1Score.Text = (int.Parse(Player1Score.Text) + 1).ToString();
                }
            }

            return winnerName;
        }

        private string getFinalWinnerName()
        {
            string winnerName = string.Empty;
            if(m_TicTacToeGame.GameState != GameLogic.TicTacToeLogic.eGameState.Draw)
            {
                if(m_TicTacToeGame.Player1.Score > m_TicTacToeGame.Player2.Score)
                {
                    winnerName = r_Player1Name;
                }
                else
                {
                    winnerName = r_Player2Name;
                }
            }

            return winnerName;
        }

        private void TicTacToeGame_InvalidPlay()
        {
            MessageBox.Show("Invalid move!", "Error");
        }

        private void initGameBoard()
        {
            m_ButtonsBoard = new TicTacToeButton[r_BoardSize, r_BoardSize];

            for(int i = 0; i < r_BoardSize; ++i)
            {
                for (int j = 0; j < r_BoardSize; j++)
                {
                    TicTacToeButton button = new TicTacToeButton(i, j, k_ButtonSize);
                    button.BackColor = Color.LightBlue;
                    button.Text = string.Empty;
                    button.Click += new EventHandler(newBoardButton_Click);
                    Controls.Add(button);
                    m_ButtonsBoard[i, j] = button;
                }
            }
        }

        private void updateGameBoard(int i_Row, int i_Col, char i_PlayerSymbol)
        {
            TicTacToeButton buttonToUpdate = m_ButtonsBoard[i_Row, i_Col];
            buttonToUpdate.Text = i_PlayerSymbol.ToString();
            buttonToUpdate.BackColor = i_PlayerSymbol == (char)GameLogic.TicTacToe.ePlayerSign.One 
                                                     ? k_Player1Color : k_Player2Color;
        }

        private void clearGameBoard()
        {
            for (int i = 0; i < r_BoardSize; ++i)
            {
                for (int j = 0; j < r_BoardSize; j++)
                {
                    m_ButtonsBoard[i, j].BackColor = Color.LightBlue;
                    m_ButtonsBoard[i, j].Text = string.Empty;
                }
            }

            Player1Name.Font = new Font(Player1Name.Font, FontStyle.Bold);
            Player1Score.Font = new Font(Player1Score.Font, FontStyle.Bold);
            Player2Name.Font = new Font(Player2Name.Font, FontStyle.Regular);
            Player2Score.Font = new Font(Player2Score.Font, FontStyle.Regular);
        }

        private void changeBoldText()
        {
            if (Player1Name.Font.Bold)
            {
                Player2Name.Font = new Font(Player2Name.Font, FontStyle.Bold);
                Player2Score.Font = new Font(Player2Score.Font, FontStyle.Bold);
                Player1Name.Font = new Font(Player1Name.Font, FontStyle.Regular);
                Player1Score.Font = new Font(Player1Score.Font, FontStyle.Regular);
            }
            else
            {
                Player1Name.Font = new Font(Player1Name.Font, FontStyle.Bold);
                Player1Score.Font = new Font(Player1Score.Font, FontStyle.Bold);
                Player2Name.Font = new Font(Player2Name.Font, FontStyle.Regular);
                Player2Score.Font = new Font(Player2Score.Font, FontStyle.Regular);
            }
        }

        private void newBoardButton_Click(object sender, EventArgs e)
        {
            TicTacToeButton button = sender as TicTacToeButton;
            if(button != null)
            {
                if(m_TicTacToeGame.GetMoveFromPlayer(new Point(button.Row + 1, button.Col + 1)))
                {
                    updateGameBoard(button.Row, button.Col, m_TicTacToeGame.CurrentPlayer.Symbol);
                    GameLogic.TicTacToeLogic.eGameState state = m_TicTacToeGame.GameState;
                    changeBoldText();

                    if (state == GameLogic.TicTacToeLogic.eGameState.Lose || state == GameLogic.TicTacToeLogic.eGameState.Draw)
                    {
                        m_TicTacToeGame.HandleGameOver();
                    }
                    else 
                    {
                        m_TicTacToeGame.SwitchPlayer();
                        if (r_IsPlayer2AI)
                        {
                            changeBoldText();
                            makeAIMove(state);
                        }
                    }
                }
            }
        }

        private void makeAIMove(GameLogic.TicTacToeLogic.eGameState i_GameState)
        {
            GameLogic.TicTacToeLogic.GetMoveFromAI(GameLogic.TicTacToe.ePlayerSign.Two, m_TicTacToeGame.GameBoard, ref i_GameState, out Point newMove);
            updateGameBoard(newMove.X, newMove.Y, m_TicTacToeGame.CurrentPlayer.Symbol);
            if (i_GameState == GameLogic.TicTacToeLogic.eGameState.Lose || i_GameState == GameLogic.TicTacToeLogic.eGameState.Draw)
            {
                m_TicTacToeGame.GameState = i_GameState;
                m_TicTacToeGame.HandleGameOver();
                m_TicTacToeGame.SwitchPlayer();
            }

            m_TicTacToeGame.SwitchPlayer();
        }

        private bool endOfRoundMessageBox(string i_WinnerName)
        {
            string drawOrWin = i_WinnerName == string.Empty ? "A draw between the two players!" :
                                               string.Format(
                                                      "The winner is {0}!",
                                                       i_WinnerName);
            string message = string.Format("{0}{1}Would you like to play another round?", drawOrWin, Environment.NewLine);
            string title = i_WinnerName == string.Empty ? "A draw!" : "A win!";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            return result == DialogResult.Yes;
        }
    }
}
