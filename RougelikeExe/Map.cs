using Pastel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougelikeExe
{
    class Map
    {
        private char[][] matrix;
        private readonly int width;
        private readonly int height;
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
            //reading currentMaze.txt
            string textFromFile;
            using (FileStream fileStream = File.OpenRead(Const.PATHTXT))
            {
                byte[] array = new byte[fileStream.Length];

                fileStream.Read(array, 0, array.Length);

                textFromFile = System.Text.Encoding.Default.GetString(array);
            }
            string[] lines = textFromFile.Split(';');

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (lines[i][j] == '0') matrix[i][j] = Const.CELL;
                    if (lines[i][j] == '1') matrix[i][j] = Const.WALL;
                    if (lines[i][j] == '2') matrix[i][j] = Const.COIN;
                }
            }
        }

        public void AddEntity(int posX, int posY, char type)
        {
            matrix[posY][posX] = type;
        }

        public void RemoveEntity(int posX, int posY)
        {
            matrix[posY][posX] = ' ';
        }

        public void Print()
        {
            string buffer;
            for (int i = 0; i < height; i++)
            {
                if (!GUI)
                {
                    Console.WriteLine(string.Join(" ", matrix[i]));
                }
                else
                {
                    for (int j = 0; j < width; j++)
                    {
                        buffer = matrix[i][j].ToString() + " ";
                        
                        if (matrix[i][j] == Const.WALL)
                        {
                            buffer = buffer.Pastel(Color.White).PastelBg(Color.White);
                        }
                        
                        if (matrix[i][j] == Const.PLAYER)
                        {
                            buffer = buffer.Pastel(Color.LimeGreen).PastelBg(Color.LimeGreen);
                        }

                        if (matrix[i][j] == Const.COIN)
                        {
                            buffer = buffer.Pastel(Color.Gold).PastelBg(Color.Gold);
                        }
                        if (matrix[i][j] == Const.CELL)
                        {
                            //buffer.Pastel(Color.Green).PastelBg(Color.Green);
                        }
                        

                        Console.Write(buffer);
                        //Console.BackgroundColor = ConsoleColor.Black;
                        //Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.WriteLine();
                }
                
            }
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
    }
}
