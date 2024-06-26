﻿using SanyaVsZondB.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NAudio.Wave;
using System.Windows.Media;
using System.IO;
using System.Windows.Forms;

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
        private DateTime _lastSpawnFlower;
        private WaveChannel32 _flowerChannel;
        private const int DeltaSpawnFlower = 15000;
        private readonly WaveOut _waveOutFlower;
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
            _waveOutFlower = new WaveOut();
        }

        public void MoveZondBS()
        {
            foreach (var zondB in ZondBs)
            {
                zondB.Move(MoveDirection.Calculated);
            }
            NotifyObservers();
        }

        public void HitZondB()
        {
            foreach (var zondB in ZondBs)
                zondB.Hit();
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
            var zondB = new ZondB(Level.ZondBHp, Level.ZondBSpeed, 50, new Point(randomX, randomY), Level.ZondBDamage, Player, ZondBs, Flowers, this, Level.Data);
            ZondBs.Add(zondB);
            NotifyObservers();
            return zondB;
        }

        public void SpawnFlower()
        {
            if ((DateTime.Now - _lastSpawnFlower).TotalMilliseconds >= DeltaSpawnFlower && Flowers.Count < 2)
            {
                var spawnSound = new AudioFileReader("sounds\\FlowerSpawn.wav");
                _flowerChannel = new WaveChannel32(spawnSound);
                _flowerChannel.Volume = Level.Data.SoundVolume / 100;
                _waveOutFlower.Init(_flowerChannel);
                _waveOutFlower.Play();
                var flower = new Flower(
                    (int)(100 * Level.Data.MultiplierFlowerHp), 
                    new Point(0, 0), 
                    0, 
                    25, 
                    Player.Position, 
                    Flowers, 
                    (int)(15 * Level.Data.MultiplierFlowerDamage),
                    Level.Data.IsFlowerCanShoot);
                Flowers.Add(flower);
                NotifyObservers();
                _lastSpawnFlower = DateTime.Now;
            }
        }

        public async void ShootByPlayer()
        {
            //отложу до лучших времен
            //AudioFileReader audioFile;
            //switch (Level.Data.Player.Weapon.Name)
            //{
            //    case "Pistol":
            //        audioFile = new AudioFileReader("sounds\\WeaponPistol.wav");
            //        break;
            //    case "Rifle":
            //        audioFile = new AudioFileReader("sounds\\WeaponRifle.wav");
            //        break;
            //    case "SniperRifle":
            //        audioFile = new AudioFileReader("sounds\\WeaponSniperRifle.wav");
            //        break;
            //    default:
            //        audioFile = null;
            //        break;
            //}
            //_soundChannel = new WaveChannel32(audioFile);
            //_soundChannel.Volume = Level.Data.SoundVolume / 100;
            //_waveOut.Init(_soundChannel);
            //_waveOut.Play();
            var weapon = Level.Player.Weapon;
            for (var i = 0; i < weapon.CountBulletsInQueue; i++)
            {
                var bullet = new Bullet(1, new Point(Player.Target), weapon.BulletSpeed, 15, new Point(Player.Position), weapon.Damage, ZondBs, Bullets);
                Bullets.Add(bullet);
                await Task.Delay((int)(1000 / weapon.ShootingFrequency));
                NotifyObservers();
            }
        }


        public void ShootByFlower()
        {
            foreach (var flower in Flowers)
                if (ZondBs.Count > 0)
                    if (flower.IsCanShoot)
                    {
                        var bullet = new Bullet(1, new Point(ZondBs[0].Position), 50, 15, new Point(flower.Position), flower.Damage, ZondBs, Bullets);
                        Bullets.Add(bullet);
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
