using System;
using System.Collections.Generic;

namespace AlgorithmAnalysis
{
    public class MapClass
    {
        public char[][] huntKillArray;

        public Node[,] data;

        public Node isStart;
        //Length of data array (one line). Length * Length = full length of the data array
        public int length;

        //Integer number means generated algorithm number (1-Prim's; 2-RecursiveBacktracing, etc.)
        public int algorithm;

        public MapClass(int size)
        {
            length = size;
            data = new Node[size, size];
            huntKillArray = new char[size][];
        }
        public int getData(int x, int y)
        {
            return data[x, y].Data;
        }
        public void setData(int x, int y, int field)
        {
            data[x, y].Data = field;
        }
        /// <summary>
        /// Prints map double array and size
        /// </summary>
        public void print()
        {
            Console.WriteLine(" size: {0}x{0}\n", length);
            for (int x = 0; x < length; x++)
            {
                for (int y = 0; y < length; y++)
                {
                    if (!data[x, y].isStart)
                    {
                        Console.Write("{0}", data[x, y].Data);
                    }
                    else
                    {
                        Console.Write("#");
                    }
                    
                }
                Console.WriteLine();
            }
        }


        /// <summary>
        /// First algorithm
        /// Reiktų perkelti kodą iš unity projekto į konsolinę aplinką čia.
        /// </summary>
        public void generateMapPrim()
        {
            algorithm = 1;
            int wall = 0;
            int ground = 1;
            //Generate double array with prim algorithm
            for (int x = 0; x < length; x++)
            {
                for (int y = 0; y < length; y++)
                {
                    data[x, y] = new Node(wall);
                    data[x, y].Position = new Point(x, y);
                    if (y > 0)
                    {
                        data[x, y][0] = data[x, y - 1];
                        data[x, y - 1][2] = data[x, y];
                    }
                    if (x > 0)
                    {
                        data[x, y][3] = data[x - 1, y];
                        data[x - 1, y][1] = data[x, y];
                    }
                }
            }
            Random r = new Random();
            Node current = data[r.Next(0, length), r.Next(0, length)];
            current.Data = ground;
            current.isStart = true;
            isStart = current;

            List<Node> validNodes = new List<Node>();

            for (int i = 0; i < 4; i++)
            {
                if (current[i] != null)
                {
                    validNodes.Add(current[i]);
                    current.Data = ground;
                }
            }
            while (validNodes.Count > 0)
            {
                int ran = r.Next(0, validNodes.Count);

                List<Node> readyNeighbours = new List<Node>();

                #region ifs
                if (validNodes[ran][0] != null && validNodes[ran][0].Data == wall
                            && validNodes[ran][0][3] != null && validNodes[ran][0][1] != null
                            && validNodes[ran][0][3].Data == wall && validNodes[ran][0][1].Data == wall
                            && validNodes[ran][0][0] != null && validNodes[ran][0][0].Data == wall)
                {
                    readyNeighbours.Add(validNodes[ran][0]);
                }
                if (validNodes[ran][1] != null && validNodes[ran][1].Data == wall
                    && validNodes[ran][1][0] != null && validNodes[ran][1][2] != null
                    && validNodes[ran][1][0].Data == wall && validNodes[ran][1][2].Data == wall
                    && validNodes[ran][1][1] != null && validNodes[ran][1][1].Data == wall)
                {
                    readyNeighbours.Add(validNodes[ran][1]);
                }
                if (validNodes[ran][2] != null && validNodes[ran][2].Data == wall
                    && validNodes[ran][2][3] != null && validNodes[ran][2][1] != null
                    && validNodes[ran][2][3].Data == wall && validNodes[ran][2][1].Data == wall
                    && validNodes[ran][2][2] != null && validNodes[ran][2][2].Data == wall)
                {
                    readyNeighbours.Add(validNodes[ran][2]);
                }
                if (validNodes[ran][3] != null && validNodes[ran][3].Data == wall
                    && validNodes[ran][3][0] != null && validNodes[ran][3][2] != null
                    && validNodes[ran][3][0].Data == wall && validNodes[ran][3][2].Data == wall
                    && validNodes[ran][3][3] != null && validNodes[ran][3][3].Data == wall)
                {
                    readyNeighbours.Add(validNodes[ran][3]);
                }
                #endregion

                while (readyNeighbours.Count > 0)
                {
                    int rand = r.Next(0, readyNeighbours.Count);
                    readyNeighbours[rand].Data = ground;
                    validNodes.Add(readyNeighbours[rand]);
                    readyNeighbours.RemoveAt(rand);
                }

                validNodes.RemoveAt(ran);
            }
        }


        /// <summary>
        /// Second algorithm
        /// </summary>
        public void generateMapRecursiveBacktracing()
        {
            algorithm = 2;
            //Generate double array with recursive backtracing algorithm Hunt&Kill
        }


        /// <summary>
        /// Third algorithm
        /// </summary>
        public void generateMapHuntAndKill()
        {
            algorithm = 3;
            //Generate double array with Hunt&Kill algorithm
        }

        /// <summary>
        /// Node class
        /// </summary>
        public class Node
        {
            private int data;

            public bool isStart;

            private Node up, right, down, left;

            private Point pos;

            /// <summary>
            /// Getter/setter for adj. nodes. 0 - up, 1 - right, 2 - down, 3 - left
            /// </summary>
            /// <param name="index">referenced node</param>
            /// <returns>Node</returns>
            public Node this[int index]
            {
                get
                {
                    switch (index)
                    {
                        case 0:
                            return up;
                        case 1:
                            return right;
                        case 2:
                            return down;
                        case 3:
                            return left;
                        default:
                            return null;
                    }
                }
                set
                {
                    switch (index)
                    {
                        case 0:
                            up = value;
                            break;
                        case 1:
                            right = value;
                            break;
                        case 2:
                            down = value;
                            break;
                        case 3:
                            left = value;
                            break;
                        default:
                            break;
                    }
                }
            }

            public Node(int data = 0)
            {
                this.data = data;
            }

            public int Data
            {
                get
                {
                    return data;
                }
                set
                {
                    data = value;
                }
            }

            public Point Position
            {
                get
                {
                    return pos;
                }
                set
                {
                    pos = value;
                }
            }
        }

        public struct Point
        {
            public int x, y;

            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }
    }
}
