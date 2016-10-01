using System;
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
            PosX = 0;
            PosY = 0;
        }

        public void MoveDown()
        {
            foreach (Part p in Parts)
                p.MoveDown();
        }
    }

}
