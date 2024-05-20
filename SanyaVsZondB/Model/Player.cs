using NAudio.Wave;
using SanyaVsZondB.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Deployment.Application;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SanyaVsZondB.Model
{
    public class Player : Entity, IObservable, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Weapon Weapon { get; private set; }
        public List<ZondB> ZondBs { get; private set; }
        public List<Bullet> Bullets { get; private set; }
        public Data Data { get; private set; }
        public bool IsAlive {  
            get { return isAlive;  }
            private set
            {
                if (isAlive != value)
                {
                    isAlive = value;
                    OnPropertyChanged(nameof(IsAlive));
                }
            }
        }
        public override int Hp { get; set; }
        public override Point Target { get; set; }
        public override double Speed { get; set; }
        public override double HitboxRadius { get; set; }
        public override Point Position { get; set; }
        public bool IsWPressed { get; set; } = false;
        public bool IsAPressed { get; set; } = false;
        public bool IsSPressed { get; set; } = false;
        public bool IsDPressed { get; set; } = false;
        private List<IObserver> observers = new List<IObserver>();
        private bool isAlive = true;
        private WaveChannel32 _soundChannel;
        private WaveOut _waveOut;

        public Player(
            int hp,
            Point target,
            double speed,
            double hitboxRadius,
            Point position,
            Weapon weapon,
            List<Bullet> bullets,
            List<ZondB> zondBs,
            Data data) 
                : base(hp, target, speed, hitboxRadius, position)
        {
            Weapon = weapon;
            ZondBs = zondBs;
            Bullets = bullets;
            Data = data;
            _waveOut = new WaveOut();
        }

        public override void Move(Enum direction)
        {
            if (IsWPressed)
                Position.Y -= Speed;
            if (IsAPressed)
                Position.X -= Speed;
            if (IsSPressed)
                Position.Y += Speed;
            if (IsDPressed)
                Position.X += Speed;
            NotifyObservers();
        }

        public override void TakeDamage(int damage)
        {
            var hitSound = new AudioFileReader("sounds\\HitPlayer.mp3");
            _soundChannel = new WaveChannel32(hitSound);
            _soundChannel.Volume = Data.SoundVolume / 100;
            _waveOut.Init(_soundChannel);
            if (damage < Hp)
            {
                Hp -= damage;
                _waveOut.Play();
            }
            else if (damage >= Hp)
                Die();
        }

        public override void Die()
        {
            var hitSound = new AudioFileReader("sounds\\PlayerDie.mp3");
            _soundChannel = new WaveChannel32(hitSound);
            _soundChannel.Volume = Data.SoundVolume / 100;
            _waveOut.Init(_soundChannel);
            _waveOut.Play();
            IsAlive = false;
            IsWPressed = false;
            IsAPressed = false;
            IsSPressed = false;
            IsDPressed = false;
        }

        public void ChangeWeapon(Weapon newWeapon)
        {
            Weapon = newWeapon;
        }

        public override Image GetImageFileName()
        {
            switch (Weapon.Name)
            {
                case "Pistol":
                    return Image.FromFile("images\\PlayerPistol.png");
                case "Rifle":
                    return Image.FromFile("images\\PlayerRifle.png");
                case "SniperRifle":
                    return Image.FromFile("images\\PlayerSniper.png");
                default:
                    return null;
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
