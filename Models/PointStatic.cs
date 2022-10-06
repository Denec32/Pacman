using System.Drawing;
namespace Pacman
{
    public class PointStatic
    {
        protected int X;
        protected int Y;
        public Bitmap Sprite;

        public PointStatic(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int x
        {
            get { return X; }
            set { X = value; }
        }

        public int y
        {
            get { return Y; }
            set { Y = value; }
        }


    }
}
