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
        public void NextStep()
        {
            Random random = new Random();
            switch (random.Next(4))
            {
                case 0:
                    Up();
                    break;
                case 1:
                    Down();
                    break;
                case 2:
                    Right();
                    break;
                case 3:
                    Left();
                    break;
                default:
                    break;
            }
        }
    }
}
