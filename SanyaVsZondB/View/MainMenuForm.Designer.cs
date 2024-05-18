using System.Diagnostics.Tracing;
using System.Drawing;
using System.Windows.Forms;

namespace SanyaVsZondB
{
    partial class MainMenuForm
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
            this.playButton = new System.Windows.Forms.Button();
            this.settingsButton = new System.Windows.Forms.Button();
            this.loadGameButton = new System.Windows.Forms.Button();
            this.quitGameButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // playButton
            // 
            this.playButton.Location = new System.Drawing.Point(710, 190); 
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(500, 167); 
            this.playButton.TabIndex = 0;
            this.playButton.UseVisualStyleBackColor = true;
            this.playButton.Image = Image.FromFile("images\\NewGame2.png");
            this.playButton.FlatStyle = FlatStyle.Flat;
            this.playButton.BackColor = Color.Transparent;
            //
            // settingsButton
            //
            this.settingsButton.Location = new System.Drawing.Point(710, 390);
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(500, 167);
            this.settingsButton.TabIndex = 0;
            this.settingsButton.UseVisualStyleBackColor = true;
            this.settingsButton.Image = Image.FromFile("images\\Options.png");
            this.settingsButton.FlatStyle = FlatStyle.Flat;
            this.settingsButton.BackColor = Color.Transparent;
            //
            // loadGameButton
            //
            this.loadGameButton.Location = new System.Drawing.Point(710, 590);
            this.loadGameButton.Name = "loadGameButton";
            this.loadGameButton.Size = new System.Drawing.Size(500, 167);
            this.loadGameButton.TabIndex = 0;
            this.loadGameButton.UseVisualStyleBackColor = true;
            this.loadGameButton.Image = Image.FromFile("images\\Load.png");
            this.loadGameButton.FlatStyle = FlatStyle.Flat;
            this.loadGameButton.BackColor = Color.Transparent;
            //
            // quitGameButton
            //
            this.quitGameButton.Location = new System.Drawing.Point(710, 790);
            this.quitGameButton.Name = "quitGameButton";
            this.quitGameButton.Size = new System.Drawing.Size(500, 167);
            this.quitGameButton.TabIndex = 0;
            this.quitGameButton.UseVisualStyleBackColor = true;
            this.quitGameButton.Image = Image.FromFile("images\\Exit.png");
            this.quitGameButton.FlatStyle = FlatStyle.Flat;
            this.quitGameButton.BackColor = Color.Transparent;
            // 
            // MainMenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Name = "MainMenuForm";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.MainMenuForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

    }
}