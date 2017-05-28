using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmAnalysis
{
    class Sidewinder
    {
        public static int x = 1;
        public static int y = 3;
        public static int xs = 1;
        public static int ys = 1;
        public static int mapSize;
        public static char[][] grid;
        public static char[] directions = new char[4];
        public static Random random = new Random();
        //starting pointo nera, jis kaip ir virsutine map'o dalis, kai y=1, x={1;size-1};

        public static char[][] GenerateMap(int sizeOfMap)
        {

            if (sizeOfMap % 2 == 0)
            {
                mapSize = sizeOfMap + 1;
            }
            else
            {
                mapSize = sizeOfMap;
            }
            grid = new char[mapSize][];
            x = 1;
            y = 3;
            xs = 1;
            ys = 1;
            for (int i = 0; i < grid.Length; i++)
            {
                grid[i] = new char[mapSize];
            }


            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    grid[i][j] = '0';
                }
            }
            for (int j = 1; j < grid[0].Length - 1; j++)
            {
                grid[1][j] = '1';
            }

            //---------------------------

            bool creating = true;

            while (creating)
            {
                creating = eat();
            }
            return grid;
        }
        private static void refresh()
        {
            Console.Clear();
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid.Length; j++)
                {

                    if (i == y && x == j)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    if (i % 2 == 0 && grid[i][j] == '1')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.Write(grid[i][j]);
                }
                Console.WriteLine();
            }
        }
        static void eatpath(int start, int end)
        {

            int rand;
            rand = random.Next(start, end + 1);

            while (grid[y - 2][rand] == '0')
            {
                rand = random.Next(start, end + 1);
            }
            grid[y - 1][rand] = '1';
        }
        static bool eat()
        {
            int kazkoks = 0;
            int kazkoks2 = 0;
            int start = 1;
            int end = 0;
            while (x < mapSize - 1)
            {
                kazkoks = random.Next(0, 2);
                kazkoks2 = random.Next(0, 3);

                switch (kazkoks)
                {
                    case 0:
                        if (x + 1 < mapSize - 1)
                        {
                            start = x;
                            grid[y][x] = '1';
                            grid[y][x + 1] = '1';
                            end = x + 1;
                            eatpath(start, end);
                            if (kazkoks2 == 2)
                            {
                                x += 2;
                            }
                            else
                            {
                                x += 3;
                            }



                            if (x == mapSize - 1)
                            {
                                grid[y][x - 1] = '1';
                            }
                        }
                        break;

                    case 1:
                        if (x + 3 < mapSize - 1)
                        {
                            start = x;
                            grid[y][x] = '1';
                            grid[y][x + 1] = '1';
                            grid[y][x + 2] = '1';
                            grid[y][x + 3] = '1';
                            end = x + 4;
                            eatpath(start, end);
                            if (kazkoks2 == 2)
                            {
                                x += 4;
                            }
                            else
                            {
                                x += 5;
                            }

                            if (x == mapSize - 1)
                            {
                                grid[y][x - 1] = '1';
                            }

                        }
                        break;

                }
                if (x == mapSize - 2)
                {
                    grid[y][x] = '1';
                    grid[y][x - 1] = '1';
                    x += 2;
                }
            }
            if (y + 2 < mapSize - 1)
            {
                x = 1;
                y += 2;
                return true;
            }
            else
            {
                return false;
            }
        }
        public static void print(char[][] map)
        {
            Console.WriteLine(" size: {0}x{0}\n", map.Length-1);
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid.Length; j++)
                {
                    Console.Write(grid[i][j]);
                }
                Console.WriteLine();
            }


        }
    }
}
