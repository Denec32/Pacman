using System.Drawing;
namespace Pacman
{
    class Pacman : PointMove
    {
        int Lives;
        bool Powered;

        public Pacman(int x, int y, int speed = 1)
            : base(x, y, speed)
        {
            Sprite = new Bitmap(Properties.Resources.pacman);
            //Sprite.MakeTransparent(Color.White);
            Lives = 3;
            Powered = false;
        }


        public void Die()
        {
            X = 216;
            Y = 416;
            Lives--;
        }

        public int lives
        {
            get { return Lives; }
            set { Lives = value; }
        }

        public bool powered
        {
            get { return Powered; }
            set { Powered = value; }
        }

    }
}
