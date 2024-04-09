using SanyaVsZondB.Control;
using SanyaVsZondB.Model;
using SanyaVsZondB.View;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SanyaVsZondB
{
    public partial class Form1 : Form, IObserver
    {
        public Controller Controller { get; private set; }
        public Model.Point Point { get; private set; }
        public Map Game { get; private set; }

        public Form1()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            Game = new Game().CreateMap();
            Game.Player.RegisterObserver(this);
            Game.RegisterObserver(this);
            Point = Game.Player.Position; // Инициализация целевой позиции
            Controller = new Controller(this, Point, Game);
            Controller.InitializeKeyHandling();
            this.MouseDown += MouseClick;
        }

        private void DrawCircle(Graphics g, int x, int y, int width, int height)
        {
            SolidBrush brush = new SolidBrush(Color.Red);
            g.FillEllipse(brush, x, y, width, height);
            brush.Dispose();
        }

        private void MouseClick(object sender, MouseEventArgs e)
        {
            Controller.Shoot(sender, e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Game.ZondBs.Count > 0)
                foreach (var zondB in Game.ZondBs)
                { 
                    DrawZombie(e.Graphics, (int)(zondB.Position.X - zondB.HitboxRadius), (int)(zondB.Position.Y - zondB.HitboxRadius), (int)zondB.HitboxRadius * 2, (int)zondB.HitboxRadius * 2);
                }

            if (Game.Bullets.Count > 0)
                foreach (var bullet in Game.Bullets)
                {
                    DrawBullet(e.Graphics, (int)(bullet.Position.X - bullet.HitboxRadius), (int)(bullet.Position.Y - bullet.HitboxRadius), (int)bullet.HitboxRadius * 2, (int)bullet.HitboxRadius * 2);
                }

            if (Game.Flowers.Count > 0)
                foreach(var flower in Game.Flowers)
                {
                    DrawFlower(e.Graphics, (int)(flower.Position.X - flower.HitboxRadius), (int)(flower.Position.Y - flower.HitboxRadius), (int)flower.HitboxRadius * 2, (int)flower.HitboxRadius * 2);
                }

            base.OnPaint(e);

            int x = (int)(Point.X - Game.Player.HitboxRadius);
            int y = (int)(Point.Y - Game.Player.HitboxRadius);

            DrawCircle(e.Graphics, x, y, (int)Game.Player.HitboxRadius * 2, (int)Game.Player.HitboxRadius * 2);
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

        public void Update(IObservable observable)
        {
            Game.Player.Target = new Model.Point(Cursor.Position.X, Cursor.Position.Y);
            this.Invalidate();   
        }
    }
}
