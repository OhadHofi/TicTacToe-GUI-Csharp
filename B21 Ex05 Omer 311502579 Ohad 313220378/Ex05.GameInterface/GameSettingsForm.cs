﻿using System;
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
    public partial class GameSettingsForm : Form
    {
        public GameSettingsForm()
        {
            InitializeComponent();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            // Not allowing empty names
            if (Player1Name == string.Empty || (!IsPlayer2AI && Player2Name == string.Empty))
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

        public bool IsPlayer2AI
        {
            get { return !checkBoxPlayer2.Checked; }
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
            if(numericRows.Value > numericRows.Maximum)
            {
                numericRows.Value = numericRows.Maximum;
            }
            else if (numericRows.Value < numericRows.Minimum)
            {
                numericRows.Value = numericRows.Minimum;
            }

            // Update rows & cols to be identical
            numericCols.Value = numericRows.Value;
        }

        private void numericCols_ValueChanged(object sender, EventArgs e)
        {
            if (numericCols.Value > numericCols.Maximum)
            {
                numericCols.Value = numericCols.Maximum;
            }
            else if (numericCols.Value < numericCols.Minimum)
            {
                numericCols.Value = numericCols.Minimum;
            }

            // Update rows & cols to be identical
            numericRows.Value = numericCols.Value;
        }
    }
}