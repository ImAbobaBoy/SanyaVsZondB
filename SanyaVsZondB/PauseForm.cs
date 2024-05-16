using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SanyaVsZondB
{
    public partial class PauseForm : Form
    {
        public PauseForm()
        {
            InitializeComponent();

            var resumeButton = new Button { Text = "Возобновить", Width = 150, Height = 30 };
            var settingsButton = new Button { Text = "Настройки", Width = 150, Height = 30 };
            var exitToMainMenuButton = new Button { Text = "Выход в Главное Меню", Width = 150, Height = 30 };

            Controls.Add(resumeButton);
            Controls.Add(settingsButton);
            Controls.Add(exitToMainMenuButton);

            resumeButton.Click += (sender, args) => throw new Exception();
            settingsButton.Click += (sender, args) => throw new Exception();
            exitToMainMenuButton.Click += (sender, args) => throw new Exception();
        }
    }
}
