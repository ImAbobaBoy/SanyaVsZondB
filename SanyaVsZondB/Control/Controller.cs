using SanyaVsZondB.Model;
using System.Windows.Forms;
using System;

namespace SanyaVsZondB.Control
{
    public class Controller
    {
        public Form View { get; private set; }
        public Point Point { get; private set; }
        public Map Map {  get; private set; }
        public Timer ZondBMoveTimer { get; private set; }
        public Timer ZondBHitTimer { get; private set; }
        private int count = 0;

        public Controller(Form view, Point point, Map game)
        {
            View = view;
            Point = point;
            Map = game;

            ZondBHitTimer = new Timer();
            ZondBHitTimer.Interval = 2000;
            ZondBHitTimer.Tick += HitZondB;
            ZondBHitTimer.Start();

            ZondBMoveTimer = new Timer();
            ZondBMoveTimer.Interval = 6;
            ZondBMoveTimer.Tick += UpdateZombiePositions;
            ZondBMoveTimer.Start();
        }

        public void Shoot(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                Map.Shoot();
        }

        public void InitializeKeyHandling()
        {
            View.KeyDown += View_KeyDown;
            View.MouseDown += MouseClick;
        }

        private void MouseClick(object sender, MouseEventArgs e)
        {
            Shoot(sender, e);
        }

        public void UpdateZombiePositions(object sender, EventArgs e)
        {
            Map.MoveZondBS();
            if (Map.Bullets.Count > 0)
                Map.MoveBullets();
            if (count < Map.Level.ZondBCount)    
                Map.SpawnZondB();
            count++;
        }

        public void HitZondB(object sender, EventArgs e)
        {
            Map.HitZondB();
        }

        private void View_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode.ToString())
            {
                case "A":
                    Map.Player.Move(MoveDirection.Left);
                    break;
                case "D":
                    Map.Player.Move(MoveDirection.Right);
                    break;
                case "W":
                    Map.Player.Move(MoveDirection.Up);
                    break;
                case "S":
                    Map.Player.Move(MoveDirection.Down);
                    break;
                case "E":
                    Map.SpawnFlower();
                    break;
            }
        }
    }
}
