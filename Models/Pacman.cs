namespace Pacman
{
    class Pacman : PointMove
    {
        public int Lives { get; set; } = 3;
        public bool Powered { get; set; } = false;

        public Pacman(int x, int y, int speed = 1) : base(x, y, speed)
        {
            Sprite = new Bitmap(Properties.Resources.pacman);
        }

        public void Die()
        {
            X = 216;
            Y = 416;
            Lives--;
        }
    }
}
