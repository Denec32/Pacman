namespace Pacman
{
    class Dot : Tile
    {
        protected int score;

        public Dot(int x, int y)
            : base(x, y)
        {
            score = 10;
        }

        public int Score { get; }

    }
}
