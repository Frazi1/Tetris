using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Threading;

namespace Tetris
{
    public class Missile : Block
    {
        public int Speed { get; set; }
        public Color Color;

        public Missile(int startingRow,int startingColumn)
        {
            PosX = startingColumn;
            PosY = startingRow - 1;
            Parts = new List<Part>() { new Part(this, 0, 0) };
            Speed = 400;
            Color = Colors.Red;
        }

        public void MoveUp()
        {
            --PosY;
        }



    }
}
