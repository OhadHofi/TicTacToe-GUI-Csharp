using Ex05.GameInterface;

namespace Ex05.GameLogic
{
    public class Program
    {
        public static void Main()
        {
            GameSettings gameSettings = new GameSettings();

            int size = gameSettings.BoardSize;
            bool isAI = gameSettings.IsAIPlayer;
            string player1 = gameSettings.Player1Name;
            string player2 = gameSettings.Player2Name;

            TicTacToe reversedTicTacToeGame = new TicTacToe();
            reversedTicTacToeGame.Run();
        }
    }
}
