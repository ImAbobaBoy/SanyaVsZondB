using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanyaVsZondB.Model
{
    internal class SniperRifle : Weapon
    {
        public static string WeaponName { get; private set; }
        public override string Name { get; set; }
        public override int Damage { get; set; }
        public override int BulletSpeed { get; set; }
        public override int CountBulletsInQueue { get; set; }
        public override double ShootingFrequency { get; set; }

        public SniperRifle(int damage, int countBulletsInQueue, int bulletSpeed, double shootingFrequency)
            : base(WeaponName, damage, countBulletsInQueue, bulletSpeed, shootingFrequency)
        {
            WeaponName = "SniperRifle";
        }

        public override void IncreaseCountBulletsInQueue(int count)
        {
            CountBulletsInQueue += count;
        }

        public override void IncreaseDamage(double multiplier)
        {
            Damage = (int)(Damage * multiplier);
        }

        public override void IncreaseShootingFrequency(int count)
        {
            ShootingFrequency += count;
        }
    }
}
