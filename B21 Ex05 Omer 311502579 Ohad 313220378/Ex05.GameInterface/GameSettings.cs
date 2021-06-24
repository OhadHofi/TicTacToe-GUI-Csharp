using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ex05.GameInterface
{
    public partial class GameSettings : Form
    {
        public GameSettings()
        {
            InitializeComponent();
            ShowDialog();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (Player1Name == string.Empty || (!AIPlayer2 && Player2Name == string.Empty))
            {
                MessageBox.Show("Please enter proper names (no blanks)");
            }
            else
            {
                Close();
            }
        }

        private void checkBoxPlayer2_CheckedChanged(object sender, EventArgs e)
        {
            textBoxPlayer2.Enabled = !textBoxPlayer2.Enabled;
            textBoxPlayer2.Text = textBoxPlayer2.Enabled ? string.Empty : "[Computer]";
        }

        public int BoardSize
        {
            get { return (int)numericCols.Value; }
        }

        public bool AIPlayer2
        {
            get { return checkBoxPlayer2.Checked == false; }
        }

        public string Player1Name
        {
            get { return textBoxPlayer1.Text; }
        }

        public string Player2Name
        {
            get { return textBoxPlayer2.Text; }
        }

        private void numericRows_ValueChanged(object sender, EventArgs e)
        {
            if(numericRows.Value > 10)
            {
                numericRows.Value = 10;
            }
            else if (numericRows.Value < 4)
            {
                numericRows.Value = 4;
            }

            numericCols.Value = numericRows.Value;
        }

        private void numericCols_ValueChanged(object sender, EventArgs e)
        {
            if (numericCols.Value > 10)
            {
                numericCols.Value = 10;
            }
            else if (numericCols.Value < 4)
            {
                numericCols.Value = 4;
            }

            numericRows.Value = numericCols.Value;
        }
    }
}
