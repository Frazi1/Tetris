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

        public Block parentBlock { get; set; }

        public int PosX
        {
            get
            {
                return posX + parentBlock.PosX;
            }

        }
        public int PosY
        {
            get
            {
                return posY + parentBlock.PosY;
            }
        }

        public Part(Block parentBlock, int posx, int posy)
        {
            this.parentBlock = parentBlock;
            posX = posx;
            posY = posy;
        }

        public void MoveDown()
        {
            ++posY;
        }
        public void MoveUp()
        {
            --posY;
        }
    }
}