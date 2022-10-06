using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    public class Ghost : PointMove
    {
        bool isDead;

        public Ghost(int x, int y, int speed = 1)
           : base(x, y, speed)
        {
            isDead = false;
            Sprite = new Bitmap(Properties.Resources.redDown);
        }

        public bool IsDead
        {
            get { return isDead; }
            set { isDead = value; }
        }

        public void Respawn()
        {
            x = 216;

            y = 224;

            isDead = false;

        }
    }
}
