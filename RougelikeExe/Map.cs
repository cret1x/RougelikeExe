using Pastel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RougelikeExe
{
    class Map
    {
        private char[][] matrix;
        private readonly int width;
        private readonly int height;
        private bool hasCoin = false;
        private readonly bool GUI = true;

        public Map(int lenX, int lenY)
        {
            height = lenY;
            width = lenX;
            matrix = new char[lenY][];
            for (int i = 0; i < lenY; i++)
            {
                matrix[i] = new char[lenX];
                for (int j = 0; j < lenX; j++)
                {
                    matrix[i][j] = Const.WALL;
                }
            }
        }

        public void Init()
        {
            //ueses RandMapGen.exe to generate maze in file currentMaze.txt
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo
            {
                WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                FileName = "cmd.exe",
                Arguments = "/C " + Const.MAZEGENEXE + " " + width + " " + height
            };
            process.StartInfo = startInfo;
            process.Start();
            Thread.Sleep(500);
            //reading currentMaze.txt
            string textFromFile;
            using (FileStream fileStream = File.OpenRead(Const.PATHTXT))
            {
                byte[] array = new byte[fileStream.Length];

                fileStream.Read(array, 0, array.Length);

                textFromFile = Encoding.Default.GetString(array);
            }
            string[] lines = textFromFile.Split(';');

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (lines[i][j] == '0') matrix[i][j] = Const.CELL;
                    if (lines[i][j] == '1') matrix[i][j] = Const.WALL;
                    if (lines[i][j] == '2')
                    {
                        if (matrix[i][j] == Const.PLAYER)
                        {
                            Init();
                        }
                        matrix[i][j] = Const.COIN;
                        hasCoin = true;
                    }
                }
            }
            if (!hasCoin) Init();
        }

        public void AddEntity(int posX, int posY, char type)
        {
            matrix[posY][posX] = type;
        }

        public void RemoveEntity(int posX, int posY)
        {
            matrix[posY][posX] = ' ';
        }

        public string[] PrepareBuffer()
        {
            string[] buffer = new string[height];
            string line_buffer = "";
            string[] char_buffer = new string[width];
            for (int i = 0; i < height; i++)
            {
                if (!GUI)
                {
                    buffer[i] = string.Join(" ", matrix[i]);
                }
                else
                {
                    for (int j = 0; j < width; j++)
                    {
                        char_buffer[j] = matrix[i][j].ToString() + " ";
                        
                        if (matrix[i][j] == Const.WALL)
                        {
                            char_buffer[j] = char_buffer[j].Pastel(Color.LimeGreen).PastelBg(Color.LimeGreen);
                        }
                        
                        if (matrix[i][j] == Const.PLAYER)
                        {
                            char_buffer[j] = char_buffer[j].Pastel(Color.Purple).PastelBg(Color.Purple);
                        }

                        if (matrix[i][j] == Const.COIN)
                        {
                            char_buffer[j] = char_buffer[j].Pastel(Color.Gold).PastelBg(Color.Gold);
                        }
                        if (matrix[i][j] == Const.CELL)
                        {
                            char_buffer[j] = char_buffer[j].Pastel(Color.ForestGreen).PastelBg(Color.ForestGreen);
                        }
                        line_buffer = String.Join("",char_buffer);
                    }
                    buffer[i] = line_buffer;
                }
                //Console.WriteLine(line_buffer);
                
            }

            return buffer;
        }

        public bool IsCoin(int x, int y)
        {
            if (matrix[y][x] == Const.COIN) return true;
            return false;
        }

        public bool IsWall(int x, int y)
        {
            if (matrix[y][x] == Const.WALL) return true;
            return false;
        }

        public void Debug()
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Console.Write(matrix[i][j]);
                }
                Console.WriteLine();
            }
        }
    }
}
