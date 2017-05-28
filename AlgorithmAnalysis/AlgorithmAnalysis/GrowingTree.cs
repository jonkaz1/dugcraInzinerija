using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmAnalysis
{
    class GrowingTree
    {
        public static bool isOddNumber = true;
        public static int x = 0;
        public static int y = 0;
        public static int xs = 0;
        public static int ys = 0;
        public static int mapSize;
        public static char direction = 'N';
        public static char[][] grid;
        public static char[] directions = new char[4];
        public static bool first = true;
        public static LinkedList<char> list = new LinkedList<char>();
        public static char[][] GenerateMap(int sizeOfMap)
        {
            if(sizeOfMap%2==0)
            {
                mapSize = sizeOfMap+1;
                isOddNumber = true;
            }
            else
            {
                mapSize = sizeOfMap;
                isOddNumber = false;
            }

            grid = new char[mapSize][];
            Random random = new Random();

            //Gridas pilnas sienu
            directions[0] = 'E';
            directions[1] = 'S';
            directions[2] = 'W';
            directions[3] = 'N';
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

            //Random pozicija
            x = RandomNumberOdd(1, mapSize-1, random);
            y = RandomNumberOdd(1, mapSize-1, random);
            xs = x;
            ys = y;
            grid[y][x] = '1';

            bool creating = true;

            int kazkoks;
            while (creating)
            {



                kazkoks = random.Next(0, 4);
                direction = directions[kazkoks];
                if (canWalk())
                {
                    Walk();
                }
                else if (!canWalk())
                {
                    direction = list.Last.Value;
                    list.RemoveLast();
                    fallback();

                }
                if (!canWalk() && x == xs && y == ys)
                {
                    creating = false;
                }

            }

            return grid;

        }
        private static void fallback()
        {
            switch (direction)
            {
                case 'E':
                    direction = 'W';
                    x = x - 2;
                    grid[y][x + 1] = '1';
                    grid[y][x] = '1';
                    break;

                case 'S':
                    direction = 'N';
                    y = y - 2;
                    grid[y + 1][x] = '1';
                    grid[y][x] = '1';
                    break;

                case 'W':
                    direction = 'E';
                    x = x + 2;
                    grid[y][x - 1] = '1';
                    grid[y][x] = '1';
                    break;

                case 'N':
                    direction = 'S';
                    y = y + 2;
                    grid[y - 1][x] = '1';
                    grid[y][x] = '1';
                    break;
            }
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
                    Console.Write(grid[i][j]);
                }
                Console.WriteLine();
            }
        }
        private static void Walk()
        {
            switch (direction)
            {
                case 'E':
                    if ((x + 2 != mapSize))
                    {
                        if (!BeenThere(x + 2, y))
                        {
                            if (!first)
                            {
                                list.AddLast(direction);
                            }
                            else
                            {
                                list.AddFirst(direction);
                                first = false;
                            }
                            x = x + 2;
                            grid[y][x - 1] = '1';
                            grid[y][x] = '1';
                        }

                    }
                    break;

                case 'S':
                    if ((y + 2 != mapSize))
                    {
                        if (!BeenThere(x, y + 2))
                        {
                            if (!first)
                            {
                                list.AddLast(direction);
                            }
                            else
                            {
                                list.AddFirst(direction);
                                first = false;
                            }
                            y = y + 2;
                            grid[y - 1][x] = '1';
                            grid[y][x] = '1';
                        }

                    }
                    break;

                case 'W':
                    if ((x - 2 != -1))
                    {
                        if (!BeenThere(x - 2, y))
                        {
                            if (!first)
                            {
                                list.AddLast(direction);
                            }
                            else
                            {
                                list.AddFirst(direction);
                                first = false;
                            }
                            x = x - 2;
                            grid[y][x + 1] = '1';
                            grid[y][x] = '1';
                        }

                    }
                    break;

                case 'N':
                    if ((y - 2 != -1))
                    {
                        if (!BeenThere(x, y - 2))
                        {
                            if (!first)
                            {
                                list.AddLast(direction);
                            }
                            else
                            {
                                list.AddFirst(direction);
                                first = false;
                            }
                            y = y - 2;
                            grid[y + 1][x] = '1';
                            grid[y][x] = '1';
                        }

                    }
                    break;
            }
        }
        private static bool BeenThere(int xB, int yB)
        {
            if (grid[yB][xB] == '1')
            {
                return true;
            }
            else
                return false;
        }
        private static int RandomNumberOdd(int min, int max, Random random)
        {
            int ans = random.Next(min, max);
            if (ans % 2 == 1) return ans;
            else
            {
                if (ans + 1 <= max)
                    return ans + 1;
                else if (ans - 1 >= min)
                    return ans - 1;
                else return 0;
            }
        }
        private static bool canWalk()
        {
            int east = 0;
            int south = 0;
            int west = 0;
            int north = 0;

            if (x + 2 == mapSize)
            {
                east = 1;
            }
            else if (BeenThere(x + 2, y))
            {
                east = 1;
            }

            if (y + 2 == mapSize)
            {
                south = 1;
            }
            else if (BeenThere(x, y + 2))
            {
                south = 1;
            }

            if ((x - 2 == -1))
            {
                west = 1;
            }
            else if (BeenThere(x - 2, y))
            {
                west = 1;
            }

            if ((y - 2 == -1))
            {
                north = 1;
            }
            else if (BeenThere(x, y - 2))
            {
                north = 1;
            }
            if (east + south + west + north == 4)
            {
                return false;
            }
            return true;
        }
        public static void print(char[][] map)
        {
            if (isOddNumber)
            {
                Console.WriteLine(" size: {0}x{0}\n", map.Length - 1);
            }
            else
                Console.WriteLine(" size: {0}x{0}\n", map.Length);
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
