using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Tetris
{
    public class Player: Block
    {
        private int Rows;
        private int Cols;

        public Color Color { get; set; }

        public Player(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;

            PosX = cols / 2;
            PosY = rows + 1;
            Color = Colors.Blue;
            Parts = new List<Part>() { new Part(this, 0, 0) };
        }

        public void MoveLeft()
        {
            if (PosX > 0)
            {
                --PosX;
                PlayerMoved();
            }
        }
        public void MoveRight()
        {
            if (PosX < Cols - 1)
            {
                ++PosX;
                PlayerMoved();
            }
        }
        public Missile ShootMissile()
        {
            Missile Missile = new Missile(this.PosY - 1, this.PosX);
            return Missile;
        }

        //delegates
        public delegate void PlayerMovedEventHandler();

        //events
        public event PlayerMovedEventHandler PlayerMoved;

    }
}
