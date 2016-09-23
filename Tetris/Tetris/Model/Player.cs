using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Tetris
{
    public class Player: Block
    {
        public Color Color { get; set; }
        public int MissileSpeed { get; set; }

        public Player(int playerRow)
        {
            PosX = 5;
            PosY = playerRow;
            Color = Colors.Blue;
            Parts = new List<Part>() {new Part(this, 0, 0)};
        }

        public void MoveLeft()
        {
            if (PosX > 0)
            {
                --PosX;
            }
        }
        public void MoveRight()
        {
            if (PosX < 9)
            {
                ++PosX;
            }
        }
        public Missile ShootMissile()
        {
            Missile Missile = new Missile(this.PosY - 1, this.PosX);
            return Missile;
        }

    }
}
