using NAudio.Wave;
using SanyaVsZondB.Model;
using SanyaVsZondB.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace SanyaVsZondB
{
    public partial class MainMenuForm : Form
    {
        public Game Game { get; private set; }
        private Button playButton;
        private Button settingsButton;
        private Button loadGameButton;
        private Button quitGameButton;
        private int _currentLevel = 1;
        private WaveOut _waveOut;
        private WaveOut _waveOutLevel;
        private WaveChannel32 _mainMenuMusicChannel;
        private WaveChannel32 _levelMusicChannel;
        private Panel _settingsPanel;
        private TrackBar _musicTrackBar;
        private TrackBar _soundTrackBar;

        public MainMenuForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            Game = new Game();

            this.Controls.Add(playButton);
            this.Controls.Add(settingsButton);
            this.Controls.Add(loadGameButton);
            this.Controls.Add(quitGameButton);

            this.BackgroundImage = Image.FromFile("images\\MainMenuBackground.jpg");
            this.BackgroundImageLayout = ImageLayout.Stretch; 

            Game.Data.LoadLastTwoPropertiesFromFile();

            _settingsPanel = new Panel
            {
                Size = new Size(250, 167), 
                Location = new System.Drawing.Point(((this.Width - 600) / 2) - 270, 390), 
                ForeColor = ColorTranslator.FromHtml("#140e10"),
                BackColor = ColorTranslator.FromHtml("#140e10"),
                Visible = false 
            };
            Controls.Add(_settingsPanel);
            AddControlsToSettingsPanel();

            _mainMenuMusicChannel = new WaveChannel32(new AudioFileReader("music\\MainMenuMusic.mp3"));
            _mainMenuMusicChannel.Volume = Game.Data.MusicVolume / 100;
            _waveOut = new WaveOut();
            _waveOut.Init(_mainMenuMusicChannel);
            _waveOut.Play();

            _levelMusicChannel = new WaveChannel32(new AudioFileReader("music\\EpicBattleMusic.mp3"));
            _levelMusicChannel.Volume = Game.Data.MusicVolume / 100;
            _waveOutLevel = new WaveOut();
            _waveOutLevel.Init(_levelMusicChannel);

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
            var level = new LevelForm(Game, _currentLevel, _waveOutLevel, _levelMusicChannel);
            SwitchForm(level);
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            _settingsPanel.Visible = !_settingsPanel.Visible;
        }

        private void ButtonLoadGame_Click(object sender, EventArgs e)
        {
            _currentLevel = Game.Data.LoadPropertiesFromFile();
            var level = new LevelForm(Game, _currentLevel, _waveOutLevel, _levelMusicChannel);
            SwitchForm(level);
        }

        private void ButtonQuitGame_Click(object sender, EventArgs e)
        {
            Game.Data.SaveLastTwoPropertiesToFile();
            Application.Exit();
        }

        private void AddControlsToSettingsPanel()
        {
            var musicLabel = new Label
            {
                Text = "Музыка",
                Location = new System.Drawing.Point(10, 20),
                AutoSize = true,
                ForeColor = Color.Red,
                Font = new System.Drawing.Font("Segoe UI", 10)
            };
            _settingsPanel.Controls.Add(musicLabel);

            _musicTrackBar = new TrackBar
            {
                Location = new System.Drawing.Point(10, 40),
                Size = new Size(200, 45),
                Minimum = 0,
                Maximum = 100,
                Value = (int)(Game.Data.MusicVolume)
            };
            _musicTrackBar.Scroll += MusicTrackBar_Scroll;
            _settingsPanel.Controls.Add(_musicTrackBar);

            var soundLabel = new Label
            {
                Text = "Звук",
                Location = new System.Drawing.Point(10, 90),
                AutoSize = true,
                ForeColor = Color.Red,
                Font = new System.Drawing.Font("Segoe UI", 10)
            };
            _settingsPanel.Controls.Add(soundLabel);

            _soundTrackBar = new TrackBar
            {
                Location = new System.Drawing.Point(10, 110),
                Size = new Size(200, 45),
                Minimum = 0,
                Maximum = 100,
                Value = (int)(Game.Data.SoundVolume)
            };
            _soundTrackBar.Scroll += SoundTrackBar_Scroll;
            _settingsPanel.Controls.Add(_soundTrackBar);
        }

        private void MusicTrackBar_Scroll(object sender, EventArgs e)
        {
            _mainMenuMusicChannel.Volume = (float)_musicTrackBar.Value / 100;
            Game.Data.ChangeMusicVolume(_musicTrackBar.Value);
        }

        private void SoundTrackBar_Scroll(object sender, EventArgs e)
        {
            Game.Data.ChangeSoundVolume(_soundTrackBar.Value);
        }

        private void SwitchForm(Form newForm)
        {
            _waveOut.Dispose();
            this.Hide();
            newForm.Show();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x1328)
                m.Result = (IntPtr)1;
            else
                base.WndProc(ref m);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Game.Data.SaveLastTwoPropertiesToFile();
            Application.Exit();
        }
    }
}
