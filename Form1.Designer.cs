﻿using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using Timer = System.Windows.Forms.Timer;

namespace Pacman
{
    partial class PacmanForms
    {
        static readonly Timer pacmanMove = new Timer();

        static readonly Timer ghostMove = new Timer();

        static readonly Timer startMenu = new Timer();

        static readonly Timer startDeathMenu = new Timer();

        static readonly Timer powerUp = new Timer();

        static readonly Timer deathPenalty = new Timer();

        static readonly Timer pacmanAnimation = new Timer();

        static readonly Timer ghostAnimation = new Timer();

        Pacman pacman = new Pacman(216, 416);

        bool freeMove = false;

        int preMove = 0;

        byte dotsNumber = 0;

        bool showHelp = false;

        bool showMain = true;

        int animationFrame = 0;

        int ghostFrame = 0;

        int currentScore = 0;

        int highScore;

        Ghost[] ghosts = new Ghost[4];

        Random rnd = new Random();

        Bitmap mainMenu = new Bitmap(Properties.Resources.MainMenu, 448, 576);
        Bitmap helpMenu = new Bitmap(Properties.Resources.HelpMenu, 448, 576);

        Tile[,] objectMap = new Tile[31, 28];

        Bitmap[] nums = new Bitmap[10];

        byte[,] digitMap = new byte[31, 28] {
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 }, //1
            {1,2,2,2,2,2,2,2,2,2,2,2,2,1,1,2,2,2,2,2,2,2,2,2,2,2,2,1 }, //2
            {1,2,1,1,1,1,2,1,1,1,1,1,2,1,1,2,1,1,1,1,1,2,1,1,1,1,2,1 }, //3
            {1,3,1,1,1,1,2,1,1,1,1,1,2,1,1,2,1,1,1,1,1,2,1,1,1,1,3,1 }, //4
            {1,2,1,1,1,1,2,1,1,1,1,1,2,1,1,2,1,1,1,1,1,2,1,1,1,1,2,1 }, //5
            {1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1 }, //6
            {1,2,1,1,1,1,2,1,1,2,1,1,1,1,1,1,1,1,2,1,1,2,1,1,1,1,2,1 }, //7
            {1,2,1,1,1,1,2,1,1,2,1,1,1,1,1,1,1,1,2,1,1,2,1,1,1,1,2,1 }, //8
            {1,2,2,2,2,2,2,1,1,2,2,2,2,1,1,2,2,2,2,1,1,2,2,2,2,2,2,1 }, //9
            {1,1,1,1,1,1,2,1,1,1,1,1,0,1,1,0,1,1,1,1,1,2,1,1,1,1,1,1 }, //10
            {0,0,0,0,0,1,2,1,1,1,1,1,0,1,1,0,1,1,1,1,1,2,1,0,0,0,0,0 }, //11
            {0,0,0,0,0,1,2,1,1,0,0,0,0,0,0,0,0,0,0,1,1,2,1,0,0,0,0,0 }, //12
            {0,0,0,0,0,1,2,1,1,0,1,1,1,1,1,1,1,1,0,1,1,2,1,0,0,0,0,0 }, //13
            {1,1,1,1,1,1,2,1,1,0,1,0,0,0,0,0,0,1,0,1,1,2,1,1,1,1,1,1 }, //14
            {0,0,0,0,0,0,2,0,0,0,1,0,1,1,1,1,0,1,0,0,0,2,0,0,0,0,0,0 }, //15
            {1,1,1,1,1,1,2,1,1,0,1,0,0,0,0,0,0,1,0,1,1,2,1,1,1,1,1,1 }, //16
            {0,0,0,0,0,1,2,1,1,0,1,1,1,1,1,1,1,1,0,1,1,2,1,0,0,0,0,0 }, //17
            {0,0,0,0,0,1,2,1,1,0,0,0,0,0,0,0,0,0,0,1,1,2,1,0,0,0,0,0 }, //18
            {0,0,0,0,0,1,2,1,1,0,1,1,1,1,1,1,1,1,0,1,1,2,1,0,0,0,0,0 }, //19
            {1,1,1,1,1,1,2,1,1,0,1,1,1,1,1,1,1,1,0,1,1,2,1,1,1,1,1,1 }, //20
            {1,2,2,2,2,2,2,2,2,2,2,2,2,1,1,2,2,2,2,2,2,2,2,2,2,2,2,1 }, //21
            {1,2,1,1,1,1,2,1,1,1,1,1,2,1,1,2,1,1,1,1,1,2,1,1,1,1,2,1 }, //22
            {1,2,1,1,1,1,2,1,1,1,1,1,2,1,1,2,1,1,1,1,1,2,1,1,1,1,2,1 }, //23
            {1,3,2,2,1,1,2,2,2,2,2,2,2,0,0,2,2,2,2,2,2,2,1,1,2,2,3,1 }, //24
            {1,1,1,2,1,1,2,1,1,2,1,1,1,1,1,1,1,1,2,1,1,2,1,1,2,1,1,1 }, //25
            {1,1,1,2,1,1,2,1,1,2,1,1,1,1,1,1,1,1,2,1,1,2,1,1,2,1,1,1 }, //26
            {1,2,2,2,2,2,2,1,1,2,2,2,2,1,1,2,2,2,2,1,1,2,2,2,2,2,2,1 }, //27
            {1,2,1,1,1,1,1,1,1,1,1,1,2,1,1,2,1,1,1,1,1,1,1,1,1,1,2,1 }, //28
            {1,2,1,1,1,1,1,1,1,1,1,1,2,1,1,2,1,1,1,1,1,1,1,1,1,1,2,1 }, //29
            {1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1 }, //30
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 }, //31
        };

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Pen wallPen = new Pen(Brushes.DeepSkyBlue);

            Pen dotPen = new Pen(Brushes.Pink);

            SolidBrush dotBrush = new SolidBrush(Color.Pink);

            string score = currentScore.ToString();
            char[] chScore = score.ToCharArray(); ;

            for (int i = 0; i < chScore.Length; i++)
                e.Graphics.DrawImage(nums[(int)char.GetNumericValue(chScore[i])], 295 + 12 * i, 20);

            score = highScore.ToString();
            chScore = score.ToCharArray();

            for (int i = 0; i < chScore.Length; i++)
                e.Graphics.DrawImage(nums[(int)char.GetNumericValue(chScore[i])], 195 + 12 * i, 20);

            for (int i = 0; i <= 30; i++)
                for (int j = 0; j <= 27; j++)
                {
                    //if (digitMap[i, j] == 1)
                    //e.Graphics.DrawRectangle(wallPen, objectMap[i, j].x, objectMap[i, j].y, 15, 15); // wall hitboxes

                    if (digitMap[i, j] == 2)
                    {
                        e.Graphics.FillRectangle(dotBrush, objectMap[i, j].x + 7, objectMap[i, j].y + 7, 4, 4); // dots
                        //e.Graphics.DrawRectangle(dotPen, objectMap[i, j].x + 4, objectMap[i, j].y + 4, 8, 8);
                    }

                    if (digitMap[i, j] == 3)
                        e.Graphics.FillEllipse(dotBrush, objectMap[i, j].x + 2, objectMap[i, j].y + 2, 12, 12); // energizer                      
                }


            e.Graphics.DrawImage(pacman.Sprite, pacman.x - 5, pacman.y - 5); // pacman
            //e.Graphics.FillRectangle(dotBrush, pacman.x, pacman.y, 16, 16); // pacman hitbox 

            e.Graphics.DrawImage(ghosts[0].Sprite, ghosts[0].x - 5, ghosts[0].y - 5); // pink 
            //e.Graphics.FillRectangle(dotBrush, ghost[0].x, ghost[0].y, 16, 16);

            e.Graphics.DrawImage(ghosts[1].Sprite, ghosts[1].x - 5, ghosts[1].y - 5); // red

            e.Graphics.DrawImage(ghosts[2].Sprite, ghosts[2].x - 5, ghosts[2].y - 5); // blue
            //e.Graphics.FillRectangle(dotBrush, ghost[2].x, ghost[2].y, 16, 16);

            e.Graphics.DrawImage(ghosts[3].Sprite, ghosts[3].x - 5, ghosts[3].y - 5); // orange
            //e.Graphics.FillRectangle(dotBrush, ghost[3].x, ghost[3].y, 16, 16);


            for (int i = 0; i < pacman.lives; i++)
                e.Graphics.DrawImage(Properties.Resources.LivesMeter, 24 + 24 * i, 548);

            if (showMain == true)
            {
                e.Graphics.DrawImage(mainMenu, 0, 0);

                score = highScore.ToString();
                chScore = score.ToCharArray();
                if (pacman.lives >= 0)
                {
                    for (int i = 0; i < chScore.Length; i++)
                        e.Graphics.DrawImage(nums[(int)char.GetNumericValue(chScore[i])], 195 + 12 * i, 20);
                }

                if (pacman.lives < 0)
                {
                    score = currentScore.ToString();
                    chScore = score.ToCharArray(); ;

                    for (int i = 0; i < chScore.Length; i++)
                        e.Graphics.DrawImage(nums[(int)char.GetNumericValue(chScore[i])], 205 + 12 * i, 240);

                    score = highScore.ToString();
                    chScore = score.ToCharArray();

                    for (int i = 0; i < chScore.Length; i++)
                        e.Graphics.DrawImage(nums[(int)char.GetNumericValue(chScore[i])], 205 + 12 * i, 360);

                }
            }
            if (showHelp == true)
                e.Graphics.DrawImage(helpMenu, 0, 0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.FixedSingle; // запретить изменять размер окна
            MaximizeBox = false; // убрать кнопку "Во весь экран"
            MinimizeBox = false; // убрать кнопку "Свернуть окно"
        }

        private void PowerUp(object sender, EventArgs e)
        {
            pacman.powered = false;
        }

        private void DeathPenalty(object sender, EventArgs e)
        {
            int target = -1;

            for (int i = 0; i <= 3; i++)
                if (ghosts[i].IsDead == true)
                    target = i;

            if (target >= 0)
                ghosts[target].Respawn();
            else
                deathPenalty.Stop();
        }

        private void StartMenu(object sender, EventArgs e)
        {
            if (Keyboard.IsKeyDown(Keys.Space))
            {
                if (showHelp == false)
                {
                    showMain = false;
                    startMenu.Stop();
                    ghostMove.Start();
                    pacmanMove.Start();
                    pacmanAnimation.Start();
                    ghostAnimation.Start();

                    mainMenu = new Bitmap(Properties.Resources.PauseMenu, 448, 576);

                    this.Invalidate();
                }
            }
            if (Keyboard.IsKeyDown(Keys.Tab))
            {
                showHelp = true;
                showMain = false;
            }

            if (Keyboard.IsKeyDown(Keys.Escape))
            {
                if (showHelp == true)
                {
                    showMain = true;
                    showHelp = false;
                }
            }

            this.Invalidate();

        }

        private void StartDeathMenu(object sender, EventArgs e)
        {
            if (Keyboard.IsKeyDown(Keys.Space))
            {

                mainMenu = new Bitmap(Properties.Resources.MainMenu, 448, 576);

                pacman.lives = 3;

                currentScore = 0;

                ResetGame();

                startDeathMenu.Stop();

                startMenu.Start();

                this.Invalidate();
            }

            if (Keyboard.IsKeyDown(Keys.Escape))
                Application.Exit();

            this.Invalidate();

        }

        private void GhostTick(object sender, EventArgs e)
        {
            for (int i = 0; i <= 3; i++)
                GhostMove(ghosts[i]);
        }

        private void PacmanTick(object sender, EventArgs e)
        {

            if (Keyboard.IsKeyDown(Keys.W))
            {
                preMove = 1;
            }
            if (Keyboard.IsKeyDown(Keys.S))
            {
                preMove = 2;
            }
            if (Keyboard.IsKeyDown(Keys.A))
            {
                preMove = 3;
            }
            if (Keyboard.IsKeyDown(Keys.D))
            {
                preMove = 4;
            }

            if (Keyboard.IsKeyDown(Keys.Escape))
            {
                ghostMove.Stop();
                pacmanMove.Stop();
                showMain = true;
                startMenu.Start();
                this.Invalidate();
            }

            switch (preMove)
            {
                case 0:
                    if (MapCollision(pacman))
                        pacman.Move();
                    break;

                case 1:
                    MoveGen(0, -1);
                    break;

                case 2:
                    MoveGen(0, 1);
                    break;

                case 3:
                    MoveGen(-1, 0);
                    break;

                case 4:
                    MoveGen(1, 0);
                    break;
            }

            PacmanEat();

            /*Check for win*/
            if (dotsNumber == 0)
                ResetGame();

            this.Invalidate(); // нужна для перерисовки
        }

        public void PacmanEat()
        {
            /*Check collision for dots*/
            for (int i = 0; i <= 30; i++)
                for (int j = 0; j <= 27; j++)
                {
                    if (digitMap[i, j] == 2)
                    {
                        if (pacman.CheckCollision(objectMap[i, j].x + 4, objectMap[i, j].y + 4, 8) == true)
                        {
                            digitMap[i, j] = 4;
                            objectMap[i, j] = new Tile(j, i);
                            dotsNumber--;
                            currentScore += 10;
                        }
                    }

                    if (digitMap[i, j] == 3)
                        if (pacman.CheckCollision(objectMap[i, j].x + 4, objectMap[i, j].y + 4, 8) == true)
                        {
                            digitMap[i, j] = 5;
                            objectMap[i, j] = new Tile(j, i);
                            currentScore += 50;

                            powerUp.Stop();

                            pacman.powered = true;

                            powerUp.Start();
                        }
                }


            /*Check collision for Ghosts*/
            for (int i = 0; i <= 3; i++)
            {
                if (pacman.CheckCollision(ghosts[i].x, ghosts[i].y, 15) == true)
                    if (pacman.powered == false)
                        Reset();
                    else
                        Reset(ghosts[i]);
            }
        }

        public void Reset()
        {
            System.Threading.Thread.Sleep(1000);
            for (int i = 0; i <= 3; i++)
                ghosts[i].Respawn();
            pacman.Die();
            if (pacman.lives < 0)
            {
                if (currentScore > highScore)
                {
                    Properties.Settings.Default.HighScore = currentScore;
                    highScore = currentScore;
                    Properties.Settings.Default.Save();
                }
                ghostMove.Stop();
                pacmanMove.Stop();
                mainMenu = new Bitmap(Properties.Resources.DeathMenu, 448, 576);
                startDeathMenu.Start();
                showMain = true;
                this.Invalidate();
            }
        }

        public void Reset(Ghost entity)
        {
            System.Threading.Thread.Sleep(100);
            currentScore += 200;
            entity.x = 216;
            entity.y = 256;
            entity.IsDead = true;
            deathPenalty.Start();
        }

        public void GhostMove(PointMove entity)
        {
            freeMove = true;

            if (MapCollision(entity) && !(MapCollision(entity, entity.yDir, entity.xDir) || MapCollision(entity, -entity.yDir, -entity.xDir)))
                entity.Move();
            else
            {
                freeMove = true;

                if (Randomize() == 1)
                    if (entity.xDir == 0)
                    {
                        entity.ChangeDirection(1, 0);

                        if (entity.x > pacman.x)
                            entity.ChangeDirection(-1, 0);

                        if (pacman.powered == true)
                            entity.ChangeDirection(-entity.xDir, 0);

                        if (!MapCollision(entity))
                            entity.ChangeDirection(entity.xDir * -1, 0);
                        entity.Move();

                    }
                    else
                    {
                        entity.ChangeDirection(0, 1);

                        if (entity.y > pacman.y)
                            entity.ChangeDirection(0, -1);

                        if (pacman.powered == true)
                            entity.ChangeDirection(0, -entity.yDir);

                        if (!MapCollision(entity))
                            entity.ChangeDirection(0, entity.yDir * -1);

                        entity.Move();
                    }

                else if (MapCollision(entity))
                    entity.Move();
            }

        }

        public void MoveGen(int dx, int dy) // check the collisions and decide whether to change direction of movement or not
        {
            if (!MapCollision(pacman, dx, dy))
                freeMove = false;

            if (freeMove == true)
            {
                pacman.ChangeDirection(dx, dy);
                pacman.Move();
                preMove = 0;
            }
            else if (MapCollision(pacman))
                pacman.Move();
        }

        public bool MapCollision(PointMove entity) // check if there are any collisions for the current direction
        {
            for (int i = 0; i <= 30; i++)
                for (int j = 0; j <= 27; j++)
                    if (digitMap[i, j] == 1)
                        if (entity.CheckCollision(objectMap[i, j].x, objectMap[i, j].y, 16) == true)
                            return false;
            return true;
        }

        public bool MapCollision(PointMove entity, int dx, int dy)// check if there are any collisions for the chosen direction
        {
            for (int i = 0; i <= 30; i++)
                for (int j = 0; j <= 27; j++)
                    if (digitMap[i, j] == 1)
                        if (entity.CheckCollision(objectMap[i, j].x, objectMap[i, j].y, dx, dy) == true)
                            return false;
            return true;
        }

        public static class Keyboard
        {
            private static readonly HashSet<Keys> keys = new HashSet<Keys>();

            public static void OnKeyDown(object sender, KeyEventArgs e)
            {
                if (keys.Contains(e.KeyCode) == false)
                {
                    keys.Add(e.KeyCode);
                }
            }

            public static void OnKeyUp(object sender, KeyEventArgs e)
            {
                if (keys.Contains(e.KeyCode))
                {
                    keys.Remove(e.KeyCode);
                }
            }

            public static bool IsKeyDown(Keys key)
            {
                return keys.Contains(key);
            }
        }

        public int Randomize()
        {
            return rnd.Next(0, 2);
        }

        private void PacmanAnimation(object sender, EventArgs e)
        {
            switch (animationFrame)
            {
                case 0:
                    if (pacman.xDir == 1)
                        pacman.Sprite = new Bitmap(Properties.Resources.pRight1);
                    if (pacman.xDir == -1)
                        pacman.Sprite = new Bitmap(Properties.Resources.pLeft1);
                    if (pacman.yDir == 1)
                        pacman.Sprite = new Bitmap(Properties.Resources.pDown1);
                    if (pacman.yDir == -1)
                        pacman.Sprite = new Bitmap(Properties.Resources.pUp1);

                    animationFrame++;
                    break;

                case 1:
                    if (pacman.xDir == 1)
                        pacman.Sprite = new Bitmap(Properties.Resources.pRight);
                    if (pacman.xDir == -1)
                        pacman.Sprite = new Bitmap(Properties.Resources.pLeft);
                    if (pacman.yDir == 1)
                        pacman.Sprite = new Bitmap(Properties.Resources.pDown);
                    if (pacman.yDir == -1)
                        pacman.Sprite = new Bitmap(Properties.Resources.pUp);

                    animationFrame++;
                    break;

                case 2:
                    if (pacman.xDir == 1)
                        pacman.Sprite = new Bitmap(Properties.Resources.pRight1);
                    if (pacman.xDir == -1)
                        pacman.Sprite = new Bitmap(Properties.Resources.pLeft1);
                    if (pacman.yDir == 1)
                        pacman.Sprite = new Bitmap(Properties.Resources.pDown1);
                    if (pacman.yDir == -1)
                        pacman.Sprite = new Bitmap(Properties.Resources.pUp1);

                    animationFrame++;
                    break;

                case 3:
                    pacman.Sprite = new Bitmap(Properties.Resources.pacman);
                    animationFrame = 0;
                    break;

            }

        }

        private void GhostAnimation(object sender, EventArgs e)
        {
            switch (ghostFrame)
            {
                case 0:

                    if (pacman.powered == true)
                        for (int i = 0; i <= 3; i++)
                            ghosts[i].Sprite = new Bitmap(Properties.Resources.frightened);
                    else
                    {
                        if (ghosts[0].xDir == 1)
                            ghosts[0].Sprite = new Bitmap(Properties.Resources.pinkRight1);
                        if (ghosts[0].xDir == -1)
                            ghosts[0].Sprite = new Bitmap(Properties.Resources.pinkLeft1);
                        if (ghosts[0].yDir == 1)
                            ghosts[0].Sprite = new Bitmap(Properties.Resources.pinkDown1);
                        if (ghosts[0].yDir == -1)
                            ghosts[0].Sprite = new Bitmap(Properties.Resources.pinkUp1);

                        if (ghosts[1].xDir == 1)
                            ghosts[1].Sprite = new Bitmap(Properties.Resources.redRight1);
                        if (ghosts[1].xDir == -1)
                            ghosts[1].Sprite = new Bitmap(Properties.Resources.redLeft1);
                        if (ghosts[1].yDir == 1)
                            ghosts[1].Sprite = new Bitmap(Properties.Resources.redDown1);
                        if (ghosts[1].yDir == -1)
                            ghosts[1].Sprite = new Bitmap(Properties.Resources.redUp1);

                        if (ghosts[2].xDir == 1)
                            ghosts[2].Sprite = new Bitmap(Properties.Resources.blueRight1);
                        if (ghosts[2].xDir == -1)
                            ghosts[2].Sprite = new Bitmap(Properties.Resources.blueLeft1);
                        if (ghosts[2].yDir == 1)
                            ghosts[2].Sprite = new Bitmap(Properties.Resources.blueDown1);
                        if (ghosts[2].yDir == -1)
                            ghosts[2].Sprite = new Bitmap(Properties.Resources.blueUp1);

                        if (ghosts[3].xDir == 1)
                            ghosts[3].Sprite = new Bitmap(Properties.Resources.orangeRight1);
                        if (ghosts[3].xDir == -1)
                            ghosts[3].Sprite = new Bitmap(Properties.Resources.orangeLeft1);
                        if (ghosts[3].yDir == 1)
                            ghosts[3].Sprite = new Bitmap(Properties.Resources.orangeDown1);
                        if (ghosts[3].yDir == -1)
                            ghosts[3].Sprite = new Bitmap(Properties.Resources.orangeUp1);
                    }
                    ghostFrame++;
                    break;

                case 1:

                    if (pacman.powered == true)
                        for (int i = 0; i <= 3; i++)
                            ghosts[i].Sprite = new Bitmap(Properties.Resources.frightened1);
                    else
                    {
                        if (ghosts[0].xDir == 1)
                            ghosts[0].Sprite = new Bitmap(Properties.Resources.pinkRight);
                        if (ghosts[0].xDir == -1)
                            ghosts[0].Sprite = new Bitmap(Properties.Resources.pinkLeft);
                        if (ghosts[0].yDir == 1)
                            ghosts[0].Sprite = new Bitmap(Properties.Resources.pinkDown);
                        if (ghosts[0].yDir == -1)
                            ghosts[0].Sprite = new Bitmap(Properties.Resources.pinkUp);

                        if (ghosts[1].xDir == 1)
                            ghosts[1].Sprite = new Bitmap(Properties.Resources.redRight);
                        if (ghosts[1].xDir == -1)
                            ghosts[1].Sprite = new Bitmap(Properties.Resources.redLeft);
                        if (ghosts[1].yDir == 1)
                            ghosts[1].Sprite = new Bitmap(Properties.Resources.redDown);
                        if (ghosts[1].yDir == -1)
                            ghosts[1].Sprite = new Bitmap(Properties.Resources.redUp);

                        if (ghosts[2].xDir == 1)
                            ghosts[2].Sprite = new Bitmap(Properties.Resources.blueRight);
                        if (ghosts[2].xDir == -1)
                            ghosts[2].Sprite = new Bitmap(Properties.Resources.blueLeft);
                        if (ghosts[2].yDir == 1)
                            ghosts[2].Sprite = new Bitmap(Properties.Resources.blueDown);
                        if (ghosts[2].yDir == -1)
                            ghosts[2].Sprite = new Bitmap(Properties.Resources.blueUp);

                        if (ghosts[3].xDir == 1)
                            ghosts[3].Sprite = new Bitmap(Properties.Resources.orangeRight);
                        if (ghosts[3].xDir == -1)
                            ghosts[3].Sprite = new Bitmap(Properties.Resources.orangeLeft);
                        if (ghosts[3].yDir == 1)
                            ghosts[3].Sprite = new Bitmap(Properties.Resources.orangeDown);
                        if (ghosts[3].yDir == -1)
                            ghosts[3].Sprite = new Bitmap(Properties.Resources.orangeUp);
                    }
                    ghostFrame--;
                    break;
            }
        }

        public void ResetGame()
        {
            for (int i = 0; i < digitMap.GetLength(0); i++)
            {
                for (int j = 0; j < digitMap.GetLength(1); j++)
                {
                    if (digitMap[i, j] == 4)
                    {
                        digitMap[i, j] = 2;
                        objectMap[i, j] = new Dot(j, i + 3);
                        dotsNumber++;
                    }
                    else if (digitMap[i, j] == 5)
                    {
                        digitMap[i, j] = 3;
                        objectMap[i, j] = new Energizer(j, i + 3);
                    }
                }
            }

            foreach (var ghost in ghosts)
            {
                ghost.Respawn();
            }

            pacman.x = 216;
            pacman.y = 416;

            pacman.ChangeDirection(0, 0);
        }

        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PacmanForms));
            this.SuspendLayout();
            // 
            // PacmanForms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = global::Pacman.Properties.Resources.Map;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(448, 575);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PacmanForms";
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pacman";
            this.ResumeLayout(false);

        }

        #endregion
    }
}