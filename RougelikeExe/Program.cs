using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace RougelikeExe
{
    class Program
    {
        static readonly int WIDTH = 21;
        static readonly int HEIGTH = 21;

        static void Main(string[] args)
        {
            Map map = new Map(WIDTH, HEIGTH);
            Player player = new Player(1, HEIGTH-2);
            Game game = new Game(map, player, WIDTH, HEIGTH);
            game.showPreview();
            Thread.Sleep(2000);
            game.ClearScreen();
            game.NextStage();
            game.DrawFrame();

            ConsoleKeyInfo key = Console.ReadKey();
            while (key.Key != ConsoleKey.Escape)
            {
                game.ClearScreen();
                game.InputHandler(key.Key);
                game.DrawFrame();
                key = Console.ReadKey();
            }
        }
    }
}
