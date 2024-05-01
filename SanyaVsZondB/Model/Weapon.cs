using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SanyaVsZondB.Model
{
    public abstract class Weapon
    {
        public abstract string Name {  get; set; }
        public abstract int Damage { get; set; }
        public abstract int BulletSpeed { get; set; }
        public abstract int CountBulletsInQueue { get; set; }
        public abstract int ShootingFrequency { get; set; }
        public Weapon(string name, int damage, int countBulletsInQueue, int bulletSpeed, int shootingFrequency)
        {
            Name = name;
            Damage = damage;
            CountBulletsInQueue = countBulletsInQueue;
            BulletSpeed = bulletSpeed;
            ShootingFrequency = shootingFrequency;
        }

        public abstract void IncreaseCountBulletsInQueue(int count);

        public abstract void IncreaseDamage(double multiplier);

        public abstract void IncreaseShootingFrequency(int count);
    }
}
