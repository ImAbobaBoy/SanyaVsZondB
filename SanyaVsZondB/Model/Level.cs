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
        public int PlayerHp {  get; private set; }
        public int ZondBHp {  get; private set; }
        public int ZondBCount { get; private set; }
        public double SpawnFrequency {  get; private set; }
        public double ZondBSpeed { get; private set; }
        public double PlayerSpeed { get; private set; }
        public Player Player { get; private set; }
        public Weapon StartWeapon { get; private set; }

        public Level(int zondBCount, double spawnFrequency, double zondBSpeed, double playerSpeed, int zondBHp, int playerHp)
        {
            ZondBCount = zondBCount;
            SpawnFrequency = spawnFrequency;
            ZondBSpeed = zondBSpeed;
            PlayerSpeed = playerSpeed;
            ZondBHp = zondBHp;
            PlayerHp = playerHp;
            StartWeapon = new Weapon("Pistol", 10);
            Player = new Player(playerHp, new Point(0, 0), playerSpeed, 50, new Point(300, 300), StartWeapon, new List<Bullet>(), new List<ZondB>());
        }
    }
}
