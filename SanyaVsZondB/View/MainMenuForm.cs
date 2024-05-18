using SanyaVsZondB.Model;
using SanyaVsZondB.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace SanyaVsZondB
{
    public partial class MainMenuForm : Form
    {
        private Button playButton;
        private Button settingsButton;
        private Button loadGameButton;
        private Button quitGameButton;
        private int _currentLevel = 1;
        public Game Game { get; private set; }

        public MainMenuForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;


            Game = new Game();

            // Добавление кнопки на форму
            this.Controls.Add(playButton);
            this.Controls.Add(settingsButton);
            this.Controls.Add(loadGameButton);
            this.Controls.Add(quitGameButton);

            this.BackgroundImage = Image.FromFile("images\\MainMenuBackground.jpg");
            this.BackgroundImageLayout = ImageLayout.Stretch; // или другой режим растяжения

            Game.Data.LoadLastTwoPropertiesFromFile();

            playButton.Click += new EventHandler(this.ButtonPlay_Click);
            settingsButton.Click += new EventHandler(this.SettingsButton_Click);
            loadGameButton.Click += new EventHandler(this.ButtonLoadGame_Click);
            quitGameButton.Click += new EventHandler(this.ButtonQuitGame_Click);
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
        }

        private void MainMenuForm_Load(object sender, EventArgs e)
        {

        }

        private void ButtonPlay_Click(object sender, EventArgs e)
        {
            var level = new LevelForm(Game, _currentLevel);
            SwitchForm(level);
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {

        }
        private void ButtonLoadGame_Click(object sender, EventArgs e)
        {
            _currentLevel = Game.Data.LoadPropertiesFromFile();
            var level = new LevelForm(Game, _currentLevel);
            SwitchForm(level);
        }

        private void ButtonQuitGame_Click(object sender, EventArgs e)
        {
            Game.Data.SaveLastTwoPropertiesToFile();
            Application.Exit();
        }

        private void SwitchForm(Form newForm)
        {
            this.Hide();
            newForm.Show();
        }

        protected override void WndProc(ref Message m)
        {
            // Перехват сообщения TCM_ADJUSTRECT для скрытия вкладок
            if (m.Msg == 0x1328)
            {
                m.Result = (IntPtr)1;
            }
            else
            {
                base.WndProc(ref m);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Game.Data.SaveLastTwoPropertiesToFile();
            Application.Exit();
        }
    }
}
