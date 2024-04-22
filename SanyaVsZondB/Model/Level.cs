using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanyaVsZondB.Model
{
    public class Level
    {
        public int ZondBHp {  get; private set; }
        public int ZondBCount { get; private set; }
        public double SpawnFrequency {  get; private set; }
        public double ZondBSpeed { get; private set; }
        public Player Player { get; private set; }

        public Level(int zondBCount, double spawnFrequency, double zondBSpeed, int zondBHp, Player player)
        {
            ZondBCount = zondBCount;
            SpawnFrequency = spawnFrequency;
            ZondBSpeed = zondBSpeed;
            ZondBHp = zondBHp;
            Player = player;
        }
    }
}
