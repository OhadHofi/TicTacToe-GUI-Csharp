using System;

namespace Ex05.GameInterface
{
    public partial class GameForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.player1Name = new System.Windows.Forms.Label();
            this.player1Score = new System.Windows.Forms.Label();
            this.player2Name = new System.Windows.Forms.Label();
            this.player2Score = new System.Windows.Forms.Label();
            this.ClientSize = new System.Drawing.Size(r_BoardSize * k_ButtonSize + 20, r_BoardSize * k_ButtonSize + 140);
            this.SuspendLayout();

            // 
            // Player1Score
            // 

            this.player1Score.AutoSize = true;
            this.player1Score.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.player1Score.Name = "player1Score";
            this.player1Score.AutoSize = true;
            this.player1Score.TabIndex = 1;
            this.player1Score.Text = "0";
            this.player1Score.Location = new System.Drawing.Point(Width / 2 - 25, Bottom - 60);

            // 
            // Player1Name
            // 

            this.player1Name.AutoSize = true;
            this.player1Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.player1Name.Name = "player1Name";
            this.player1Name.TabIndex = 0;
            this.player1Name.AutoSize = true;
            this.player1Name.Text = string.Format("{0}: ", r_Player1Name);
            this.player1Name.Location = new System.Drawing.Point(player1Score.Location.X - 7 * (player1Name.Text.Length), player1Score.Location.Y);

            // 
            // Player2Name
            // 

            this.player2Name.AutoSize = true;
            this.player2Name.Name = "player2Name";
            this.player2Name.AutoSize = true;
            this.player2Name.TabIndex = 3;
            this.player2Name.Text = string.Format(r_IsPlayer2AI ? "Computer: " : "{0}: ", r_Player2Name);
            this.player2Name.Location = new System.Drawing.Point(Width / 2 - 5, this.player1Score.Location.Y);

            // 
            // Player2Score
            // 

            this.player2Score.AutoSize = true;
            this.player2Score.Name = "player2Score";
            this.player1Score.AutoSize = true;
            this.player2Score.TabIndex = 2;
            this.player2Score.Text = "0";
            this.player2Score.Location = new System.Drawing.Point(this.player2Name.Location.X + 7 * (player2Name.Text.Length), this.player2Name.Location.Y);

            // 
            // GameForm
            // 

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.player2Name);
            this.Controls.Add(this.player2Score);
            this.Controls.Add(this.player1Score);
            this.Controls.Add(this.player1Name);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "GameForm";
            this.Text = "Tic-Tac-Toe 2021";
            this.Load += GameForm_Load;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Label player1Name;
        private System.Windows.Forms.Label player1Score;
        private System.Windows.Forms.Label player2Name;
        private System.Windows.Forms.Label player2Score;
    }
}