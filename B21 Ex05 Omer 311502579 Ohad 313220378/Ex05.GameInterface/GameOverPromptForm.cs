using System;
using System.Windows.Forms;

namespace Ex05.GameInterface
{
    public partial class GameOverPromptForm : Form
    {
        private readonly string r_Winner;
        private readonly bool r_IsDraw;
        public GameOverPromptForm(string i_WinnerName, bool i_Draw)
        {
            r_Winner = i_WinnerName;
            r_IsDraw = i_Draw;
            InitializeComponent();
        }
        private void OkButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
