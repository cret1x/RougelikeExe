using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougelikeExe
{
    class ScreenRenderer
    {
        /*
        private readonly int W = Console.WindowWidth;
        private readonly int H = Console.WindowHeight;
        */
        public static void PrintBuffer(string[] buff)
        {
            foreach (var line in buff)
            {
                Console.WriteLine(line);
            }
        }
    }
}
