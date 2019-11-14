using Pastel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RougelikeExe
{
    class Game
    {
        private Map map;
        private Player player;
        private int mapNumber;
        private readonly int HEIGHT;
        private readonly int WIDTH;

        public Game(Map map_, Player player_, int width, int height)
        {
            map = map_;
            player = player_;
            mapNumber = 0;
            HEIGHT = height;
            WIDTH = width;
        }

        public void showPreview()
        {
            Console.Beep();
            int preview_height = Console.WindowHeight;
            int preview_width = Console.WindowWidth;
            for (int i = 0; i < preview_height; i++)
            {
                for (int j = 0; j < preview_width/2; j++)
                {
                    if (PixelArt.pixelMap[i, j] == 0) Console.Write("  ".PastelBg(Color.White));
                    else if (PixelArt.pixelMap[i, j] == 1) Console.Write("  ".PastelBg(Color.Red));
                    else if (PixelArt.pixelMap[i, j] == 2) Console.Write("  ".PastelBg(Color.Goldenrod));
                    else if (PixelArt.pixelMap[i, j] == 3) Console.Write("  ".PastelBg(Color.DarkRed));
                }
            }
        }

        public void DrawFrame()
        {
            string buffer;
            map.AddEntity(player.GetX(), player.GetY(), Const.PLAYER);
            map.Print();
            buffer = "Score: " + player.GetScore().ToString();
            Console.WriteLine(buffer.Pastel(Color.White));
            buffer = "Map: " + mapNumber.ToString();
            Console.WriteLine(buffer.Pastel(Color.White));
        }

        public void ClearScreen()
        {
            Console.Clear();
            map.RemoveEntity(player.GetX(), player.GetY());
        }

        public void InputHandler(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    player.Up();
                    if (player.GetY() < 0)
                    {
                        player.Down();
                    }
                    if (map.IsWall(player.GetX(), player.GetY()))
                        player.Down();
                    break;
                case ConsoleKey.DownArrow:
                    player.Down();
                    if (player.GetY() > HEIGHT - 1)
                    {
                        player.Up();
                    }
                    if (map.IsWall(player.GetX(), player.GetY()))
                        player.Up();
                    break;
                case ConsoleKey.LeftArrow:
                    player.Left();
                    if (player.GetX() < 0)
                    {
                        player.Right();
                    }
                    if (map.IsWall(player.GetX(), player.GetY()))
                        player.Right();
                    break;
                case ConsoleKey.RightArrow:
                    player.Right();
                    if (player.GetX() > WIDTH - 1)
                    {
                        player.Left();
                    }
                    if (map.IsWall(player.GetX(), player.GetY()))
                        player.Left();
                    break;
                case ConsoleKey.R:
                    map.Init();
                    break;
                default:
                    break;
            }
            if (map.IsCoin(player.GetX(), player.GetY()))
            {
                player.SetScore(player.GetScore() + 10 * mapNumber);
                NextStage();
            }
        }

        public void NextStage()
        {
            map.Init();
            mapNumber++;
        }
    }
}
