using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanyaVsZondB.Model
{
    public class Bullet : Entity
    {
        public int Damage { get; private set; }
        public List<ZondB> ZondBs { get; private set; }
        public List<Bullet> Bullets { get; private set; }
        public override int Hp { get; set; }
        public override Point Target { get; set; }
        public override double Speed { get; set; }
        public override double HitboxRadius { get; set; }
        public override Point Position { get; set; }
        private double dx;
        private double dy;

        public Bullet(
            int hp,
            Point target,
            double speed,
            double hitboxRadius,
            Point position,
            int damage,
            List<ZondB> zondBs,
            List<Bullet> bullets) : base(hp, new Point(target), speed, hitboxRadius, position)
        {
            Damage = damage;
            ZondBs = zondBs;
            dx = (Target.X - Position.X) / CalculateDistance(Target);
            dy = (Target.Y - Position.Y) / CalculateDistance(Target);
            Bullets = bullets;
        }

        public bool HitZondB()
        {
            foreach (var zondB in ZondBs)
                if (IsIntersectsWith(zondB))
                {
                    zondB.TakeDamage(Damage);
                    Die();
                    return true;
                    break;
                }
            return false;
        }

        public override void Move(Enum direction)
        {
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
            Bullets.Remove(this);
        }

        private bool IsIntersectsWith(ZondB zondB) => CalculateDistance(zondB.Position) <= HitboxRadius + zondB.HitboxRadius;

        private double CalculateDistance(Point target) => Math.Sqrt(
                (target.X - Position.X) * (target.X - Position.X)
                + (target.Y - Position.Y) * (target.Y - Position.Y));
    }
}
