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
        public Player Player { get; private set; }
        public bool IsFlowerCanShoot { get; private set; }

        public Data()
        {
            MultiplierZondBCount = 1;
            MultiplierZondBHp = 1;
            MultiplierZondBDamage = 1;
            MultiplierZondBSpeed = 1;
            Player = new Player(100, new Point(0, 0), 5, 50, new Point(300, 300), new Pistol(100, 1, 50, 4), new List<Bullet>(), new List<ZondB>());
        }

        public void UpdateMultiplierZondBCount(double newValue)
        {
            if (newValue > 0)
                MultiplierZondBCount = newValue;
        }

        public void UpdateMultiplierZondBHp(double newValue)
        {
            if (newValue > 0)
                MultiplierZondBHp = newValue;
        }

        public void UpdateMultiplierZondBDamage(double newValue)
        {
            if (newValue > 0)
                MultiplierZondBDamage = newValue;
        }

        public void UpdateMultiplierZondBSpeed (double newValue)
        {
            if (newValue > 0)
                MultiplierZondBSpeed = newValue;
        }

        public void MakeFlowerCanShoot()
        {
            IsFlowerCanShoot = true;
        }
    }
}
