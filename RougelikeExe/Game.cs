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

        public Game(int width, int height)
        {
            HEIGHT = height;
            WIDTH = width;
            map = new Map(WIDTH, HEIGHT);
            player = new Player(1, HEIGHT - 2);
            mapNumber = 0;
        }

        public void ShowPreview(string path)
        {
            ClearScreen();
            Console.Beep();
            Bitmap prev = PixelArt.CreatePixelArt(path);
            int preview_height = Console.WindowHeight;
            int preview_width = Console.WindowWidth;
            for (int i = 0; i < preview_height; i++)
            {
                for (int j = 0; j < preview_width/2; j++)
                {
                    Color color = prev.GetPixel(j, i);
                    Console.Write("  ".PastelBg(color));
                }
            }
        }

        public void DrawFrame()
        {
            List<string> lbuffer = new List<string>(HEIGHT+2);
            map.AddEntity(player.GetX(), player.GetY(), Const.PLAYER);
            lbuffer.AddRange(map.PrepareBuffer());
            lbuffer.Add(("Coins: " + player.GetScore().ToString()).Pastel(Color.Gold));
            lbuffer.Add(("Map: " + mapNumber.ToString()).Pastel(Color.White));
            ScreenRenderer.PrintBuffer(lbuffer.ToArray());
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
                ShowPreview(Const.RES_COIN);
                Thread.Sleep(1000);
                ClearScreen();
                player.SetScore(player.GetScore() + 10);
                NextStage();
            }
        }

        public void NextStage()
        {
            
            map.Init();
            mapNumber++;
        }

        public void Dubug()
        {
            Console.Clear();
            map.Debug();
        }
    }
}
