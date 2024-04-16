using SanyaVsZondB.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanyaVsZondB.Model
{
    public class ZondB : Entity, IObservable
    {
        public int Damage { get; private set; }
        public Player Player { get; private set; }
        public List<ZondB> ZondBS { get; private set; }
        public override int Hp { get; set; }
        public override Point Target { get; set; }
        public override double Speed { get; set; }
        public override double HitboxRadius { get; set; }
        public override Point Position { get; set; }
        public List<Flower> Flowers { get; private set; }
        public Map Map { get; private set; }
        private List<IObserver> observers = new List<IObserver>();

        public ZondB(
            int hp,
            double speed,
            double hitbox,
            Point position,
            int damage,
            Player player,
            List<ZondB> zondBS,
            List<Flower> flowers,
            Map map) : base(hp, player.Target, speed, hitbox, position)
        {
            Damage = damage;
            Player = player;
            ZondBS = zondBS;
            Flowers = flowers;
            Map = map;
        }

        public void Hit()
        {
            if (IsNear())
                if (Flowers.Count > 0)
                    Flowers[0].TakeDamage(Damage);
                else
                    Player.TakeDamage(Damage);
        }

        public override void Move(Enum direction)
        {
            if (Flowers.Count > 0)
                Target = Flowers[0].Position;
            else
                Target = Player.Position;
            var dx = (Target.X - Position.X) / CalculateDistance(Target);
            var dy = (Target.Y - Position.Y) / CalculateDistance(Target);
            Position.X += dx * Speed;
            Position.Y += dy * Speed;
        }

        public override string GetImageFileName()
        {
            throw new NotImplementedException();
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
            Map.IncrementCounKilledZondB();
            ZondBS.Remove(this);
        }

        private bool IsNear() => CalculateDistance(Target) <= Player.HitboxRadius + HitboxRadius;

        private double CalculateDistance(Point target) => Math.Sqrt(
                (target.X - Position.X) * (target.X - Position.X)
                + (target.Y - Position.Y) * (target.Y - Position.Y));

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