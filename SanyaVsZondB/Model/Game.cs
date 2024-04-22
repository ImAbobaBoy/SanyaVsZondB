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
        public Player Player { get; private set; }
        public List<(Action action, string name)> Improvements { get; private set; }

        public Game()
        {
            Player = new Player(100, new Point(0, 0), 50, 50, new Point(300, 300), new Pistol(100, 3, 20, 3), new List<Bullet>(), new List<ZondB>());
            Improvements = new List<(Action action, string name)>
            {
                (action: new Action(() => { Player.Hp += 100; }), name: "Увеличить хп"),
                (action: new Action(() => { Player.Speed += 100; }), name: "Увеличить скорость"),
                (action: new Action(() => { Player.Weapon.IncreaseCountBulletsInQueue(1); }), name: "Увеличить кол-во пуль в очереди на 1")
            };
        }

        public Map CreateFirstLevelMap() => new Map(1080, 1920, CreateLevel(1, 1, 1, 100, Player));
        public Map CreateSecondLevelMap() => new Map(1080, 1920, CreateLevel(2, 1, 1, 100, Player));

        private static Level CreateLevel(int zondBCount, double spawnFrequency, double zondBSpeed, int zondBHp, Player player) => 
            new Level(zondBCount, spawnFrequency, zondBSpeed, zondBHp, player);
    }
}
