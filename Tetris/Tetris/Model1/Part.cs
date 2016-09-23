using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
    public class Part
    {
        private int posX;
        private int posY;
        private Block parentBlock;

        public int PosX
        {
            get
            {
                return posX + parentBlock.PosX;
            }

            set
            {
                posX = value;
            }
        }

        public int PosY
        {
            get
            {
                return posY + parentBlock.PosY;
            }

            set
            {
                posY = value;
            }
        }

        public Part(Block parentBlock, int posx, int posy)
        {
            this.parentBlock = parentBlock;
            posX = posx;
            posY = posy;

        }
    }
}