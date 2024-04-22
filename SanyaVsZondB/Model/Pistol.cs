using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanyaVsZondB.Model
{
    public class Pistol : Weapon
    {
        public static string Name { get; private set; }
        public Pistol(int damage, int countBulletsInQueue, int bulletSpeed, int shootingFrequency) 
            : base(Name, damage, countBulletsInQueue, bulletSpeed, shootingFrequency)
        {
        }
    }
}
