using SanyaVsZondB.View;
using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SanyaVsZondB.Model
{
    public class Player : Entity, IObservable
    {
        public Weapon Weapon {  get; private set; }
        public List<ZondB> ZondBs { get; private set; }
        public List<Bullet> Bullets { get; private set; }
        public override int Hp { get; set; }
        public override Point Target { get; set; }
        public override double Speed { get; set; }
        public override double HitboxRadius { get; set; }
        public override Point Position { get; set; }
        private List<IObserver> observers = new List<IObserver>();

        public Player(
            int hp,
            Point target,
            double speed,
            double hitboxRadius,
            Point position,
            Weapon weapon,
            List<Bullet> bullets,
            List<ZondB> zondBs) : base(hp, target, speed, hitboxRadius, position)
        {
            Weapon = weapon;
            ZondBs = zondBs;
            Bullets = bullets;
        }

        public override void Move(Enum direction)
        {
            switch (direction)
            {
                case MoveDirection.Up:
                    Position.Y -= Speed;
                    break;
                case MoveDirection.Down:
                    Position.Y += Speed;
                    break;
                case MoveDirection.Left:
                    Position.X -= Speed;
                    break;
                case MoveDirection.Right:
                    Position.X += Speed;
                    break;
            }
            NotifyObservers();
        }

        public override void TakeDamage(int damage)
        {
            if (damage < Hp)
                Hp -= damage;
            else if (damage >= Hp)
                Die();
        }

        public override void Die()
        {
            throw new NotImplementedException();
        }

        public override string GetImageFileName()
        {
            throw new NotImplementedException("Лол как ты умер");
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
