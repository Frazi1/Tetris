using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Tetris
{
    public class Game
    {
        const int TIMER_INTERVAL = 200;

        private int ROWS;
        private int COLUMNS;

        public List<Block> Blocks { get; set; }
        public Player Player { get; set; }
        public List<Missile> Missiles { get; set; }

        public int Score
        {
            get
            {
                return score;
            }

            set
            {
                score = value;
                ScoreUpdated();
            }
        }
        private int score = 0;

        public int Difficulty { get; set; }
        private double blockSpawnCoef = 0.1;

        private int counter = 0;
        private DispatcherTimer GameTimer;


        public Game(int Rows, int Cols)
        {
            ROWS = Rows;
            COLUMNS = Cols;
            Difficulty = 3;

            Missiles = new List<Missile>();
            Blocks = new List<Block>();

            Player = new Player(ROWS, COLUMNS);
            GameTimer = new DispatcherTimer();
            GameTimer.Interval = TimeSpan.FromMilliseconds(TIMER_INTERVAL);
            GameTimer.Tick += new EventHandler(TetrisTick);
        }

        public void Start()
        {
            NewBlock();
            GameTimer.Start();
            //_Drawer.PaintTetris();
            //BlockMoved();
        }

        public void NewBlock()
        {
            Block newBlock = null;
            Random rnd = new Random();
            int type = rnd.Next(0, 5);
            switch (type)
            {
                case 0:
                    newBlock = new BlockI();
                    break;
                case 1:
                    newBlock = new BlockL();
                    break;
                case 2:
                    newBlock = new BlockO();
                    break;
                case 3:
                    newBlock = new BlockS();
                    break;
                case 4:
                    newBlock = new BlockT();
                    break;
            }
            newBlock.PosX = rnd.Next(0, COLUMNS);
            bool err = true;
            while (err)
            {
                foreach (Part p in newBlock.Parts)
                    while (p.PosX >= COLUMNS)
                    {
                        err = true;
                        --newBlock.PosX;
                        break;
                    }
                err = false;
            }

            Blocks.Add(newBlock);
            BlockSpawned();
        }
        public void SpawnBlocks(int number)
        {
            double coef = blockSpawnCoef;
            Random rnd = new Random();
            for (int i = 0; i < number; i++)
            {
                if (rnd.NextDouble() <= coef)
                {
                    NewBlock();
                    coef /= 2;
                }
                else
                    coef *= 2;

            }
        }
        public void DestroyBlock(Block b)
        {
            Blocks.Remove(b);
            BlockDestroyed();
        }
        public void DestroyPart(ref Part p)
        {
            p.parentBlock.Parts.Remove(p);
            p = null;
            BlockDestroyed();
            Score++;
        }
        public void DestroyMissile(Missile m)
        {
            Missiles.Remove(m);
            BlockDestroyed();
        }

        public bool isBlockDown(Block b)
        {
            foreach (Part p in b.Parts)
                if (p.PosY == ROWS - 1)
                {
                    GameOver();
                    GameTimer.Stop();

                    return true;
                }
            return false;
        }
        public bool isMissileOut(Missile m)
        {
            if (m.PosY == 0)
                return true;
            return false;
        }

        public void MoveDown()
        {
            if (Blocks.Count == 0)
                return;
            else
            {
                for (int i = 0; i < Blocks.Count; i++)
                {
                    if (isBlockDown(Blocks[i]))
                        DestroyBlock(Blocks[i]);
                    else
                    {
                        CheckCollision();
                        Blocks[i].MoveDown();
                        CheckCollision();
                    }
                }
            }
            BlockMoved(); //
        }
        public void MoveMissiles()
        {
            CheckCollision();
            for (int i = 0; i < Missiles.Count; i++)
            {
                if (isMissileOut(Missiles[i]))
                    DestroyMissile(Missiles[i]);
                else
                {
                    Missiles[i].MoveUp();
                }
            }

            BlockMoved();
        }
        public void ShootMissile()
        {
            Missile m = Player.ShootMissile();
            Missiles.Add(m);
            PlayerShoot();
        }


        public void CheckCollision()
        {
            for (int i = 0; i < Missiles.Count; i++)
            {
                Missile m = Missiles[i];
                for (int j = 0; j < Blocks.Count; j++)
                {
                    Block b = Blocks[j];
                    for (int k = 0; k < b.Parts.Count; k++)
                    {
                        Part p = b.Parts[k];
                        if (p.PosX == m.PosX && p.PosY == m.PosY)
                        {
                            DestroyPart(ref p);
                            DestroyMissile(m);
                            MissileCollided();
                        }
                    }
                }


            }
        }


        private void TetrisTick(object sender, EventArgs e)
        {
            ++counter;
            if (Missiles.Count != 0)
                MoveMissiles();
            if (counter == 3)
            {
                MoveDown();
                SpawnBlocks(Difficulty);
                counter = 0;
            }

        }



        //delegates
        public delegate void BlockMovedEventHandler();
        public delegate void BlockDestroyedEventHandler();
        public delegate void MissileMovedEventHandler();
        public delegate void PlayerShootEventHandler();
        public delegate bool MissileCollidedEventHandler();
        public delegate void ScoreUpdatedEventHandler();
        public delegate void BlockSpawnedEventHandler();
        public delegate void GameOverEventHandler();


        //events
        public event BlockMovedEventHandler BlockMoved;
        public event BlockDestroyedEventHandler BlockDestroyed;
        public event PlayerShootEventHandler PlayerShoot;
        public event MissileCollidedEventHandler MissileCollided;
        public event ScoreUpdatedEventHandler ScoreUpdated;
        public event BlockSpawnedEventHandler BlockSpawned;
        public event GameOverEventHandler GameOver;

    }


}

