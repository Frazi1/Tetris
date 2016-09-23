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
        const int ROWS = 18;
        const int COLUMNS = 10;

        public List<Block> TetrisGrid { get; set; }
        public List<Block> Blocks { get; set; }
        public Player Player { get; set; }
        public List<Missile> Missiles { get; set; }


        public Drawer _Drawer { get; set; }

        private int counter = 0;
        private DispatcherTimer GameTimer;


        public Game(int Rows, int Cols, Label[,] labels)
        {
            TetrisGrid = new List<Block>();
            Missiles = new List<Missile>();
            Blocks = new List<Block>();

            _Drawer = new Drawer(this, labels);

            Player = new Player(Rows + 1);
            GameTimer = new DispatcherTimer();
            GameTimer.Interval = TimeSpan.FromMilliseconds(250);
            GameTimer.Tick += new EventHandler(TetrisTick);

        }

        public void Start()
        {
            NewBlock();
            GameTimer.Start();
        }

        public void NewBlock()
        {
            Block newBlock = new BlockL();
            TetrisGrid.Add(newBlock);
            Blocks.Add(newBlock);
        }
        public void DestroyBlock(Block b)
        {
            TetrisGrid.Remove(b);
        }
        public void DestroyPart(ref Part p)
        {
            p.parentBlock.Parts.Remove(p);
            p = null;
        }
        public void DestroyMissile(Missile m)
        {
            Missiles.Remove(m);
        }
        public bool isBlockDown(Block b)
        {
            foreach (Part p in b.Parts)
                if (p.PosY == ROWS - 2)
                    return true;
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
                        Blocks[i].MoveDown();
                        Missile m;
                        Part p;
                        isMissileCollided(out m,out p);
                    }
                }
            }
        }

        private void MoveMissiles()
        {
            for (int i = 0; i < Missiles.Count; i++)
            {
                if (isMissileOut(Missiles[i]))
                    DestroyMissile(Missiles[i]);
                else
                    Missiles[i].MoveUp();
            }
            Missile m;
            Part p;
            if (isMissileCollided(out m, out p))
            {
                DestroyPart(ref p);
                DestroyMissile(m);
                return;
            }
        }
        public void ShootMissile()
        {
            Missile m = Player.ShootMissile();
            Missiles.Add(m);
        }
        public bool isMissileCollided(out Missile collidedMissile, out Part collidedPart)
        {
            collidedPart = null;
            collidedMissile = null;

            for (int i = 0; i < Missiles.Count; i++)
            {
                Missile currMissile = Missiles[i];
                for (int j = 0; j < TetrisGrid.Count; j++)
                {
                    Block currBlock = TetrisGrid[j];
                    for (int k = 0; k < currBlock.Parts.Count; k++)
                    {
                        if (currMissile.PosX == currBlock.Parts[k].PosX &&
                            currMissile.PosY == currBlock.Parts[k].PosY)
                        {
                            collidedPart = currBlock.Parts[k];
                            collidedMissile = currMissile;
                            return true;
                        }
                    }
                }
            }

            return false;
        }


        private void TetrisTick(object sender, EventArgs e)
        {
            ++counter;
            if (Missiles.Count != 0)
                MoveMissiles();
            if (counter == 3)
            {
                MoveDown();
                counter = 0;
            }
            _Drawer.PaintTetris();
        }

        //delegates

        //events

    }


}

