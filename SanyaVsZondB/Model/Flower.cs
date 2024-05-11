using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanyaVsZondB.Model
{
    public class Flower : Entity
    {
        public override int Hp { get; set; }
        public override Point Target { get; set; }
        public override double Speed { get; set; }
        public override double HitboxRadius { get; set; }
        public override Point Position { get; set; }
        public bool IsFlowerAlive { get; private set; }
        public static bool IsCanShoot { get; private set; }
        public List<Flower> Flowers { get; private set; }

        public Flower(int hp, Point target, double speed, double hitboxRadius, Point position, bool isFlowerAlive, List<Flower> flowers) : base(hp, target, speed, hitboxRadius, new Point(position))
        {
            this.IsFlowerAlive = isFlowerAlive;
            Flowers = flowers;
        }

        public override string GetImageFileName()
        {
            throw new NotImplementedException();
        }

        public override void Move(Enum direction)
        {
            return;
        }

        public override void TakeDamage(int damage)
        {
            if(damage < Hp)
                Hp -= damage;
            else if (damage >= Hp)
                Die();
        }

        public override void Die()
        {
            Flowers.Remove(this);
        }

        public static void MakeCanShooting()
        {
            IsCanShoot = true;
        }
    }
}
