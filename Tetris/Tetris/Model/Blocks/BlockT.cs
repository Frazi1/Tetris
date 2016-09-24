using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
    public class BlockT: Block
    {
        public BlockT()
        {
            Parts = new List<Part>() { new Part(this, 0, 0), new Part(this, 1, 0), new Part(this, 1, 1), new Part(this, 2,0) };
        }
    }
}
