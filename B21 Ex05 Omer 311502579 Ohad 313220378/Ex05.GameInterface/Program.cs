using Ex05.GameLogic;

namespace Ex05.GameInterface
{
    public class Program
    {
        public static void Main()
        {
            GameSettingsForm gameSettings = new GameSettingsForm();
            gameSettings.ShowDialog();
        }
    }
}
