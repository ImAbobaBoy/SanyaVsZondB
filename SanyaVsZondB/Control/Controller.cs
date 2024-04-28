using SanyaVsZondB.Model;
using System.Windows.Forms;
using System;

namespace SanyaVsZondB.Control
{
    public class Controller
    {
        public Form View { get; private set; }
        public Point Point { get; private set; }
        public Map Game {  get; private set; }
        public Timer ZondBMoveTimer { get; private set; }
        private int count = 0;

        public Controller(Form view, Point point, Map game)
        {
            View = view;
            Point = point;
            Game = game;

            ZondBMoveTimer = new Timer();
            ZondBMoveTimer.Interval = 16;
            ZondBMoveTimer.Tick += UpdateZombiePositions;
            ZondBMoveTimer.Start();
        }

        public void Shoot(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                Game.Shoot();
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
            Game.MoveZondBS();
            if (Game.Bullets.Count > 0)
                Game.MoveBullets();
            if (count < Game.Level.ZondBCount)    
                Game.SpawnZondB();
            count++;
        }

        private void View_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode.ToString())
            {
                case "A":
                    Game.Player.Move(MoveDirection.Left);
                    break;
                case "D":
                    Game.Player.Move(MoveDirection.Right);
                    break;
                case "W":
                    Game.Player.Move(MoveDirection.Up);
                    break;
                case "S":
                    Game.Player.Move(MoveDirection.Down);
                    break;
                case "E":
                    Game.SpawnFlower();
                    break;
            }
        }
    }
}
