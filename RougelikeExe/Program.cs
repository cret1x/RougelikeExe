using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace RougelikeExe
{
    class Program
    {
        static readonly int WIDTH = 21;
        static readonly int HEIGHT = 21;

        static void Main(string[] args)
        {
            //Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            Game game = new Game(WIDTH, HEIGHT);
            game.ShowPreview(Const.RES_PREVIEW);
            Thread.Sleep(2000);
            game.ClearScreen();
            game.NextStage();
            game.DrawFrame();

            ConsoleKeyInfo key = Console.ReadKey();
            while (key.Key != ConsoleKey.Escape)
            {
                game.ClearScreen();
                if(key.Key == ConsoleKey.G) game.Dubug();
                game.InputHandler(key.Key);
                game.DrawFrame();
                key = Console.ReadKey();
            }
        }
    }
}
