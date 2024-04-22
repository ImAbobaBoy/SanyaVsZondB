namespace SanyaVsZondB.View
{
    partial class FirstToSecond
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
            this.SuspendLayout();
            // 
            // buttonPlay
            // 
            this.buttonMakeChanges1.Location = new System.Drawing.Point(this.ClientSize.Width, this.ClientSize.Height / 2);
            this.buttonMakeChanges1.Name = "buttonPlay";
            this.buttonMakeChanges1.Size = new System.Drawing.Size(300, 150);
            this.buttonMakeChanges1.TabIndex = 0;
            this.buttonMakeChanges1.Text = Game.Improvements[_randomImprovementIndex].name;
            this.buttonMakeChanges1.UseVisualStyleBackColor = true;

            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "FirstToSecond";
            this.Load += new System.EventHandler(this.FirstToSecond_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}