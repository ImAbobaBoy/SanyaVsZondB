using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanyaVsZondB.Model
{
    public class Game
    {
        //
        //some game params
        //

        private static double _multiplierZondBCount;
        private static double _multiplierZondBHp;
        private static double _multiplierZondBDamage;
        private static double _multiplierZondBSpeed;

        public Player Player { get; private set; }
        public List<(Action action, string name)> PlayerImprovements { get; private set; }
        public List<(Action action, string name)> ZondBImprovements { get; private set; }

        public Game()
        {
            Player = new Player(100, new Point(0, 0), 5, 50, new Point(300, 300), new Pistol(100, 1, 50, 4), new List<Bullet>(), new List<ZondB>());
            PlayerImprovements = new List<(Action action, string name)>
            {
                (action: new Action(() => { Player.Hp += 100; }), name: "Увеличить хп"),
                (action: new Action(() => { Player.Speed *= 1.1; }), name: "Увеличить скорость на 10%"),
                (action: new Action(() => { Player.Weapon.IncreaseCountBulletsInQueue(1); }), name: "Увеличить кол-во пуль в очереди на 1"),
                (action: new Action(() => { Player.Weapon.IncreaseDamage(1.2); }), name: "Увеличить урона на 20%"),
                (action: new Action(() => { Player.Weapon.IncreaseShootingFrequency(1); }), name: "Увеличить скорость стрельбы на 1 п/с"),
                (action: new Action(() => { Player.ChangeWeapon(new Rifle(6, 1, 70, 10)); }), name: "Меняет оружие на винтовку"),
                (action: new Action(() => { Player.ChangeWeapon(new SniperRifle(100, 1, 85, 0.5)); }), name: "Меняет оружие на снайперскую винтовку"),
                //(action: new Action(() => { ; }), name: ""),
            };

            ZondBImprovements = new List<(Action action, string name)>
            {
                (action: new Action(() => { _multiplierZondBCount = 1.5; }), name: "Увеличичть кол-во ЗондБэ в 1.5 раза"),
                (action: new Action(() => { _multiplierZondBHp = 1.5; }), name: "Увеличичть хп ЗондБэ в 1.5 раза"),
                (action: new Action(() => { _multiplierZondBDamage = 1.2; }), name: "Увеличить урон ЗондБэ на 20%"),
                (action: new Action(() => { _multiplierZondBSpeed = 1.5; }), name: "Увеличть скорость ЗондБэ в 1.5 раза"),
                //(action: new Action(() => { ; }), name: ""),
            };

            _multiplierZondBCount = 1;
            _multiplierZondBHp = 1;
            _multiplierZondBDamage = 1;
            _multiplierZondBSpeed = 1;
        }

        public Map CreateFirstLevelMap() => new Map(1080, 1920, CreateLevel(1, 1, 1, 100, 20, Player));

        public Map CreateSecondLevelMap() => new Map(1080, 1920, CreateLevel(10, 1, 1.5, 100, 20, Player));

        public Map CreateThirdLevelMap() => new Map(1080, 1920, CreateLevel(25, 1, 0.5, 100, 20, Player));

        public Map CreateFourthLevelMap() => new Map(1080, 1920, CreateLevel(50, 1, 1, 100, 20, Player));

        public Map CreateFifthLevelMap() => new Map(1080, 1920, CreateLevel(75, 1, 1, 100, 20, Player));

        private static Level CreateLevel(int zondBCount, double spawnFrequency, double zondBSpeed, int zondBHp, int zondBDamage, Player player) => 
            new Level(
                (int)(zondBCount * _multiplierZondBCount), 
                spawnFrequency, 
                zondBSpeed * _multiplierZondBSpeed, 
                (int)(zondBHp * _multiplierZondBHp),
                (int)(zondBDamage * _multiplierZondBDamage),
                player);
    }
}
