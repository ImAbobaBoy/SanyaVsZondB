using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanyaVsZondB.Model
{
    public class Game
    {
        public Map CreateMap() => new Map(1080, 1920, CreateLevel());

        private static Level CreateLevel() => new Level(5, 1, 1, 50, 100, 100);
    }
}
