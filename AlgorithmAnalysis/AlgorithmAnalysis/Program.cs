using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace AlgorithmAnalysis
{
    class Program
    {
        public static int generatedMapNumber = 1000;     //Generated maps number
        public static int testTimes = 10;
        public static int NoGo;
        public static double lastMemory;
        public static double availableMemory;
        public static int runningTime = 1000*120;
        public static int size = 16;                    //Map array size. Pauliaus algoritmai su lyginiais skaiciais neveikia. Kol kas darom su nelyginiais

        static void Main(string[] args)
        {
            MapsContainer container = new MapsContainer();

            ConsoleKeyInfo cki;
            bool exit = false;
            do
            {
                DisplayMenu();
                cki = Console.ReadKey();
                Console.Clear();
                Console.WriteLine("0 - wall\n1 - ground\n2 - ground spear\n3 - ground ladder\n" +
                "4 - ground enemy\n5 - ground pit\n6 - ground treasure\n");
                switch (cki.KeyChar.ToString())
                {
                    case "1":
                        Console.WriteLine(">1. Test Prim");
                        Test_Prim_Algorithm();
                        break;
                    case "2":
                        Console.WriteLine(">2. Test GrowingTree");

                        break;
                    case "3":
                        Console.WriteLine(">3. Test Hunt&Kill");
                        Test_HuntAndKill_Algorithm();
                        break;
                    case "4":
                        Console.WriteLine(">4. Test Sidewinder");

                        break;
                    case "5":
                        Console.WriteLine(">5. Test generated items");
                        Test_GenItems();
                        break;
                    case "6":
                        Console.WriteLine(">6. Analysis");
                        Analysis(container);
                        break;
                    case "7":
                        Console.WriteLine(">7. Save generated maps\n");
                        SaveMap();
                        break;
                    case "8":
                        exit = true;
                        break;
                }
            } while (exit == false);
        }

        public static void SaveMap()
        {
            for (int i = 0; i < 10; i++)
            {

                char[][] mapArray = new char[size][];

                mapArray = HuntAndKill.GenerateMap();
                GenerateItems(mapArray);

                Save save = new Save();
                List<Save.Tiles> t = new List<Save.Tiles>();
                Save.Tiles tile;

                #region generate
                for (int x = 0; x < mapArray.Length; x++)
                {
                    for (int y = 0; y < mapArray.Length; y++)
                    {
                        tile.tile = new GridTile();
                        int num = (int)char.GetNumericValue(mapArray[x][y]);
                        if (num >= 1)
                        {
                            tile.tile.type = GridTile.TileTypes.Ground;
                        }
                        if (num == 0)
                        {
                            tile.tile.type = GridTile.TileTypes.Wall;
                        }
                        tile.pos = new WorldPos(x, y);
                        if (HuntAndKill.StartingPoint.x == x && HuntAndKill.StartingPoint.y == y)
                        {
                            tile.tile.containedObject = GridTile.ContainedObject.StartingPoint;
                        }
                        else if (num == 2)
                        {
                            tile.tile.containedObject = GridTile.ContainedObject.Spear;
                        }
                        else if (num == 3)
                        {
                            tile.tile.containedObject = GridTile.ContainedObject.Ladder;
                        }
                        else if (num == 4)
                        {
                            tile.tile.containedObject = GridTile.ContainedObject.Enemy;
                        }
                        else if (num == 5)
                        {
                            tile.tile.containedObject = GridTile.ContainedObject.Pit;
                        }
                        else if (num == 6)
                        {
                            tile.tile.containedObject = GridTile.ContainedObject.Chest;
                        }
                        t.Add(tile);
                    }
                }
                #endregion

                save.tiles = t.ToArray();

                MemoryStream stream = new MemoryStream();
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Save));

                ser.WriteObject(stream, save);
                stream.Position = 0;
                StreamReader sr = new StreamReader(stream);
                FileStream file = new FileStream("maps\\"+i+";"+i+".json", FileMode.OpenOrCreate);
                stream.WriteTo(file);
                stream.Close();
                file.Close();
                file = new FileStream("levelConfig.cfg", FileMode.OpenOrCreate);
                string conf = "gridSize=" + HuntAndKill.mapSize;
                file.Write(Encoding.UTF8.GetBytes(conf), 0, conf.Length);
                file.Close();
            }
            Console.WriteLine("Saved!");
        }

        /// <summary>
        /// User interface
        /// </summary>
        static public void DisplayMenu()
        {
            Console.WriteLine("\n Menu map generating algorithms\n");
            Console.WriteLine(">1. Test Prim");
            Console.WriteLine(">2. Test GrowingTree");
            Console.WriteLine(">3. Test Hunt&Kill");
            Console.WriteLine(">4. Test Sidewinder");
            Console.WriteLine(">5. Test generated items");
            Console.WriteLine(">6. Analysis");
            Console.WriteLine(">7. Save generated maps");
            Console.WriteLine(">8. Exit \n");
            Console.Write(">");
        }

        /// <summary>
        /// Prim's algorithm test
        /// </summary>
        public static void Test_Prim_Algorithm()
        {
            MapClass mapArray = new MapClass(size);
            Console.WriteLine("\n Double array generated with Prim's algorithm \n");
            mapArray.generateMapPrim();
            mapArray.print();


        }
        /// <summary>
        /// Recursive backtracing algorithm test
        /// </summary>
        public static void Test_Recursive_Backtracing_Algorithm()
        {
            //MapClass mapArray = new MapClass(size);
            //Console.WriteLine("\n Double array generated with recursive backtracing algorithm \n");
            //mapArray.generateMapRecursiveBacktracing();
            //mapArray.print();


        }
        /// <summary>
        /// Hunt and kill algorithm test
        /// </summary>
        public static void Test_HuntAndKill_Algorithm()
        {
            char[][] mapArray = new char[size][];
            Console.WriteLine("\n Double array generated with Hunt&Kill algorithm \n");
            mapArray = HuntAndKill.GenerateMap();

            Console.WriteLine(" size: {0}x{0}\n", mapArray.Length);
            for (int i = 0; i < mapArray.Length; i++)
            {
                for (int j = 0; j < mapArray.Length; j++)
                {
                    if (HuntAndKill.StartingPoint.x == i && HuntAndKill.StartingPoint.y == j)
                    {
                        Console.Write("X");
                    }
                    else Console.Write(mapArray[i][j]);
                }
                Console.WriteLine();
            }
        }

        public static void Test_GenItems()
        {
            char[][] mapArray = new char[size][];
            mapArray = HuntAndKill.GenerateMap();
            GenerateItems(mapArray);

            for (int i = 0; i < mapArray.Length; i++)
            {
                for (int j = 0; j < mapArray.Length; j++)
                {
                    if (HuntAndKill.StartingPoint.x == i && HuntAndKill.StartingPoint.y == j)
                    {
                        Console.Write("X");
                    }
                    else Console.Write(mapArray[i][j]);
                }
                Console.WriteLine();
            }
        }


        /// <summary>
        /// Algorithm analysis (only generating time at this moment)
        /// </summary>
        /// <param name="container"></param>
        public static void Analysis(MapsContainer container)
        {
            Console.Write("Enter integer number for map size: ");
            string consoleSize = Console.ReadLine();
            while (!int.TryParse(consoleSize, out size))
            {
                Console.WriteLine("\nError: Invalid map size.\n");
                Console.WriteLine("Enter valid integer number for map size.");
                Console.WriteLine("Enter \"exit\" to go back to the main menu.");
                Console.Write("\n >> ");
                consoleSize = Console.ReadLine();
                if (consoleSize.ToLower().Trim() == "exit")
                {
                    return;
                }
            }

            if (size < 5)
            {
                Console.WriteLine("\nError: Too small map size: {0}", size);
                Console.WriteLine("Enter valid integer number for map size.");
                Console.WriteLine("Enter \"exit\" to go back to the main menu.");
                Console.Write("\n >> ");
                consoleSize = Console.ReadLine();
                if (consoleSize.ToLower().Trim() == "exit")
                {
                    return;
                }
                while (!int.TryParse(consoleSize, out size))
                {
                    Console.WriteLine("\nError: Invalid map size.\n");
                    Console.WriteLine("Enter valid integer number for map size.");
                    Console.WriteLine("Enter \"exit\" to go back to the main menu.");
                    Console.Write("\n >> ");
                    consoleSize = Console.ReadLine();
                    if (consoleSize.ToLower().Trim() == "exit")
                    {
                        return;
                    }
                }
            }
            Process proc = Process.GetCurrentProcess();
            availableMemory = 1800000000;// (new Microsoft.VisualBasic.Devices.ComputerInfo().AvailablePhysicalMemory) * 0.7;
            double memory = 0;
            Console.WriteLine("\n Prim's algorithm \n\n N        Runtime              RAM\n");

            Stopwatch myTimer = new Stopwatch();
            Stopwatch allTime = new Stopwatch();

            allTime.Start();
            lastMemory = proc.PrivateMemorySize64 / 1000;
            for (int j = 1; j <= testTimes; j++)
            {
                myTimer.Start();
                for (int i = 0; i < generatedMapNumber * j; i++)
                {
                    MapClass mapArray = new MapClass(size);
                    mapArray.generateMapPrim();
                    container.addMap(mapArray, 1);

                    if (i % 10 == 0)
                    {
                        if (proc.PrivateMemorySize64 >= availableMemory)
                        {
                            Console.WriteLine("\nError: Program used too much RAM: {0}MB {1}MB", proc.PrivateMemorySize64 / 1000000, availableMemory / 1000000);
                            return;
                        }
                    }
                }
                myTimer.Stop();
                proc.Refresh();
                memory = (proc.PrivateMemorySize64 / 1000) - lastMemory;
                Console.WriteLine(" {0,-5}    {1}     {2}B", generatedMapNumber * j, myTimer.Elapsed, memory);
                myTimer.Reset();
                allTime = myTimer;
                if (allTime.ElapsedMilliseconds > runningTime)
                {
                    Console.WriteLine("\nError: Program ran too long: {0}", allTime.Elapsed);
                    return;
                }
                if (proc.PrivateMemorySize64 >= availableMemory)
                {
                    Console.WriteLine("\nError: Program used too much RAM: {0}MB", proc.PrivateMemorySize64 / 1000000);
                    return;
                }
                GC.Collect();
            }
            lastMemory += memory;

            Console.WriteLine("\n GrowingTree algorithm \n\n N       Runtime              RAM\n");
            for (int j = 1; j <= testTimes; j++)
            {
                myTimer.Start();
                for (int i = 0; i < generatedMapNumber *j; i++)
                {
                    MapClass mapArray = new MapClass(size);
                    mapArray.generateMapRecursiveBacktracing();
                    container.addMap(mapArray, 2);
                }
                myTimer.Stop();
                proc.Refresh();
                memory = (proc.PrivateMemorySize64 / 1000) - lastMemory;
                Console.WriteLine(" {0,-5}    {1}     {2}B", generatedMapNumber *j, myTimer.Elapsed, memory);
                myTimer.Reset();
                if (allTime.ElapsedMilliseconds > runningTime)
                {
                    Console.WriteLine("\nError: Program ran too long: {0}", allTime.Elapsed);
                    return;
                }
                if (proc.PrivateMemorySize64 >= availableMemory)
                {
                    Console.WriteLine("\nError: Program used too much RAM: {0}MB", proc.PrivateMemorySize64 / 1000000);
                    return;
                }
                GC.Collect();
            }
            lastMemory += memory;

            Console.WriteLine("\n Hunt and kill algorithm \n\n N       Runtime              RAM\n");
            for (int j = 1; j <= testTimes; j++)
            {
                myTimer.Start();
                for (int i = 0; i < generatedMapNumber * j; i++)
                {
                    MapClass mapArray = new MapClass(size);
                    mapArray.huntKillArray = HuntAndKill.GenerateMap();
                    container.addMap(mapArray, 3);
                }
                myTimer.Stop();
                proc.Refresh();
                memory = (proc.PrivateMemorySize64 / 1000) - lastMemory;
                Console.WriteLine(" {0, -5}    {1}     {2}B", generatedMapNumber * j, myTimer.Elapsed, memory);
                myTimer.Reset();
                if (allTime.ElapsedMilliseconds > runningTime)
                {
                    Console.WriteLine("\nError: Program ran too long: {0}", allTime.Elapsed);
                    return;
                }
                if (proc.PrivateMemorySize64 >= availableMemory)
                {
                    Console.WriteLine("\nError: Program used too much RAM: {0}MB", proc.PrivateMemorySize64 / 1000000);
                    return;
                }
                GC.Collect();
            }
            allTime.Stop();
            GC.Collect();
        }

        public static void GenerateItems(char[][] mapArray)
        {

            List<TilePoint> validPos = new List<TilePoint>();

            for (int x = 1; x < size - 1; x++)
            {
                for (int y = 1; y < HuntAndKill.mapSize - 1; y++)
                {
                    if (HuntAndKill.StartingPoint.x == x && HuntAndKill.StartingPoint.y == y)
                    {
                        mapArray[x][y] = 'X';
                    }
                    else if (mapArray[x][y] == '1')
                    {
                        if (mapArray[x - 1][y] == '0' && mapArray[x + 1][y] == '0')
                        {
                            if (mapArray[x][y + 1] == '0' || mapArray[x][y - 1] == '0')
                            {
                                TilePoint p = new TilePoint();
                                p.pos = new WorldPos(x, y);

                                if (mapArray[x][y + 1] == '0')
                                {
                                    p.next = new WorldPos(x, y - 1);
                                }
                                else
                                {
                                    p.next = new WorldPos(x, y + 1);
                                }
                                validPos.Add(p);
                            }
                        }
                        else if (mapArray[x][y - 1] == '0' && mapArray[x][y + 1] == '0')
                        {
                            if (mapArray[x + 1][y] == '0' || mapArray[x - 1][y] == '0')
                            {
                                TilePoint p = new TilePoint();
                                p.pos = new WorldPos(x, y);

                                if (mapArray[x + 1][y] == '0')
                                {
                                    p.next = new WorldPos(x - 1, y);
                                }
                                else
                                {
                                    p.next = new WorldPos(x + 1, y);
                                }
                                validPos.Add(p);
                            }
                        }
                    }
                }
            }

            bool placedChest = false;
            bool placedSpear = false; 
            TilePoint point = new TilePoint();
            Random rand = new Random();


            if (validPos.Count == 1)
            {
                point = validPos[0];
                mapArray[point.pos.x][point.pos.y] = '6';
                validPos.RemoveAt(0);
            }
            else if (validPos.Count == 2)
            {
                int num = rand.Next(0, validPos.Count);
                point = validPos[num];
                mapArray[point.pos.x][point.pos.y] = '6';
                mapArray[point.next.x][point.next.y] = '4';
                validPos.RemoveAt(num);

                point = validPos[0];
                mapArray[point.pos.x][point.pos.y] = '2';
                validPos.RemoveAt(0);
            }
            else if (validPos.Count == 3)
            {
                int num = rand.Next(0, validPos.Count);
                point = validPos[num];
                mapArray[point.pos.x][point.pos.y] = '6';
                mapArray[point.next.x][point.next.y] = '4';
                validPos.RemoveAt(num);

                num = rand.Next(0, validPos.Count);
                point = validPos[num];
                mapArray[point.pos.x][point.pos.y] = '2';
                mapArray[point.next.x][point.next.y] = '5';
                validPos.RemoveAt(num);

                point = validPos[0];
                mapArray[point.pos.x][point.pos.y] = '3';
                validPos.RemoveAt(0);
            }

            while (validPos.Count > 0)
            {                
                int num = rand.Next(0, validPos.Count);

                if (validPos.Count == 1)
                {
                    point = validPos[num];
                    mapArray[point.pos.x][point.pos.y] = '3';
                }
                else if (!placedChest)
                {
                    point = validPos[num];
                    mapArray[point.pos.x][point.pos.y] = '6';
                    mapArray[point.next.x][point.next.y] = '4';
                    placedChest = true;
                }
                else
                {
                    if (!placedSpear)
                    {
                        point = validPos[num];
                        mapArray[point.pos.x][point.pos.y] = '2';
                        mapArray[point.next.x][point.next.y] = '5';
                        placedSpear = true;
                    }
                    else
                    {
                        point = validPos[num];
                        mapArray[point.pos.x][point.pos.y] = '3';
                        mapArray[point.next.x][point.next.y] = '5';
                    }
                }
                validPos.RemoveAt(num);
            }
        }

        public static void findNoGO(char[][] mapArray)
        {

            List<TilePoint> validPos = new List<TilePoint>();

            for (int x = 1; x < size - 1; x++)
            {
                for (int y = 1; y < HuntAndKill.mapSize - 1; y++)
                {
                    if (HuntAndKill.StartingPoint.x == x && HuntAndKill.StartingPoint.y == y)
                    {
                        mapArray[x][y] = 'X';
                    }
                    else if (mapArray[x][y] == '1')
                    {
                        if (mapArray[x - 1][y] == '0' && mapArray[x + 1][y] == '0')
                        {
                            if (mapArray[x][y + 1] == '0' || mapArray[x][y - 1] == '0')
                            {
                                TilePoint p = new TilePoint();
                                p.pos = new WorldPos(x, y);

                                if (mapArray[x][y + 1] == '0')
                                {
                                    NoGo++;
                                    p.next = new WorldPos(x, y - 1);
                                }
                                else
                                {
                                    NoGo++;
                                    p.next = new WorldPos(x, y + 1);
                                }
                                validPos.Add(p);
                            }
                        }
                        else if (mapArray[x][y - 1] == '0' && mapArray[x][y + 1] == '0')
                        {
                            if (mapArray[x + 1][y] == '0' || mapArray[x - 1][y] == '0')
                            {
                                TilePoint p = new TilePoint();
                                p.pos = new WorldPos(x, y);

                                if (mapArray[x + 1][y] == '0')
                                {
                                    NoGo++;
                                    p.next = new WorldPos(x - 1, y);
                                }
                                else
                                {
                                    NoGo++;
                                    p.next = new WorldPos(x + 1, y);
                                }
                                validPos.Add(p);
                            }
                        }
                    }
                }
            }

            bool placedChest = false;
            bool placedSpear = false;
            TilePoint point = new TilePoint();
            Random rand = new Random();


            if (validPos.Count == 1)
            {
                point = validPos[0];
                mapArray[point.pos.x][point.pos.y] = '6';
                validPos.RemoveAt(0);
            }
            else if (validPos.Count == 2)
            {
                int num = rand.Next(0, validPos.Count);
                point = validPos[num];
                mapArray[point.pos.x][point.pos.y] = '6';
                mapArray[point.next.x][point.next.y] = '4';
                validPos.RemoveAt(num);

                point = validPos[0];
                mapArray[point.pos.x][point.pos.y] = '2';
                validPos.RemoveAt(0);
            }
            else if (validPos.Count == 3)
            {
                int num = rand.Next(0, validPos.Count);
                point = validPos[num];
                mapArray[point.pos.x][point.pos.y] = '6';
                mapArray[point.next.x][point.next.y] = '4';
                validPos.RemoveAt(num);

                num = rand.Next(0, validPos.Count);
                point = validPos[num];
                mapArray[point.pos.x][point.pos.y] = '2';
                mapArray[point.next.x][point.next.y] = '5';
                validPos.RemoveAt(num);

                point = validPos[0];
                mapArray[point.pos.x][point.pos.y] = '3';
                validPos.RemoveAt(0);
            }

            while (validPos.Count > 0)
            {
                int num = rand.Next(0, validPos.Count);

                if (validPos.Count == 1)
                {
                    point = validPos[num];
                    mapArray[point.pos.x][point.pos.y] = '3';
                }
                else if (!placedChest)
                {
                    point = validPos[num];
                    mapArray[point.pos.x][point.pos.y] = '6';
                    mapArray[point.next.x][point.next.y] = '4';
                    placedChest = true;
                }
                else
                {
                    if (!placedSpear)
                    {
                        point = validPos[num];
                        mapArray[point.pos.x][point.pos.y] = '2';
                        mapArray[point.next.x][point.next.y] = '5';
                        placedSpear = true;
                    }
                    else
                    {
                        point = validPos[num];
                        mapArray[point.pos.x][point.pos.y] = '3';
                        mapArray[point.next.x][point.next.y] = '5';
                    }
                }
                validPos.RemoveAt(num);
            }
        }

        public struct TilePoint
        {
            public WorldPos pos;
            public WorldPos next;
        }
    }
}