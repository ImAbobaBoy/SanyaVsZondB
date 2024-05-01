namespace SanyaVsZondB.View
{
    partial class MovingToAnotherLevel
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
            this.buttonMakeChanges1 = new System.Windows.Forms.Button();
            this.buttonMakeChanges2 = new System.Windows.Forms.Button();
            this.buttonMakeChanges3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonMakeChanges1
            // 
            this.buttonMakeChanges1.Location = new System.Drawing.Point(this.ClientSize.Width - 300, this.ClientSize.Height / 2);
            this.buttonMakeChanges1.Name = "buttonMakeChanges1";
            this.buttonMakeChanges1.Size = new System.Drawing.Size(300, 150);
            this.buttonMakeChanges1.TabIndex = 0;
            this.buttonMakeChanges1.Text = $"{Game.PlayerImprovements[_randomPlayerImprovementIndexFirst].name} " +
                $"\n {Game.ZondBImprovements[_randomZondBImprovementIndexFirst].name}";
            this.buttonMakeChanges1.UseVisualStyleBackColor = true;
            //
            // buttonMakeChanges2
            //
            this.buttonMakeChanges2.Location = new System.Drawing.Point(this.ClientSize.Width + 100, this.ClientSize.Height / 2);
            this.buttonMakeChanges2.Name = "buttonMakeChanges2";
            this.buttonMakeChanges2.Size = new System.Drawing.Size(300, 150);
            this.buttonMakeChanges2.TabIndex = 0;
            this.buttonMakeChanges2.Text = $"{Game.PlayerImprovements[_randomPlayerImprovementIndexSecond].name} " +
                $"\n {Game.ZondBImprovements[_randomZondBImprovementIndexSecond].name}";
            this.buttonMakeChanges2.UseVisualStyleBackColor = true;
            //
            // buttonMakeChanges3
            //
            this.buttonMakeChanges3.Location = new System.Drawing.Point(this.ClientSize.Width + 500, this.ClientSize.Height / 2);
            this.buttonMakeChanges3.Name = "buttonMakeChanges3";
            this.buttonMakeChanges3.Size = new System.Drawing.Size(300, 150);
            this.buttonMakeChanges3.TabIndex = 0;
            this.buttonMakeChanges3.Text = $"{Game.PlayerImprovements[_randomPlayerImprovementIndexThird].name} " +
                $"\n {Game.ZondBImprovements[_randomZondBImprovementIndexThird].name}";
            this.buttonMakeChanges3.UseVisualStyleBackColor = true;
            //
            // 
            //
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Text = "FirstToSecond";
            this.Load += new System.EventHandler(this.FirstToSecond_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}