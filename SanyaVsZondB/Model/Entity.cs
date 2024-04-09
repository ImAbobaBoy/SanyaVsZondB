using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SanyaVsZondB.Model
{
    public abstract class Entity
    {
        public abstract int Hp {  get; set; }
        public abstract Point Target { get; set; }
        public abstract double Speed { get; set; }
        public abstract double HitboxRadius { get; set; }
        public abstract Point Position { get; set; }

        public Entity(int hp, Point target, double speed, double hitboxRadius, Point position)
        {
            Hp = hp;
            Target = target;
            Speed = speed;
            HitboxRadius = hitboxRadius;
            Position = position;
        }

        public abstract string GetImageFileName();

        public abstract void TakeDamage(int damage);

        public abstract void Die();

        public abstract void Move(Enum direction);
    }
}
