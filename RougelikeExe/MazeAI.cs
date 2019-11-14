using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougelikeExe
{
    class MazeAI : Player
    {
        private int x;
        private int y;
        public MazeAI(int posX, int posY) : base(posX, posY)
        {
            x = posX;
            y = posY;
        }
    }
}
