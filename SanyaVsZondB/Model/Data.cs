using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace SanyaVsZondB.Model
{
    public class Data
    { 
        public double MultiplierZondBCount {  get; private set; }
        public double MultiplierZondBHp {  get; private set; }
        public double MultiplierZondBDamage { get; private set; }
        public double MultiplierZondBSpeed { get; private set; }
        public double ZondBSpawnFrequency { get; private set; }
        public Player Player { get; private set; }
        public bool IsFlowerCanShoot { get; private set; }
        public double MultiplierFlowerHp { get; private set; }
        public double MultiplierFlowerDamage { get; private set; }
        public double MultiplierWeaponDamage { get; private set; }
        public int AdditionalWeaponShootingFrequency { get; private set; }
        public int AdditionalWeaponBulletsInQueue { get; private set; }

        private const int InitialPlayerHP = 100;
        private const double InitialPlayerSpeed = 5;
        private const double PlayerHitboxRadius = 50;
        private Pistol _initialWeapon = new Pistol(100, 1, 50, 4);

        public Data()
        {
            MultiplierZondBCount = 1;
            MultiplierZondBHp = 1;
            MultiplierZondBDamage = 1;
            MultiplierZondBSpeed = 1;
            ZondBSpawnFrequency = 1;
            Player = new Player(
                InitialPlayerHP, 
                new Point(0, 0),
                InitialPlayerSpeed,
                PlayerHitboxRadius, 
                new Point(300, 300),
                _initialWeapon, 
                new List<Bullet>(), 
                new List<ZondB>());
            MultiplierFlowerHp = 1;
            MultiplierFlowerDamage = 1;
            MultiplierWeaponDamage = 1;
        }

        public void UpdateMultiplierZondBCount(double multiplier)
        {
            if (multiplier > 0)
                MultiplierZondBCount *= multiplier;
        }

        public void UpdateMultiplierZondBHp(double multiplier)
        {
            if (multiplier > 0)
                MultiplierZondBHp *= multiplier;
        }

        public void UpdateMultiplierZondBDamage(double multiplier)
        {
            if (multiplier > 0)
                MultiplierZondBDamage *= multiplier;
        }

        public void UpdateMultiplierZondBSpeed (double multiplier)
        {
            if (multiplier > 0)
                MultiplierZondBSpeed *= multiplier;
        }

        public void MakeFlowerCanShoot()
        {
            IsFlowerCanShoot = true;
        }

        public void UpdateMultiplierFlowerHp(double newValue)
        {
            MultiplierFlowerHp = newValue;
        }

        public void IncreaseCountBulletsInQueue(int addedValue)
        {
            Player.Weapon.IncreaseCountBulletsInQueue(addedValue);
            AdditionalWeaponBulletsInQueue += addedValue;
        }

        public void IncreaseDamage(double multiplier)
        {
            Player.Weapon.IncreaseDamage(multiplier);
            MultiplierWeaponDamage *= multiplier;
        }

        public void IncreaseShootingFrequency(int addedValue)
        {
            Player.Weapon.IncreaseShootingFrequency(addedValue);
            AdditionalWeaponShootingFrequency += addedValue;
        }
    }
}
