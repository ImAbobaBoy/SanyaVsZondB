using SanyaVsZondB.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanyaVsZondB.Model
{
    public class Map : IObservable
    {
        public int Height { get; private set; }
        public int Width { get; private set; }
        public Level Level { get; private set; }
        public List<ZondB> ZondBs { get; private set; }
        public List<Bullet> Bullets { get; private set; }
        public List<Flower> Flowers { get; private set; }
        public Player Player { get; private set; }
        private Random random;
        private List<IObserver> observers = new List<IObserver>();

        public Map(int height, int width, Level level)
        {
            Height = height;
            Width = width;
            Level = level;
            ZondBs = new List<ZondB>();
            Bullets = new List<Bullet>();
            Flowers = new List<Flower>();
            random = new Random();
            Player = new Player(
                Level.PlayerHp, 
                new Point(width / 2, width), 
                Level.PlayerSpeed, 50, 
                new Point(width / 2, height / 2), 
                Level.StartWeapon, Bullets, ZondBs);
            
        }

        public void MoveZondBS()
        {
            foreach (var zondB in ZondBs)
            {
                zondB.Move(MoveDirection.Up);
                zondB.Hit();
            }
            NotifyObservers();
        }

        public void MoveBullets()
        {
            foreach (var bullet in Bullets)
            {
                if (bullet.HitZondB())
                    break;
                bullet.Move(MoveDirection.Up);
            }
            NotifyObservers();
        }

        public ZondB SpawnZondB()
        {
            var randomX = random.Next(Width);
            var randomY = random.Next(Height);
            var zondB = new ZondB(Level.ZondBHp, Level.ZondBSpeed, 50, new Point(randomX, randomY), 20, Player, ZondBs, Flowers);
            ZondBs.Add(zondB);
            NotifyObservers();
            return zondB;
        }

        public Flower SpawnFlower()
        {
            var flower = new Flower(100, new Point(0, 0), 0, 25, Player.Position, true, Flowers);
            Flowers.Add(flower);
            NotifyObservers();
            return flower;
        }

        public Bullet Shoot()
        {
            var bullet = new Bullet(1, new Point(Player.Target), 10, 15, new Point(Player.Position), Player.Weapon.Damage, ZondBs, Bullets);
            Bullets.Add(bullet);
            NotifyObservers();
            return bullet;
        }

        public void RegisterObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in observers)
                observer.Update(this);
        }
    }
}
