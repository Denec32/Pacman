using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    class Energizer : Dot
    {

        public Energizer(int x, int y)
            : base(x, y)
        {
            score = 50;

        }

    }
}
