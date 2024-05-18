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
            this.playButton.Location = new System.Drawing.Point(this.ClientSize.Width, this.ClientSize.Height / 2); 
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(300, 150); 
            this.playButton.TabIndex = 0;
            this.playButton.Text = "Play"; 
            this.playButton.UseVisualStyleBackColor = true;
            //
            // settingsButton
            //
            this.settingsButton.Location = new System.Drawing.Point(500, 500);
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(300, 150);
            this.settingsButton.TabIndex = 0;
            this.settingsButton.Text = "Settings";
            this.settingsButton.UseVisualStyleBackColor = true;
            //
            // loadGameButton
            //
            this.loadGameButton.Location = new System.Drawing.Point(900, 500);
            this.loadGameButton.Name = "loadGameButton";
            this.loadGameButton.Size = new System.Drawing.Size(300, 150);
            this.loadGameButton.TabIndex = 0;
            this.loadGameButton.Text = "Load Game";
            this.loadGameButton.UseVisualStyleBackColor = true;
            //
            // quitGameButton
            //
            this.quitGameButton.Location = new System.Drawing.Point(1300, 500);
            this.quitGameButton.Name = "quitGameButton";
            this.quitGameButton.Size = new System.Drawing.Size(300, 150);
            this.quitGameButton.TabIndex = 0;
            this.quitGameButton.Text = "Quit";
            this.quitGameButton.UseVisualStyleBackColor = true;
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