using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Tetris
{
    public class Player: Block
    {
        //public int posX { get; set; }
        //public int posY { get; }
        public Color Color { get; set; }
        public int MissileSpeed { get; set; }

        public Player(int playerRow)
        {
            PosX = 5;
            PosY = playerRow;
            Color = Colors.Blue;
            Parts = new List<Part>() {new Part(this, 0, playerRow)};
        }

        public void MoveLeft()
        {
            if (PosX > 0)
            {
                PlayerMoving();
                --PosX;
                PlayerMoved();
            }
        }
        public void MoveRight()
        {
            if (PosX < 9)
            {
                PlayerMoving();
                ++PosX;
                PlayerMoved();
            }
        }
        public Missile ShootMissile()
        {
            Missile Missile = new Missile(this.PosY - 1, this.PosX);
            //PlayerShoot();
            return Missile;
        }

        //delegates
        public delegate void PlayerMovedEventHandler();
        public delegate void PlayerMovingEventHandler();
        public delegate Block PlayerShootEventHandler();

        //events
        public event PlayerMovedEventHandler PlayerMoved;
        public event PlayerMovingEventHandler PlayerMoving;

    }
}
