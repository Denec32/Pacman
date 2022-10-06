using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    public class PointMove : PointStatic
    {
        protected int Speed;
        protected int XDir;
        protected int YDir;

        public PointMove(int x, int y, int speed)
            : base(x, y)
        {
            Speed = speed;

        }

        public void Move()
        {
            if (X == 448)
            {
                X = -10;
            }

            else if (X == -16)
            {
                X = 446;
            }
            else
            {
                X += Speed * XDir;
                Y += Speed * YDir;
            }
        }

        public int xDir
        {
            get { return XDir; }
        }

        public int speed
        {
            get { return Speed; }
        }

        public int yDir
        {
            get { return YDir; }
        }

        public void ChangeDirection(int xDir, int yDir)
        {
            XDir = xDir;
            YDir = yDir;
        }

        public bool CheckCollision(int tx1, int ty1, int dx, int dy)
        {
            int tx2 = tx1 + 16;
            int ty2 = ty1 + 16;

            int x2 = x + 16 + speed * dx;
            int y2 = y + 16 + speed * dy;

            int x1 = x + speed * dx;
            int y1 = y + speed * dy;

            if (((x2 > tx1) && (y2 > ty1)) && ((x2 < tx2) && (y2 <= ty2)) ||
                ((x2 > tx1) && (y1 > ty1)) && ((x2 <= tx2) && (y1 < ty2)) ||
                ((x1 > tx1) && (y1 >= ty1)) && ((x1 < tx2) && (y1 < ty2)) ||
                ((x1 >= tx1) && (y2 > ty1)) && ((x1 < tx2) && (y2 < ty2))
                )
            {
                return true;
            }
            else return false;
        }

        public bool CheckCollision(int tx1, int ty1, int size)
        {
            int tx2 = tx1 + size;
            int ty2 = ty1 + size;

            int x2 = x + size + speed * xDir;
            int y2 = y + size + speed * yDir;

            int x1 = x + speed * xDir;
            int y1 = y + speed * yDir;


            if (((x2 > tx1) && (y2 > ty1)) && ((x2 < tx2) && (y2 <= ty2)) ||
                  ((x2 > tx1) && (y1 > ty1)) && ((x2 <= tx2) && (y1 < ty2)) ||
                  ((x1 > tx1) && (y1 >= ty1)) && ((x1 < tx2) && (y1 < ty2)) ||
                  ((x1 >= tx1) && (y2 > ty1)) && ((x1 < tx2) && (y2 < ty2))
                )
            {
                return true;
            }
            else return false;

        }

    }
}
