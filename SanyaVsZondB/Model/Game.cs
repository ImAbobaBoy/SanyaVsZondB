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

        public Player Player { get; private set; }
        public List<(Action action, string name)> Improvements { get; private set; }

        public Game()
        {
            Player = new Player(100, new Point(0, 0), 50, 50, new Point(300, 300), new Pistol(10, 1, 50, 3), new List<Bullet>(), new List<ZondB>());
            Improvements = new List<(Action action, string name)>
            {
                (action: new Action(() => { Player.Hp += 100; }), name: "Увеличить хп"),
                (action: new Action(() => { Player.Speed += 100; }), name: "Увеличить скорость"),
                (action: new Action(() => { Player.Weapon.IncreaseCountBulletsInQueue(1); }), name: "Увеличить кол-во пуль в очереди на 1"),
                (action: new Action(() => { _multiplierZondBCount = 1.5; }), name: "Увеличичть кол-во ЗондБэ в 1.5 раза"),
                (action: new Action(() => { _multiplierZondBHp = 1.5; }), name: "Увеличичть хп ЗондБэ в 1.5 раза"),
            };

            _multiplierZondBCount = 1;
            _multiplierZondBHp = 1;
        }

        public Map CreateFirstLevelMap() => new Map(1080, 1920, CreateLevel(15, 1, 1, 100, Player));

        public Map CreateSecondLevelMap() => new Map(1080, 1920, CreateLevel(10, 1, 1.5, 100, Player));

        public Map CreateThirdLevelMap() => new Map(1080, 1920, CreateLevel(25, 1, 0.5, 100, Player));

        public Map CreateFourthLevelMap() => new Map(1080, 1920, CreateLevel(50, 1, 1, 100, Player));

        public Map CreateFifthLevelMap() => new Map(1080, 1920, CreateLevel(75, 1, 1, 100, Player));

        private static Level CreateLevel(int zondBCount, double spawnFrequency, double zondBSpeed, int zondBHp, Player player) => 
            new Level((int)(zondBCount * _multiplierZondBCount), spawnFrequency, zondBSpeed, (int)(zondBHp * _multiplierZondBHp), player);
    }
}
