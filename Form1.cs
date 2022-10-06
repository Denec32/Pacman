namespace Pacman
{
    public partial class PacmanForms : Form
    {
        public PacmanForms()
        {
            InitializeComponent();
            DoubleBuffered = true; // fixes flickering
            KeyPreview = true;

            ghosts[0] = new Ghost(192, 224);
            ghosts[1] = new Ghost(208, 224);
            ghosts[2] = new Ghost(224, 224);
            ghosts[3] = new Ghost(240, 224);

            foreach (var ghost in ghosts)
            {
                ghost.ChangeDirection(1 - 2 * rnd.Next(0, 2), 0);
            }

            rnd = new Random();

            KeyDown += Keyboard.OnKeyDown;
            KeyUp += Keyboard.OnKeyUp;

            highScore = Properties.Settings.Default.HighScore;

            /* timer creation */

            pacmanMove.Tick += new EventHandler(PacmanTick);
            pacmanMove.Interval = 1;

            ghostMove.Tick += new EventHandler(GhostTick);
            ghostMove.Interval = 1;

            startMenu.Tick += new EventHandler(StartMenu);
            startMenu.Interval = 1;
            startMenu.Start();

            startDeathMenu.Tick += new EventHandler(StartDeathMenu);
            startDeathMenu.Interval = 1;

            powerUp.Tick += new EventHandler(PowerUp);
            powerUp.Interval = 10000;

            deathPenalty.Tick += new EventHandler(DeathPenalty);
            deathPenalty.Interval = 10000;

            pacmanAnimation.Tick += new EventHandler(PacmanAnimation);
            pacmanAnimation.Interval = 60;

            ghostAnimation.Tick += new EventHandler(GhostAnimation);
            ghostAnimation.Interval = 60;


            /* tile map init */
            for (int i = 0; i < digitMap.GetLength(0); i++)
            {
                for (int j = 0; j < digitMap.GetLength(1); j++)
                {
                    if (digitMap[i, j] == 1)
                    {
                        objectMap[i, j] = new Wall(j, i + 3);
                    }

                    else if (digitMap[i, j] == 2)
                    {
                        objectMap[i, j] = new Dot(j, i + 3);
                        dotsNumber++;
                    }
                    else if (digitMap[i, j] == 3)
                    {
                        objectMap[i, j] = new Energizer(j, i + 3);
                    }

                    else
                    {
                        objectMap[i, j] = new Tile(j, i + 3);
                    }
                }
            }

            nums[0] = Properties.Resources._0;
            nums[1] = Properties.Resources._1;
            nums[2] = Properties.Resources._2;
            nums[3] = Properties.Resources._3;
            nums[4] = Properties.Resources._4;
            nums[5] = Properties.Resources._5;
            nums[6] = Properties.Resources._6;
            nums[7] = Properties.Resources._7;
            nums[8] = Properties.Resources._8;
            nums[9] = Properties.Resources._9;
        }
    }
}