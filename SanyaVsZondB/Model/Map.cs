﻿using SanyaVsZondB.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SanyaVsZondB.Model
{
    public class Map : IObservable, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public int Height { get; private set; }
        public int Width { get; private set; }
        public Level Level { get; private set; }
        public List<ZondB> ZondBs { get; private set; }
        public List<Bullet> Bullets { get; private set; }
        public List<Flower> Flowers { get; private set; }
        public Player Player { get; private set; }
        public int CountKilledZondB {  get; private set; }
        public bool IsLevelClear
        {
            get { return IsLevelClear; }
            private set
            {
                if (isLevelClear != value)
                {
                    isLevelClear = value;
                    OnPropertyChanged(nameof(IsLevelClear));
                }
            }
        }
        private Random random;
        private List<IObserver> observers = new List<IObserver>();
        private bool isLevelClear = false;

        public Map(int height, int width, Level level)
        {
            Height = height;
            Width = width;
            Level = level;
            ZondBs = new List<ZondB>();
            Bullets = new List<Bullet>();
            Flowers = new List<Flower>();
            random = new Random();
            Player = level.Player;
        }

        public void MoveZondBS()
        {
            foreach (var zondB in ZondBs)
            {
                zondB.Move(MoveDirection.Calculated);
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
            var zondB = new ZondB(Level.ZondBHp, Level.ZondBSpeed, 50, new Point(randomX, randomY), 20, Player, ZondBs, Flowers, this);
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

        public async void Shoot()
        {
            var weapon = Level.Player.Weapon;
            for (var i = 0; i < weapon.CountBulletsInQueue; i++)
            {
                var bullet = new Bullet(1, new Point(Player.Target), weapon.BulletSpeed, 15, new Point(Player.Position), weapon.Damage, ZondBs, Bullets);
                Bullets.Add(bullet);
                await Task.Delay(1000 / weapon.ShootingFrequency);
                NotifyObservers();
            }
        }

        public void IncrementCounKilledZondB()
        {
            CountKilledZondB++;
            if (CountKilledZondB == Level.ZondBCount)
            {
                IsLevelClear = true;
            }
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

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
