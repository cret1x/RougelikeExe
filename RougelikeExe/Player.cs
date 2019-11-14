using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougelikeExe
{
    class Player
    {
        private int x;
        private int y;
        private int score;


        public Player(int posX, int posY)
        {
            x = posX;
            y = posY;
            score = 0;
        }

        public void Up()
        {
            y--;
        }
        public void Down()
        {
            y++;
        }
        public void Left()
        {
            x--;
        }
        public void Right()
        {
            x++;
        }
        public int GetX()
        {
            return x;
        }
        public int GetY()
        {
            return y;
        }
        public int GetScore()
        {
            return score;
        }
        public void SetScore(int val)
        {
            score = val;
        }
    }
}
