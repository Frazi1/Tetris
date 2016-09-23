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

        public int[] GetStartingCoords()
        {
            int[] coords = { 0, 0 };
            Random rnd = new Random();
            int x = rnd.Next(0, 10);
            coords[0] = x;
            return coords;
        }

        public void MoveDown()
        {
            PosX++;
        }
    }
}
