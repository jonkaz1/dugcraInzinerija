using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmAnalysis
{
    class HuntAndKill
    {
        public static int x = 0;
        public static int y = 0;
        public static int mapSize = 15;
        public static char direction = 'N';
        public static char huntDirecton = 'B';
        public static WorldPos StartingPoint = new WorldPos();        
        public static char[][] grid = new char[mapSize][];
        public static char[] directions = new char[4];

        public static char[][] GenerateMap()
        {
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
            x = RandomNumberOdd(1, mapSize - 1, random);
            y = RandomNumberOdd(1, mapSize - 1, random);
            grid[y][x] = '1';
            StartingPoint = new WorldPos(x, y);

            bool hunting = true;

            while (hunting)
            {
                hunting = true;
                int kazkoks = random.Next(0, 4);
                direction = directions[kazkoks];
                if (canWalk())
                {
                    if (Walk())
                    {
                    }
                }
                else
                {
                    if (Hunt())
                    {
                        if (Walk())
                        {
                        }
                    }
                    else
                    {
                        hunting = false;
                    }
                }
            }
            return grid;
        }
        public static void refresh()
        {
            Console.Clear();
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid.Length; j++)
                {
                    Console.Write(grid[i][j]);
                }
                Console.WriteLine();
            }
        }
        public static bool Walk()
        {
            switch (direction)
            {
                case 'E':
                    if ((x + 2 != mapSize))
                    {
                        if (!BeenThere(x + 2, y))
                        {
                            x = x + 2;
                            grid[y][x - 1] = '1';
                            grid[y][x] = '1';
                            return true;
                        }
                    }
                    break;
                case 'S':
                    if ((y + 2 != mapSize))
                    {
                        if (!BeenThere(x, y + 2))
                        {
                            y = y + 2;
                            grid[y - 1][x] = '1';
                            grid[y][x] = '1';
                            return true;
                        }
                    }
                    break;
                case 'W':
                    if ((x - 2 != -1))
                    {
                        if (!BeenThere(x - 2, y))
                        {
                            x = x - 2;
                            grid[y][x + 1] = '1';
                            grid[y][x] = '1';

                            return true;
                        }
                    }
                    break;

                case 'N':
                    if ((y - 2 != -1))
                    {
                        if (!BeenThere(x, y - 2))
                        {
                            y = y - 2;
                            grid[y + 1][x] = '1';
                            grid[y][x] = '1';
                            return true;
                        }
                    }
                    break;
            }
            return false;
        }
        public static bool canWalk()
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
        public static bool canWalkInHunt()
        {
            int east = 0;
            int south = 0;
            int west = 0;
            int north = 0;

            if (x + 2 == mapSize)
            {
                east = 1;
            }
            else if (!BeenThere(x + 2, y))
            {
                east = 1;
            }

            if (y + 2 == mapSize)
            {
                south = 1;
            }
            else if (!BeenThere(x, y + 2))
            {
                south = 1;
            }

            if ((x - 2 == -1))
            {
                west = 1;
            }
            else if (!BeenThere(x - 2, y))
            {
                west = 1;
            }

            if ((y - 2 == -1))
            {
                north = 1;
            }
            else if (!BeenThere(x, y - 2))
            {
                north = 1;
            }
            if (east == 0)
            {
                x = x + 2;
                huntDirecton = 'E';
                return true;
            }
            if (south == 0)
            {
                y = y + 2;
                huntDirecton = 'S';
                return true;
            }
            if (west == 0)
            {
                x = x - 2;
                huntDirecton = 'W';
                return true;
            }
            if (north == 0)
            {
                y = y - 2;
                huntDirecton = 'N';
                return true;
            }
            return false;
        }
        public static bool BeenThere(int xB, int yB)
        {
            if (grid[yB][xB] == '1')
            {
                return true;
            }
            else
                return false;
        }
        public static bool Hunt()
        {
            bool anwser = false;
            for (int i = 1; i < grid.Length - 1; i += 2)
            {
                for (int j = 1; j < grid.Length - 1; j += 2)
                {
                    x = j;
                    y = i;
                    if (grid[i][j] == '0' && canWalkInHunt())
                    {


                        grid[i][j] = '1';
                        ConnectPrevious(j, i, x, y);

                        x = j;
                        y = i;
                        return true;
                    }

                }
            }
            return anwser;
        }
        public static bool ConnectPrevious(int xN, int yN, int xS, int yS)
        {
            switch (huntDirecton)
            {
                case 'E':
                    for (int k = xN; k != xS; k++)
                    {
                        grid[yN][k] = '1';
                    }

                    return true;

                case 'S':
                    for (int k = yN; k != yS; k++)
                    {
                        grid[k][xN] = '1';
                    }

                    return true;

                case 'W':
                    for (int k = xS; k != xN; k++)
                    {
                        grid[yN][k] = '1';
                    }
                    return true;


                case 'N':
                    for (int k = yS; k != yN; k++)
                    {
                        grid[k][xN] = '1';
                    }

                    return true;
            }
            return false;
        }
        //------------------------------------------------------
        public static int RandomNumberOdd(int min, int max, Random random)
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
        public void print(char[][] map)
        {
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
