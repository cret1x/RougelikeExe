using Pastel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougelikeExe
{
    class PixelArt
    {
        public static Bitmap CreatePixelArt(string path)
        {
            Bitmap image = new Bitmap(path);
            Bitmap bitmap = new Bitmap(Console.WindowWidth, Console.BufferHeight);
            int step = image.Width / Console.WindowWidth;
            if (step == 0)
            {
                step = 1;
            }
            int w__ptr = 0;
            int h_ptr = 0;
            for (int i = 0; i < image.Height; i += step * 2)
            {
                for (int j = 0; j < image.Width; j += step * 2)
                {

                    bitmap.SetPixel(w__ptr, h_ptr, image.GetPixel(j, i));
                    w__ptr++;
                }
                h_ptr++;
                w__ptr = 0;
                Console.WriteLine();
            }
            h_ptr = 0;
            return bitmap;
        }
    }
}
