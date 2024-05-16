﻿using SanyaVsZondB.Control;
using SanyaVsZondB.Model;
using SanyaVsZondB.View;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SanyaVsZondB
{
    public partial class LevelForm : Form, IObserver
    {
        public Controller Controller { get; private set; }
        public Model.Point Point { get; private set; }
        public Map Map { get; private set; }
        public Game Game { get; private set; }
        private int _currentLevel;
        private const int _countLevels = 5;
        private bool _isShowStats;
        private Panel _statsPanel;
        private Panel _pausePanel;

        public LevelForm(Game game, int currentLevel)
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
                Size = new Size(165, 170), // Размер панели
                Location = new System.Drawing.Point(-300, -200), // Невидимое положение
                BackColor = Color.LightGray, // Цвет фона
                BorderStyle = BorderStyle.FixedSingle // Обводка
            };
            Controls.Add(_statsPanel);

            _pausePanel = new Panel
            {
                Size = new Size(200, 100), // Установите желаемый размер
                Location = new System.Drawing.Point(10, 10), // Установите позицию
                BackColor = Color.DarkGray, // Установите цвет фона
                Visible = false // Сначала делаем панель невидимой
            };
            Controls.Add(_pausePanel);
            var resumeButton = new Button
            {
                Text = "Resume",
                Size = new Size(80, 40), // Установите желаемый размер
                Location = new System.Drawing.Point(50, 20) // Установите позицию
            };
            resumeButton.Click += ResumeButtonClick;
            _pausePanel.Controls.Add(resumeButton);

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

        private void DrawCircle(Graphics g, int x, int y, int width, int height)
        {
            SolidBrush brush = new SolidBrush(Color.Red);
            g.FillEllipse(brush, x, y, width, height);
            brush.Dispose();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Map.ZondBs.Count > 0)
                foreach (var zondB in Map.ZondBs)
                {
                    DrawZombie(e.Graphics,
                        (int)(zondB.Position.X - zondB.HitboxRadius),
                        (int)(zondB.Position.Y - zondB.HitboxRadius),
                        (int)zondB.HitboxRadius * 2,
                        (int)zondB.HitboxRadius * 2);
                    DrawZombieHp(e.Graphics, zondB.Position, zondB.Hp);
                }

            if (Map.Bullets.Count > 0)
                foreach (var bullet in Map.Bullets)
                {
                    DrawBullet(e.Graphics,
                        (int)(bullet.Position.X - bullet.HitboxRadius),
                        (int)(bullet.Position.Y - bullet.HitboxRadius),
                        (int)bullet.HitboxRadius * 2,
                        (int)bullet.HitboxRadius * 2);
                }

            if (Map.Flowers.Count > 0)
                foreach (var flower in Map.Flowers)
                {
                    DrawFlower(e.Graphics,
                        (int)(flower.Position.X - flower.HitboxRadius),
                        (int)(flower.Position.Y - flower.HitboxRadius),
                        (int)flower.HitboxRadius * 2,
                        (int)flower.HitboxRadius * 2);
                }

            string hpText = $"HP: {Map.Player.Hp}";
            Font font = new Font("Arial", 12);
            Brush brush = Brushes.Black;
            e.Graphics.DrawString(hpText, font, brush, new PointF(this.ClientSize.Width - 100, 10));

            base.OnPaint(e);

            int x = (int)(Point.X - Map.Player.HitboxRadius);
            int y = (int)(Point.Y - Map.Player.HitboxRadius);

            DrawCircle(e.Graphics, x, y, (int)Map.Player.HitboxRadius * 2, (int)Map.Player.HitboxRadius * 2);
        }

        private void DrawFlower(Graphics g, int x, int y, int width, int height)
        {
            SolidBrush brush = new SolidBrush(Color.Yellow);
            g.FillEllipse(brush, x, y, width, height);
            brush.Dispose();
        }

        private void DrawBullet(Graphics g, int x, int y, int width, int height)
        {
            SolidBrush brush = new SolidBrush(Color.Blue);
            g.FillEllipse(brush, x, y, width, height);
            brush.Dispose();
        }

        private void DrawZombie(Graphics g, int x, int y, int width, int height)
        {
            SolidBrush brush = new SolidBrush(Color.Green);
            g.FillEllipse(brush, x, y, width, height);
            brush.Dispose();
        }

        private void DrawZombieHp(Graphics g, Model.Point zombiePosition, int hp)
        {
            Font font = new Font("Arial", 10);

            string hpText = $"HP: {hp}";

            int textX = (int)zombiePosition.X - 30; 
            int textY = (int)zombiePosition.Y - 70; 

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

        private void FillStatsPanelWithGameData()
        {
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.ColumnCount = 1; 
            tableLayoutPanel.RowCount = 8; 
            tableLayoutPanel.Dock = DockStyle.Fill; 

            tableLayoutPanel.Controls.Add(new Label() { Text = $"HP: {Game.Data.Player.Hp}", AutoSize = true }, 0, 0); 
            tableLayoutPanel.Controls.Add(new Label() { Text = $"Damage: {Game.Data.Player.Weapon.Damage}", AutoSize = true }, 0, 1); 
            tableLayoutPanel.Controls.Add(new Label() { Text = $"Speed: {Game.Data.Player.Speed}", AutoSize = true }, 0, 1); 
            tableLayoutPanel.Controls.Add(new Label() { Text = $"Weapon: {Game.Data.Player.Weapon.Name}", AutoSize = true }, 0, 1); 
            tableLayoutPanel.Controls.Add(new Label() { Text = $"Shooting Frequency: {Game.Data.Player.Weapon.ShootingFrequency}", AutoSize = true }, 0, 1); 
            tableLayoutPanel.Controls.Add(new Label() { Text = $"Bullets in Queue: {Game.Data.Player.Weapon.CountBulletsInQueue}", AutoSize = true }, 0, 1); 
            tableLayoutPanel.Controls.Add(new Label() { Text = $"Flower HP: {100 * Game.Data.MultiplierFlowerHp}", AutoSize = true }, 0, 1); 
            tableLayoutPanel.Controls.Add(new Label() { Text = $"Flower Damage: {15 * Game.Data.MultiplierFlowerDamage}", AutoSize = true }, 0, 1); 
                                                                                   
            _statsPanel.Controls.Add(tableLayoutPanel);
        }

        //public void ShowPauseForm()
        //{
        //    var pauseForm = new PauseForm();
        //    if (!Controller.IsPaused)
        //        pauseForm.Show();
        //    else pauseForm.Hide();
        //}

        public void ShowPauseForm()
        {
            _pausePanel.Visible = !Controller.IsPaused;
        }

        public void Update(IObservable observable)
        {
            Map.Player.Target = new Model.Point(Cursor.Position.X, Cursor.Position.Y);
            this.Invalidate();
        }

        private void ResumeButtonClick(object sender, EventArgs e)
        {
            Controller.MakePause();
            this.Focus();
        }

        private void SwitchForm(int nextLevel)
        {
            this.Hide();
            Form nextForm;
            if (nextLevel <= 0)
                nextForm = new MainMenuForm();
            if (nextLevel > 0 && nextLevel < _countLevels)
                nextForm = new MovingToAnotherLevel(Game, _currentLevel + 1);
           else 
                nextForm = new MainMenuForm();
            nextForm.Show();
        }

        private void Player_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsAlive")
            {
                SwitchForm(0);
            }
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
                SwitchForm(_currentLevel);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}