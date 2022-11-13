using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Pacman
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool isPlaying = true;
            int pacmanX = 0;
            int pacmanY = 0;
            int pacmanDX = 0;
            int pacmanDY = 0;
            int lastDX = 0;
            int lastDY = 0;

            char[,] map = ReadMap("level1", ref pacmanX, ref pacmanY);
            DrawMap(map);

            while (isPlaying)
            {
                ShowStat(pacmanX, pacmanY, pacmanDX, pacmanDY, lastDX, lastDY);
                Console.SetCursorPosition(pacmanY, pacmanX);
                Console.Write('@');
                Console.CursorVisible = false;

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    ChooseDirection(key, ref pacmanDX, ref pacmanDY, pacmanX, pacmanY, ref lastDX, ref lastDY, map);
                }

                else if (map[pacmanX + pacmanDX, pacmanY + pacmanDY] != '#')
                {
                    ClearTrace(pacmanX, pacmanY);
                    Move(ref pacmanX,ref pacmanY,pacmanDX,pacmanDY, map);
                }

                System.Threading.Thread.Sleep(80);
            }
        }

        static char[,] ReadMap(string mapName, ref int pacmanX, ref int pacmanY)
        {
            string[] newFile = File.ReadAllLines($"maps/{mapName}.txt");
            char[,] map = new char[newFile.Length, newFile[0].Length];

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = newFile[i][j];
                    if (map[i, j] == '@')
                    {
                        pacmanX = i;
                        pacmanY = j;
                    }
                    else if (map[i,j] != '#')
                    {
                        map[i,j]= '.';
                    }
                }
            }
            return map;
        }

        static void DrawMap(char[,] map)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i,j]);
                }
                Console.WriteLine();
            }
        }

        static void Move (ref int X, ref int Y, int DX, int DY, char[,] map)
        {
            X += DX;
            Y += DY;
            Console.SetCursorPosition(Y, X);
            Console.Write("@");

            if (X == map.GetLength(1) - 2 && DX != -1)
            {
                ClearTrace(X, Y);
                X = 0;
                Console.SetCursorPosition(Y, X);
                Console.Write("@");
            }
            else if (X == 0 && DX != 1)
            {
                ClearTrace(X, Y);
                X = map.GetLength(1) - 2;
                Console.SetCursorPosition(Y, X);
                Console.Write("@");
            }

            else if (Y == map.GetLength(0) && DY != -1)
            {
                ClearTrace(X, Y);
                Y = 0;
                Console.SetCursorPosition(Y, X); 
                Console.Write("@");

            }
            else if (Y == 0 && DY != 1)
            {
                ClearTrace(X, Y);
                Y = map.GetLength(0) - 1;
                Console.SetCursorPosition(Y, X);
                Console.Write("@");

            }
        }

        static void ClearTrace(int X, int Y)
        {
            Console.SetCursorPosition(Y, X);
            Console.Write(" ");
        }

        static void ChooseDirection(ConsoleKeyInfo key, ref int DX, ref int DY, int X, int Y, ref int lastDX, ref int lastDY, char[,] map)
        {
            

            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    if (map[X + DX, Y + DY] == '#')
                    {
                        DX = -1; DY = 0;
                        lastDX = DX;
                        lastDY = DY;

                    }
                    else
                    {
                        DX = lastDX;
                        DY = lastDY;
                    }
                    break;

                case ConsoleKey.DownArrow:
                    if (map[X + DX, Y + DY] == '#')
                    {
                        DX = 1; DY = 0;
                        lastDX = DX;
                        lastDY = DY;

                    }
                    else
                    {
                        DX = lastDX;
                        DY = lastDY;
                    }
                    break;

                case ConsoleKey.LeftArrow:
                    if (map[X + DX, Y + DY] == '#')
                    {
                        DX = 0; DY = -1;
                        lastDX = DX;
                        lastDY = DY;

                    }
                    else
                    {
                        DX = lastDX;
                        DY = lastDY;
                    }
                    break;

                case ConsoleKey.RightArrow:
                    if (map[X + DX, Y + DY] == '#' || (DX == 0 && DY == 0))
                    {
                        DX = 0; DY = 1;
                        lastDX = DX;
                        lastDY = DY;

                    }
                    else
                    {
                        DX = lastDX;
                        DY = lastDY;
                    }
                    break;

                default:
                    break;
            }
        }

        static void ShowStat (int X, int Y, int DX, int DY, int lastDX, int lastDY)
        {
            Console.SetCursorPosition(50, 0);
            Console.WriteLine($"X равен - {X} Y равен - {Y}");
            Console.SetCursorPosition(50, 1);

            Console.WriteLine($"DX равен - {DX} DY равен - {DY}");
            Console.SetCursorPosition(50, 2);

            Console.WriteLine($"lastDX равен - {lastDX} lastDY равен - {lastDY}");


        }
    }
}
