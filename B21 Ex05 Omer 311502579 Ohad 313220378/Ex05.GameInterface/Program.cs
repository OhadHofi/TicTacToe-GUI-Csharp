using Ex05.GameLogic;

namespace Ex05.GameInterface
{
    public class Program
    {
        public static void Main()
        {
            GameSettingsForm gameSettings = new GameSettingsForm();
            gameSettings.ShowDialog();
            int size = gameSettings.BoardSize;
            bool isAI = gameSettings.IsPlayer2AI;
            string player1 = gameSettings.Player1Name;
            string player2 = gameSettings.Player2Name;

            TicTacToe reversedTicTacToeGame = new TicTacToe();
            reversedTicTacToeGame.Run();
        }
    }
}
