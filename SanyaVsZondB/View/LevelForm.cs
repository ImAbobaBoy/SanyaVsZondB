using NAudio.Wave;
using SanyaVsZondB.Control;
using SanyaVsZondB.Model;
using SanyaVsZondB.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace SanyaVsZondB
{
    public partial class LevelForm : Form, IObserver
    {
        public Controller Controller { get; private set; }
        public Model.Point Point { get; private set; }
        public Map Map { get; private set; }
        public Game Game { get; private set; }
        private int _currentLevel;
        private bool _isShowStats;
        private Panel _statsPanel;
        private Panel _pausePanel;
        private Panel _settingsPanel;
        private TrackBar _musicTrackBar;
        private TrackBar _soundTrackBar;
        private WaveChannel32 _levelMusicChannel;
        private WaveOut _waveOutLevel;
        private const int _countLevels = 5;

        public LevelForm(Game game, int currentLevel, WaveOut waveOutLevel, WaveChannel32 levelMusicChannel)
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            Game = game;
            Map = CreateLevel(game, currentLevel);
            Map.Player.RegisterObserver(this);
            Map.RegisterObserver(this);
            Point = Map.Player.Position;
            Controller = new Controller(this, Point, Map);
            Controller.InitializeKeyHandling();
            _currentLevel = currentLevel;

            _statsPanel = new Panel
            {
                Size = new Size(165, 145), 
                BorderStyle = BorderStyle.FixedSingle, 
                Visible = false,
                ForeColor = Color.Red,
                BackColor = ColorTranslator.FromHtml("#140e10"),
            };
            Controls.Add(_statsPanel);

            _pausePanel = new Panel
            {
                Size = new Size(600, 484), 
                Location = new System.Drawing.Point((this.Width - 600) / 2, 298), 
                Visible = false, 
                ForeColor = ColorTranslator.FromHtml("#140e10"),
                BackColor = ColorTranslator.FromHtml("#140e10"),
            };
            Controls.Add(_pausePanel);
            AddButtonsToPausePanel();

            _settingsPanel = new Panel
            {
                Size = new Size(250, 250), 
                Location = new System.Drawing.Point(((this.Width - 600) / 2) - 270, 298), 
                ForeColor = ColorTranslator.FromHtml("#140e10"),
                BackColor = ColorTranslator.FromHtml("#140e10"),
                Visible = false 
            };
            Controls.Add(_settingsPanel);
            AddControlsToSettingsPanel();

            _musicTrackBar.Value = (int)Game.Data.MusicVolume;
            _soundTrackBar.Value = (int)Game.Data.SoundVolume;

            _levelMusicChannel = levelMusicChannel;
            _levelMusicChannel.Volume = Game.Data.MusicVolume / 100;
            _waveOutLevel = waveOutLevel;
            waveOutLevel.Play();

            this.BackgroundImage = Image.FromFile("images\\Background.jpg");
            this.BackgroundImageLayout = ImageLayout.Stretch;

            Map.Player.PropertyChanged += Player_PropertyChanged;
            Map.PropertyChanged += ClearLevel_PropertyChanged;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
        }

        private static Map CreateLevel(Game game, int currentLevel)
        {
            switch (currentLevel)
            {
                case 1:
                    return game.CreateFirstLevelMap();
                case 2:
                    return game.CreateSecondLevelMap();
                case 3:
                    return game.CreateThirdLevelMap();
                case 4:
                    return game.CreateFourthLevelMap();
                case 5:
                    return game.CreateFifthLevelMap();
                default:
                    return null;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Map.ZondBs.Count > 0)
                foreach (var zondB in Map.ZondBs)
                {
                    DrawObject(e, zondB.GetImageFileName(), zondB.Target, zondB.Position);
                    DrawZombieHp(e.Graphics, zondB.Position, zondB.Hp);
                }
            if (Map.Flowers.Count > 0)
                foreach (var flower in Map.Flowers)
                    DrawObject(e, flower.GetImageFileName(), flower.Target, flower.Position);

            if (Map.Bullets.Count > 0)
                foreach (var bullet in Map.Bullets)
                    DrawObject(e, bullet.GetImageFileName(), bullet.Target, bullet.Position);

            var hpText = $"HP: {Map.Player.Hp}";
            var font = new Font("Arial", 12);
            var brush = Brushes.Black;
            e.Graphics.DrawString(hpText, font, brush, new PointF(this.ClientSize.Width - 100, 10));

            base.OnPaint(e);

            DrawObject(e, Game.Data.Player.GetImageFileName(), Game.Data.Player.Target, Point);
        }

        private void DrawObject(PaintEventArgs e, Image image, Model.Point target, Model.Point position)
        {
            double angle = Math.Atan2(target.Y - position.Y, target.X - position.X) * 180 / Math.PI;

            Matrix rotationMatrix = new Matrix();
            rotationMatrix.RotateAt((float)angle, new PointF((float)position.X, (float)position.Y)); // Использование положительного угла

            GraphicsState state = e.Graphics.Save();
            e.Graphics.Transform = rotationMatrix;

            var playerWidth = (int)image.Width;
            var playerHeight = (int)image.Height;
            var x = (int)(position.X - playerWidth / 2);
            var y = (int)(position.Y - playerHeight / 2);
            e.Graphics.DrawImage(image, x, y, playerWidth, playerHeight);

            e.Graphics.Restore(state);
        }

        private void DrawZombieHp(Graphics g, Model.Point zombiePosition, int hp)
        {
            Font font = new Font("Arial", 10);

            string hpText = $"HP: {hp}";

            var textX = (int)zombiePosition.X - 30; 
            var textY = (int)zombiePosition.Y - 70; 

            TextRenderer.DrawText(g, hpText, font, new System.Drawing.Point(textX, textY), Color.Black);

            font.Dispose();
        }

        public void ShowStats()
        {
            _isShowStats = !_isShowStats;
            if (_isShowStats)
            {
                _statsPanel.Location = new System.Drawing.Point(0, 0);
                _statsPanel.Visible = true;
                FillStatsPanelWithGameData();
            }
            else
                _statsPanel.Visible = false;
        }

        private void AddButtonsToPausePanel()
        {
            var resumeButton = new Button
            {
                Image = Image.FromFile("images\\Resume.png"),
                Size = new Size(500, 167), 
                Location = new System.Drawing.Point(50, 50),
                TabIndex = 0,
                UseVisualStyleBackColor = true,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
            };
            resumeButton.Click += ResumeButtonClick;
            _pausePanel.Controls.Add(resumeButton);

            var mainMenuButton = new Button
            {
                Image = Image.FromFile("images\\Menu.png"),
                Size = new Size(500, 167),
                Location = new System.Drawing.Point(50, 267),
                TabIndex = 0,
                UseVisualStyleBackColor = true,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
            };
            mainMenuButton.Click += MainMenuButtonClick;
            _pausePanel.Controls.Add(mainMenuButton);
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
                Value = 50 
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
                Value = 50 
            };
            _soundTrackBar.Scroll += SoundTrackBar_Scroll;
            _settingsPanel.Controls.Add(_soundTrackBar);
        }

        private void MusicTrackBar_Scroll(object sender, EventArgs e)
        {
            _levelMusicChannel.Volume = (float)_musicTrackBar.Value / 100;
            Game.Data.ChangeMusicVolume(_musicTrackBar.Value);
        }

        private void SoundTrackBar_Scroll(object sender, EventArgs e)
        {
            Game.Data.ChangeSoundVolume(_soundTrackBar.Value);
        }

        private void FillStatsPanelWithGameData()
        {
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.ColumnCount = 1; 
            tableLayoutPanel.RowCount = 8; 
            tableLayoutPanel.Dock = DockStyle.Fill; 

            tableLayoutPanel.Controls
                .Add(new Label() { Text = $"HP: {Game.Data.Player.Hp}", AutoSize = true, ForeColor = Color.White }, 0, 0); 
            tableLayoutPanel.Controls
                .Add(new Label() { Text = $"Damage: {Game.Data.Player.Weapon.Damage}", AutoSize = true, ForeColor = Color.White }, 0, 1); 
            tableLayoutPanel.Controls
                .Add(new Label() { Text = $"Speed: {Game.Data.Player.Speed}", AutoSize = true, ForeColor = Color.White }, 0, 1); 
            tableLayoutPanel.Controls
                .Add(new Label() { Text = $"Weapon: {Game.Data.Player.Weapon.Name}", AutoSize = true, ForeColor = Color.White }, 0, 1); 
            tableLayoutPanel.Controls
                .Add(new Label() { Text = $"Shooting Frequency: {Game.Data.Player.Weapon.ShootingFrequency}", AutoSize = true, ForeColor = Color.White }, 0, 1); 
            tableLayoutPanel.Controls
                .Add(new Label() { Text = $"Bullets in Queue: {Game.Data.Player.Weapon.CountBulletsInQueue}", AutoSize = true, ForeColor = Color.White }, 0, 1); 
            tableLayoutPanel.Controls
                .Add(new Label() { Text = $"Flower HP: {100 * Game.Data.MultiplierFlowerHp}", AutoSize = true, ForeColor = Color.White }, 0, 1); 
            tableLayoutPanel.Controls
                .Add(new Label() { Text = $"Flower Damage: {15 * Game.Data.MultiplierFlowerDamage}", AutoSize = true, ForeColor = Color.White }, 0, 1); 
                                                                                   
            _statsPanel.Controls.Add(tableLayoutPanel);
        }

        public void ShowPauseForm()
        {
            _pausePanel.Visible = !Controller.IsPaused;
            _settingsPanel.Visible = !Controller.IsPaused;
        }

        public void Update(IObservable observable)
        {
            Map.Player.Target = new Model.Point(Cursor.Position.X, Cursor.Position.Y);
            this.Invalidate();
        }

        private void ResumeButtonClick(object sender, EventArgs e)
        {
            Game.Data.SaveLastTwoPropertiesToFile();
            Controller.MakePause();
            this.Focus();
        }

        private void MainMenuButtonClick(object sender, EventArgs e)
        {
            SwitchForm(-1);
        }

        private void SwitchForm(int nextLevel)
        {
            this.Hide();
            Form nextForm;
            if (nextLevel <= 0)
                nextForm = new MainMenuForm();
            else if (nextLevel > 0 && nextLevel < _countLevels)
                nextForm = new MovingToAnotherLevel(Game, _currentLevel + 1, _waveOutLevel, _levelMusicChannel);
            else 
                nextForm = new MainMenuForm();
            nextForm.Show();
            Controller.StopTimers();
            this.Dispose();
            _waveOutLevel.Stop();
        }

        private void Player_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsAlive")
                SwitchForm(0);
        }

        private void ClearLevel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsLevelClear")
            {
                Map.Player.PropertyChanged -= Player_PropertyChanged;
                Map.Player.IsWPressed = false;
                Map.Player.IsAPressed = false;
                Map.Player.IsSPressed = false;
                Map.Player.IsDPressed = false;
                Game.Data.SavePropertiesToFile(_currentLevel);
                SwitchForm(_currentLevel);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Game.Data.SaveLastTwoPropertiesToFile();
            Application.Exit();
        }
    }
}