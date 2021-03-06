﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Tetris
{
    public abstract class Block
    {
        public List<Part> Parts { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }

        public Block()
        {
            PosX = 3;
            PosY = 0;
        }


        public int GetStartingCoords()
        {
            Random rnd = new Random();
            return rnd.Next(0, 10);
        }

        public void MoveDown()
        {
            foreach (Part p in Parts)
                p.MoveDown();
        }
    }

}
