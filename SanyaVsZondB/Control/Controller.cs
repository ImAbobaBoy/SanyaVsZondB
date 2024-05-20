using SanyaVsZondB.Model;
using System.Windows.Forms;
using System;

namespace SanyaVsZondB.Control
{
    public class Controller
    {
        public LevelForm View { get; private set; }
        public Point Point { get; private set; }
        public Map Map {  get; private set; }
        public Timer MoveTimer { get; private set; }
        public Timer ZondBHitTimer { get; private set; }
        public Timer ShootTimer { get; private set; }
        public Timer TickTimer { get; private set; }
        public bool IsPaused { get; private set; }
        private int count = 0;
        private DateTime lastShotTime;
        private DateTime lastSpawnZondBTime;
        private readonly double _deltaShoot;
        private const double _deltaSpawnZondB = 2000;

        public Controller(LevelForm view, Point point, Map game)
        {
            View = view;
            Point = point;
            Map = game;
            _deltaShoot = 1000 / Map.Player.Weapon.ShootingFrequency;

            ZondBHitTimer = new Timer();
            ZondBHitTimer.Interval = 2000;
            ZondBHitTimer.Tick += HitZondB;
            ZondBHitTimer.Start();

            MoveTimer = new Timer();
            MoveTimer.Interval = 15;
            MoveTimer.Tick += UpdateZombiePositions;
            MoveTimer.Start();

            ShootTimer = new Timer();
            ShootTimer.Interval = (int)(1000 / Map.Player.Weapon.ShootingFrequency);
            ShootTimer.Tick += Shoot;
            ShootTimer.Stop();
        }

        public void InitializeKeyHandling()
        {
            View.KeyDown += View_KeyDown;
            View.KeyUp += View_KeyUp;
            View.MouseDown += MouseDown;
            View.MouseUp += MouseUp;
        }

        public void MakePause()
        {
            if (!IsPaused)
            {
                ZondBHitTimer.Stop();
                MoveTimer.Stop();
            }
            else
            {
                ZondBHitTimer.Start();
                MoveTimer.Start();
            }
            View.ShowPauseForm();
            IsPaused = !IsPaused;
        }

        public void StopTimers()
        {
            ShootTimer.Stop();
            ZondBHitTimer.Stop();
            MoveTimer.Stop();
        }

        private void MouseDown(object sender, MouseEventArgs e)
        {
            ShootTimer.Start();
            if ((DateTime.Now - lastShotTime).TotalMilliseconds >= _deltaShoot)
            {
                Map.ShootByPlayer();
                lastShotTime = DateTime.Now;
            }
        }

        private void MouseUp(object sender, MouseEventArgs e)
        {
            ShootTimer.Stop();
        }

        private void UpdateZombiePositions(object sender, EventArgs e)
        {
            Map.MoveZondBS();
            if (Map.Bullets.Count > 0)
                Map.MoveBullets();
            if (count < Map.Level.ZondBCount && (DateTime.Now - lastSpawnZondBTime).TotalMilliseconds >= _deltaSpawnZondB)
            {
                Map.SpawnZondB();
                lastSpawnZondBTime = DateTime.Now;
                count++;
            }

            Map.Player.Move(MoveDirection.None);
        }

        private void Shoot(object sender, EventArgs e)
        {
            Map.ShootByPlayer();
        }

        private void HitZondB(object sender, EventArgs e)
        {
            Map.HitZondB();
            Map.ShootByFlower();
        }

        private void View_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode.ToString())
            {
                case "A":
                    Map.Player.IsAPressed = true;
                    break;
                case "D":
                    Map.Player.IsDPressed = true;
                    break;
                case "W":
                    Map.Player.IsWPressed = true;
                    break;
                case "S":
                    Map.Player.IsSPressed = true;
                    break;
                case "E":
                    Map.SpawnFlower();
                    break;
                case "Tab":
                    View.ShowStats();
                    break;
                case "Escape":
                    MakePause();
                    break;
            }
        }

        private void View_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode.ToString())
            {
                case "A":
                    Map.Player.IsAPressed = false;
                    break;
                case "D":
                    Map.Player.IsDPressed = false;
                    break;
                case "W":
                    Map.Player.IsWPressed = false;
                    break;
                case "S":
                    Map.Player.IsSPressed = false;
                    break;
                case "Escape":
                    break;
            }
        }
    }
}
