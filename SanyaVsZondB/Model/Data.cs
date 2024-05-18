using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace SanyaVsZondB.Model
{
    public class Data
    {
        public const string GameDataPath = "data\\GameData.txt";
        public const string LoadDataPath = "data\\LoadedData.txt";
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
        public int MusicVolume { get; private set; }
        public int SoundVolume { get; private set; }

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

        public void ChangeMusicVolume(int newValue)
        {
            if (newValue >= 0 && newValue <= 100)
                MusicVolume = newValue;
        }

        public void ChangeSoundVolume(int newValue)
        {
            if (newValue >= 0 && newValue <= 100)
                SoundVolume = newValue;
        }

        public void SavePropertiesToFile(int currentLevel)
        {
            using (StreamWriter writer = new StreamWriter(LoadDataPath))
            {
                writer.WriteLine(MultiplierZondBCount);
                writer.WriteLine(MultiplierZondBHp);
                writer.WriteLine(MultiplierZondBDamage);
                writer.WriteLine(MultiplierZondBSpeed);
                writer.WriteLine(ZondBSpawnFrequency);
                writer.WriteLine(IsFlowerCanShoot);
                writer.WriteLine(MultiplierFlowerHp);
                writer.WriteLine(MultiplierFlowerDamage);
                writer.WriteLine(MultiplierWeaponDamage);
                writer.WriteLine(AdditionalWeaponShootingFrequency);
                writer.WriteLine(AdditionalWeaponBulletsInQueue);
                writer.WriteLine(Player.Hp);
                writer.WriteLine(Player.Speed);
                writer.WriteLine(Player.Weapon.Name);
                writer.WriteLine(Player.Weapon.CountBulletsInQueue);
                writer.WriteLine(Player.Weapon.ShootingFrequency);
                writer.WriteLine(currentLevel);
                //writer.WriteLine();
            }
        }

        public int LoadPropertiesFromFile()
        {
            var currentLevel = 0;
            using (StreamReader reader = new StreamReader(LoadDataPath))
            {
                MultiplierZondBCount = double.Parse(reader.ReadLine());
                MultiplierZondBHp = double.Parse(reader.ReadLine());
                MultiplierZondBDamage = double.Parse(reader.ReadLine());
                MultiplierZondBSpeed = double.Parse(reader.ReadLine());
                ZondBSpawnFrequency = double.Parse(reader.ReadLine());
                IsFlowerCanShoot = bool.Parse(reader.ReadLine());
                MultiplierFlowerHp = double.Parse(reader.ReadLine());
                MultiplierFlowerDamage = double.Parse(reader.ReadLine());
                MultiplierWeaponDamage = double.Parse(reader.ReadLine());
                AdditionalWeaponShootingFrequency = int.Parse(reader.ReadLine());
                AdditionalWeaponBulletsInQueue = int.Parse(reader.ReadLine());
                Player.Hp = int.Parse(reader.ReadLine());
                Player.Speed = double.Parse(reader.ReadLine());
                Player.Weapon.Name = reader.ReadLine();
                Player.Weapon.CountBulletsInQueue = int.Parse(reader.ReadLine());
                Player.Weapon.ShootingFrequency = double.Parse(reader.ReadLine());
                currentLevel = int.Parse(reader.ReadLine());
            }
            return currentLevel;
        }

        public void SaveLastTwoPropertiesToFile()
        {
            using (StreamWriter writer = new StreamWriter(GameDataPath))
            {
                writer.WriteLine(MusicVolume);
                writer.WriteLine(SoundVolume);
            }
        }

        public void LoadLastTwoPropertiesFromFile()
        {
            using (StreamReader reader = new StreamReader(GameDataPath))
            {
                MusicVolume = int.Parse(reader.ReadLine());
                SoundVolume = int.Parse(reader.ReadLine());
            }
        }

        private void LoadPlayerWeaponFromFile(string name)
        {
            switch (name)
            {
                case "Pistol":
                    Player.ChangeWeapon(new Pistol(100, 1, 50, 4));
                    break;
                case "Rifle":
                    Player.ChangeWeapon(new Rifle(6, 1, 70, 10));
                    break;
                case "SniperRifle":
                    Player.ChangeWeapon(new SniperRifle(100, 1, 85, 0.5));
                    break;
            }
        }
    }
}
