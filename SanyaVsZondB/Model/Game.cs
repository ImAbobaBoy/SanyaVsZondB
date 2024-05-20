using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanyaVsZondB.Model
{
    public class Game
    {
        public Data Data { get; private set; }
        public List<(Action action, string name)> PlayerImprovements { get; private set; }
        public List<(Action action, string name)> ZondBImprovements { get; private set; }

        public Game()
        {
            Data = new Data();
            PlayerImprovements = new List<(Action action, string name)>
            {
                (action: new Action(() => { Data.Player.Hp += 100; }), name: "Увеличить хп"),
                (action: new Action(() => { Data.Player.Speed *= 1.1; }), name: "Увеличить скорость на 10%"),
                (action: new Action(() => { Data.IncreaseCountBulletsInQueue(1); }), name: "Увеличить кол-во пуль в очереди на 1"),
                (action: new Action(() => { Data.IncreaseDamage(1.2); }), name: "Увеличить урона на 20%"),
                (action: new Action(() => { Data.IncreaseShootingFrequency(1); }), name: "Увеличить скорость стрельбы на 1 п/с"),
                (action: new Action(() => 
                { 
                    Data.Player.ChangeWeapon(new Rifle(6, 1, 50, 10));
                    Data.Player.Weapon.IncreaseDamage(Data.MultiplierWeaponDamage);
                    Data.Player.Weapon.IncreaseShootingFrequency(Data.AdditionalWeaponShootingFrequency);
                    Data.Player.Weapon.IncreaseCountBulletsInQueue(Data.AdditionalWeaponBulletsInQueue);
                }), name: "Меняет оружие на винтовку"),
                (action: new Action(() => 
                { 
                    Data.Player.ChangeWeapon(new SniperRifle(100, 1, 85, 0.5));
                    Data.Player.Weapon.IncreaseDamage(Data.MultiplierWeaponDamage);
                    Data.Player.Weapon.IncreaseShootingFrequency(Data.AdditionalWeaponShootingFrequency);
                    Data.Player.Weapon.IncreaseCountBulletsInQueue(Data.AdditionalWeaponBulletsInQueue);
                }), name: "Меняет оружие на снайперскую винтовку"),
                (action: new Action(() => { Data.MakeFlowerCanShoot(); }), name: "Ваши цветы теперь могут стрелять"),
                (action: new Action(() => { Data.UpdateMultiplierFlowerHp(1.5); }), name: "Увеличивает здоровье для всех ваших цветов"),
                //(action: new Action(() => { ; }), name: ""),
            };

            ZondBImprovements = new List<(Action action, string name)>
            {
                (action: new Action(() => { Data.UpdateMultiplierZondBCount(1.5); }), name: "Увеличичть кол-во ЗондБэ в 1.5 раза"),
                (action: new Action(() => { Data.UpdateMultiplierZondBHp(1.5); }), name: "Увеличичть хп ЗондБэ в 1.5 раза"),
                (action: new Action(() => { Data.UpdateMultiplierZondBDamage(1.2); }), name: "Увеличить урон ЗондБэ на 20%"),
                (action: new Action(() => { Data.UpdateMultiplierZondBSpeed(1.5); }), name: "Увеличть скорость ЗондБэ в 1.5 раза"),
                //(action: new Action(() => { ; }), name: ""),
            };
        }

        public Map CreateFirstLevelMap() => new Map(1080, 1920, CreateLevel(1, 1, 1, 100, 20, Data));

        public Map CreateSecondLevelMap() => new Map(1080, 1920, CreateLevel(1, 1, 1.5, 100, 20, Data));

        public Map CreateThirdLevelMap() => new Map(1080, 1920, CreateLevel(1, 1, 0.5, 100, 20, Data));

        public Map CreateFourthLevelMap() => new Map(1080, 1920, CreateLevel(1, 1, 1, 100, 20, Data));

        public Map CreateFifthLevelMap() => new Map(1080, 1920, CreateLevel(1, 1, 1, 100, 20, Data));

        private Level CreateLevel(int zondBCount, double spawnFrequency, double zondBSpeed, int zondBHp, int zondBDamage, Data data) => 
            new Level(
                (int)(zondBCount * Data.MultiplierZondBCount), 
                spawnFrequency, 
                zondBSpeed * Data.MultiplierZondBSpeed, 
                (int)(zondBHp * Data.MultiplierZondBHp),
                (int)(zondBDamage * Data.MultiplierZondBDamage),
                data);
    }
}
