using System.Drawing;
using System.Windows.Forms;

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
            this.buttonMakeChanges1.Location = new System.Drawing.Point(460, 390);
            this.buttonMakeChanges1.Name = "buttonMakeChanges1";
            this.buttonMakeChanges1.Size = new System.Drawing.Size(300, 300);
            this.buttonMakeChanges1.TabIndex = 0;
            this.buttonMakeChanges1.Text = $"{Game.PlayerImprovements[_randomPlayerImprovementIndexFirst].name} " + "\n"
                + $"\n {Game.ZondBImprovements[_randomZondBImprovementIndexFirst].name}";
            this.buttonMakeChanges1.UseVisualStyleBackColor = true;
            this.buttonMakeChanges1.Font = new System.Drawing.Font("Segoe UI", 16);
            this.buttonMakeChanges1.ForeColor = Color.Red;
            this.buttonMakeChanges1.BackColor = ColorTranslator.FromHtml("#140e10");
            this.buttonMakeChanges1.FlatStyle = FlatStyle.Flat;
            this.buttonMakeChanges1.FlatAppearance.BorderColor = Color.Red;
            this.buttonMakeChanges1.FlatAppearance.BorderSize = 4;
            //
            // buttonMakeChanges2
            //
            this.buttonMakeChanges2.Location = new System.Drawing.Point(810, 390);
            this.buttonMakeChanges2.Name = "buttonMakeChanges2";
            this.buttonMakeChanges2.Size = new System.Drawing.Size(300, 300);
            this.buttonMakeChanges2.TabIndex = 0;
            this.buttonMakeChanges2.Text = $"{Game.PlayerImprovements[_randomPlayerImprovementIndexSecond].name} " + "\n"
                + $"\n {Game.ZondBImprovements[_randomZondBImprovementIndexSecond].name}";
            this.buttonMakeChanges2.UseVisualStyleBackColor = true;
            this.buttonMakeChanges2.Font = new System.Drawing.Font("Segoe UI", 16);
            this.buttonMakeChanges2.ForeColor = Color.Red;
            this.buttonMakeChanges2.BackColor = ColorTranslator.FromHtml("#140e10");
            this.buttonMakeChanges2.FlatStyle = FlatStyle.Flat;
            this.buttonMakeChanges2.FlatAppearance.BorderColor = Color.Red;
            this.buttonMakeChanges2.FlatAppearance.BorderSize = 4;
            //
            // buttonMakeChanges3в
            //
            this.buttonMakeChanges3.Location = new System.Drawing.Point(1160, 390);
            this.buttonMakeChanges3.Name = "buttonMakeChanges3";
            this.buttonMakeChanges3.Size = new System.Drawing.Size(300, 300);
            this.buttonMakeChanges3.TabIndex = 0;
            this.buttonMakeChanges3.Text = $"{Game.PlayerImprovements[_randomPlayerImprovementIndexThird].name} " + "\n"
                + $"\n {Game.ZondBImprovements[_randomZondBImprovementIndexThird].name}";
            this.buttonMakeChanges3.UseVisualStyleBackColor = true;
            this.buttonMakeChanges3.Font = new System.Drawing.Font("Segoe UI", 16);
            this.buttonMakeChanges3.ForeColor = Color.Red;
            this.buttonMakeChanges3.BackColor = ColorTranslator.FromHtml("#140e10");
            this.buttonMakeChanges3.FlatStyle = FlatStyle.Flat;
            this.buttonMakeChanges3.FlatAppearance.BorderColor = Color.Red;
            this.buttonMakeChanges3.FlatAppearance.BorderSize = 4;
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