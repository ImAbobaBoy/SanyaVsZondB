using SanyaVsZondB.Control;
using SanyaVsZondB.Model;
using SanyaVsZondB.View;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SanyaVsZondB
{
    public partial class SecondLevelForm : Form, IObserver
    {
        public Controller Controller { get; private set; }
        public Model.Point Point { get; private set; }
        public Map Map { get; private set; }
        public Game Game { get; private set; }

        public SecondLevelForm(Game game)
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            Game = game;
            Map = Game.CreateSecondLevelMap();
            Map.Player.RegisterObserver(this);
            Map.RegisterObserver(this);
            Point = Map.Player.Position; 
            Controller = new Controller(this, Point, Map);
            Controller.InitializeKeyHandling();

            Map.Player.PropertyChanged += Player_PropertyChanged;
            Map.PropertyChanged += ClearLevel_PropertyChanged;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
        }

        private void DrawCircle(Graphics g, int x, int y, int width, int height)
        {
            SolidBrush brush = new SolidBrush(Color.Red);
            g.FillEllipse(brush, x, y, width, height);
            brush.Dispose();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Map.ZondBs.Count > 0) { 
            }
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

            // Предположим, что текст должен быть отрисован непосредственно над головой зомби
            // Вы можете настроить эти значения в соответствии с вашими требованиями
            int textX = (int)zombiePosition.X - 30; // Сдвиг влево для центрирования
            int textY = (int)zombiePosition.Y - 70; // Сдвиг вверх для размещения над головой зомби

            // Используйте TextRenderer для отрисовки текста
            TextRenderer.DrawText(g, hpText, font, new System.Drawing.Point(textX, textY), Color.Black);

            // Освободите ресурсы
            font.Dispose();
        }

        public void Update(IObservable observable)
        {
            Map.Player.Target = new Model.Point(Cursor.Position.X, Cursor.Position.Y);
            this.Invalidate();
        }

        private void SwitchForm(Form newForm)
        {
            this.Hide();
            newForm.Show();
        }

        private void Player_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SwitchForm(new MainMenuForm());
        }

        private void ClearLevel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsLevelClear")
            {
                SwitchForm(new MainMenuForm());
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
