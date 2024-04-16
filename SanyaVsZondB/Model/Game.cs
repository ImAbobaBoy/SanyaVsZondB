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
        public Map CreateFirstLevelMap() => new Map(1080, 1920, CreateLevel(1, 1, 1, 50, 100, 100));
        public Map CreateSecondLevelMap() => new Map(1080, 1920, CreateLevel(2, 1, 1, 50, 100, 100));

        private static Level CreateLevel(int zondBCount, double spawnFrequency, double zondBSpeed, double playerSpeed, int zondBHp, int playerHp) => 
            new Level(zondBCount, spawnFrequency, zondBSpeed, playerSpeed, zondBHp, playerHp);
    }
}
