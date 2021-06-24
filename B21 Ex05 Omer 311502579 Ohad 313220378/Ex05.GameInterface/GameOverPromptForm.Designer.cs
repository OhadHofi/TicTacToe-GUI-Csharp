using System;

namespace Ex05.GameInterface
{
    partial class GameOverPromptForm
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
            this.OutputMessage = new System.Windows.Forms.Label();
            this.OkButton = new System.Windows.Forms.Button();
            this.NoButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // OutputMessage
            // 
            this.OutputMessage.AutoSize = true;
            this.OutputMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.OutputMessage.Location = new System.Drawing.Point(60, 33);
            this.OutputMessage.Name = "OutputMessage";
            this.OutputMessage.Size = new System.Drawing.Size(82, 17);
            this.OutputMessage.TabIndex = 0;
            this.OutputMessage.Text = string.Format(
                                    "{0}{1}Would you like to play another round?",
                                    r_IsDraw ? "A draw between the two players!" : string.Format("The winner is {0}!", r_Winner),
                                    Environment.NewLine);
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(58, 123);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 1;
            this.OkButton.Text = "Yes";
            this.OkButton.Click += OkButton_Click;
            this.OkButton.UseVisualStyleBackColor = true;
            // 
            // CancelButton
            // 
            this.NoButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.NoButton.Location = new System.Drawing.Point(180, 123);
            this.NoButton.Name = "NoButton";
            this.NoButton.Size = new System.Drawing.Size(75, 23);
            this.NoButton.TabIndex = 2;
            this.NoButton.Text = "No";
            this.NoButton.UseVisualStyleBackColor = true;
            // 
            // GameOverPromptForm
            // 
            this.AcceptButton = this.OkButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.NoButton;
            this.ClientSize = new System.Drawing.Size(315, 158);
            this.Controls.Add(this.NoButton);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.OutputMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameOverPromptForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = r_IsDraw ? "A draw!" : "A win!";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Label OutputMessage;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Button NoButton;
    }
}