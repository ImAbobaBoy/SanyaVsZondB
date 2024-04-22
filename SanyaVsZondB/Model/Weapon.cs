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
        public string Name {  get; private set; }
        public int Damage { get; private set; }
        public int BulletSpeed { get; private set; }
        public int CountBulletsInQueue { get; private set; }
        public int ShootingFrequency { get; private set; }
        public Weapon(string name, int damage, int countBulletsInQueue, int bulletSpeed, int shootingFrequency)
        {
            Name = name;
            Damage = damage;
            CountBulletsInQueue = countBulletsInQueue;
            BulletSpeed = bulletSpeed;
            ShootingFrequency = shootingFrequency;
        }

        public void IncreaseCountBulletsInQueue(int count)
        {
            CountBulletsInQueue += count;
        }
    }
}
