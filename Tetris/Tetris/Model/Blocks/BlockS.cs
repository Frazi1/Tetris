using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
    public class BlockS: Block
    {
        public BlockS()
        {
            Parts = new List<Part>() { new Part(this, 0, 0), new Part(this, 0, 1), new Part(this, 0, 2), new Part(this, 0, 3), new Part(this, 1, 3) };
        }
    }
}
