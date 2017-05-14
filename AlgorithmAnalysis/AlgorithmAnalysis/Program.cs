using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Json;

namespace AlgorithmAnalysis
{
    class Program
    {
        public static int generatedMapNumber = 1000;    //Generated maps number
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
                        Console.WriteLine(">1. Test ");
                        Test_Prim_Algorithm();
                        break;
                    case "2":
                        Console.WriteLine(">2. Test ");
                        Test_Recursive_Backtracing_Algorithm();
                        break;
                    case "3":
                        Console.WriteLine(">3. Test ");
                        Test_HuntAndKill_Algorithm();
                        break;
                    case "4":
                        Console.WriteLine(">4. Analysis");
                        Analysis(container);
                        break;                    
                    case "5":
                        SaveMap();
                        break;
                    case "6":
                        exit = true;
                        break;
                }
            } while (exit == false);
        }

        public static void SaveMap()
        {
            char[][] mapArray = new char[size][];
            mapArray = HuntAndKill.GenerateMap();

            Save save = new Save();
            List<Save.Tiles> t = new List<Save.Tiles>();
            Save.Tiles tile;
            for (int x = 0; x < mapArray.Length; x++)
            {
                for (int y = 0; y < mapArray.Length; y++)
                {
                    tile.tile = new GridTile();
                    int num = (int)char.GetNumericValue(mapArray[x][y]);
                    if (num == 1)
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
                    t.Add(tile);
                }
            }
            save.tiles = t.ToArray();

            MemoryStream stream = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Save));

            ser.WriteObject(stream, save);
            stream.Position = 0;
            StreamReader sr = new StreamReader(stream);
            FileStream file = new FileStream("0;0.json", FileMode.OpenOrCreate);
            stream.WriteTo(file);
            stream.Close();
            file.Close();
            Console.WriteLine("Saved!");
        }

        /// <summary>
        /// User interface
        /// </summary>
        static public void DisplayMenu()
        {
            Console.WriteLine("\n Menu map generating algorithms\n");
            Console.WriteLine(">1. Test Prim");
            Console.WriteLine(">2. Test recursive backtracing");
            Console.WriteLine(">3. Hunt&Kill");
            Console.WriteLine(">4. Analysis");
            Console.WriteLine(">5. Save Hunt&Kill");
            Console.WriteLine(">6. Exit \n");
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
                    Console.Write(mapArray[i][j]);
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
            Process proc = Process.GetCurrentProcess();
            double memory = 0;
            Console.WriteLine("\n Prim's algorithm \n\n N       Runtime              RAM\n");
            Stopwatch myTimer = new Stopwatch();
            myTimer.Start();
            for (int i = 0; i < generatedMapNumber; i++)
            {
                MapClass mapArray = new MapClass(size);
                mapArray.generateMapPrim();
                container.addMap(mapArray, 1);
            }
            myTimer.Stop();
            proc.Refresh();
            memory = proc.PrivateMemorySize64 / 1000;
            Console.WriteLine(" {0}    {1}     {2}B", generatedMapNumber, myTimer.Elapsed, memory);
            GC.Collect();


            Console.WriteLine("\n Recursive backtracing algorithm \n\n N       Runtime              RAM\n");
            myTimer.Reset();
            myTimer.Start();
            for (int i = 0; i < generatedMapNumber; i++)
            {
                MapClass mapArray = new MapClass(size);
                mapArray.generateMapRecursiveBacktracing();
                container.addMap(mapArray, 2);
            }
            myTimer.Stop();
            proc.Refresh();
            memory = proc.PrivateMemorySize64 / 1000;
            Console.WriteLine(" {0}    {1}     {2}B", generatedMapNumber, myTimer.Elapsed, memory);
            GC.Collect();


            Console.WriteLine("\n Hunt and kill algorithm \n\n N       Runtime              RAM\n");
            myTimer.Reset();
            myTimer.Start();
            for (int i = 0; i < generatedMapNumber; i++)
            {
                MapClass mapArray = new MapClass(size);
                mapArray.generateMapHuntAndKill();
                container.addMap(mapArray, 3);
            }
            myTimer.Stop();
            proc.Refresh();
            memory = proc.PrivateMemorySize64 / 1000;
            Console.WriteLine(" {0}    {1}     {2}B", generatedMapNumber, myTimer.Elapsed, memory);
            GC.Collect();
        }
    }
}