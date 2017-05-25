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
        public static int savedMapsNumber = 10;     //Generated maps number
        public static int testTimes = 10;
        public static int NoGo;
        public static double lastMemory;
        public static double availableMemory;
        public static int runningTime = 1000*120;
        public static int size = 16;                    //Map array size. Pauliaus algoritmai su lyginiais skaiciais neveikia. Kol kas darom su nelyginiais

        static MapsContainer container = new MapsContainer();

        static void Main(string[] args)
        {
            //ConsoleKeyInfo cki;
            //bool exit = false;
            //do
            //{
            //    DisplayMenu();
            //    cki = Console.ReadKey();
            //    Console.Clear();
            //    Console.WriteLine("0 - wall\n1 - ground\n2 - ground spear\n3 - ground ladder\n" +
            //    "4 - ground enemy\n5 - ground pit\n6 - ground treasure\n");
            //    switch (cki.KeyChar.ToString())
            //    {
            //        case "1":
            //            Console.WriteLine(">1. Test Prim");
            //            Test_Prim_Algorithm();
            //            break;
            //        case "2":
            //            Console.WriteLine(">2. Test GrowingTree");

            //            break;
            //        case "3":
            //            Console.WriteLine(">3. Test Hunt&Kill");
            //            Test_HuntAndKill_Algorithm();
            //            break;
            //        case "4":
            //            Console.WriteLine(">4. Test Sidewinder");

            //            break;
            //        case "5":
            //            Console.WriteLine(">5. Test generated items");
            //            Test_GenItems();
            //            break;
            //        case "6":
            //            Console.WriteLine(">6. Analysis");
            //            Analysis(container);
            //            break;
            //        case "7":
            //            Console.WriteLine(">7. Save generated maps\n");
            //            SaveMap();
            //            break;
            //        case "8":
            //            exit = true;
            //            break;
            //    }
            //} while (exit == false);
            MainMenu();
        }
        //Menu
        //algoritmo skaicius: 1 primo 2 growing tree 3 hunt n kill 4 sidewinder
        public static void MainMenu()
        {
            ConsoleKeyInfo cki;
            bool exit = false;
            do
            {
                DisplayMenu(1);
                cki = Console.ReadKey();
                Console.Clear();
                switch (cki.KeyChar.ToString())
                {
                    case "1":
                        Console.WriteLine(">1. Prim's");
                        SecondMenu(1);
                        break;
                    case "2":
                        Console.WriteLine(">2. Growing Tree");
                        SecondMenu(2);
                        break;
                    case "3":
                        Console.WriteLine(">3. Hunt&Kill");
                        SecondMenu(3);
                        break;
                    case "4":
                        Console.WriteLine(">4. Sidewinder");
                        SecondMenu(4);
                        break;
                    case "5":
                        Console.WriteLine(">5. All algorithms");
                        SecondMenu(5);
                        break;
                    case "6":
                        exit = true;
                        break;
                }
            } while (exit == false);
        }
        public static void SecondMenu(int algorithm)
        {
            ConsoleKeyInfo cki;
            bool exit = false;
            do
            {
                DisplayMenu(2);
                cki = Console.ReadKey();
                Console.Clear();
                switch (cki.KeyChar.ToString())
                {
                    case "1":
                        Console.WriteLine(">1. Compare");
                        CompareMenu(algorithm);
                        break;
                    case "2":
                        Console.WriteLine(">2. Portray");
                        Portray(algorithm);
                        break;
                    case "3":
                        Console.WriteLine(">3. Save");
                        SaveAlgorithm(algorithm);
                        break;
                    case "4":
                        MainMenu();
                        break;
                    case "5":
                        exit = true;
                        break;
                }
            } while (exit == false);

        }
        public static void CompareMenu(int algorithm)
        {
            ConsoleKeyInfo cki;
            bool exit = false;
            do
            {
                DisplayMenu(3);
                cki = Console.ReadKey();
                Console.Clear();
                switch (cki.KeyChar.ToString())
                {
                    case "1":
                        Console.WriteLine(">1. Ram analysis");
                        RamTest(algorithm);
                        break;
                    case "2":
                        Console.WriteLine(">2. Time analysis");
                        TimeTest(algorithm);
                        break;
                    case "3":
                        Console.WriteLine(">3. analysis");
                        ComplexityTest(algorithm);
                        break;
                    case "4":
                        MainMenu();
                        break;
                    case "5":
                        exit = true;
                        break;
                }
            } while (exit == false);
        }

        //Funkcijos
        public static void Portray(int algorithm)
        {
            switch (algorithm)
            {
                //primo
                case 1:
                    MapClass mapArray = new MapClass(size);
                    Console.WriteLine("\n Double array generated with Prim's algorithm \n");
                    mapArray.generateMapPrim();
                    mapArray.print();
                    break;
                case 2:
                    Test_Growing_Tree_Algorithm();
                    break;
                case 3:
                    Test_HuntAndKill_Algorithm();
                    break;
                case 4:
                    Test_Sidewinder_Algorithm();
                    break;
                case 5:
                    MapClass mapArrayA1 = new MapClass(size);
                    Console.WriteLine("\n Double array generated with Prim's algorithm \n");
                    mapArrayA1.generateMapPrim();
                    mapArrayA1.print();
                    Console.WriteLine("\n\n");
                    Test_Growing_Tree_Algorithm();
                    Console.WriteLine("\n\n");
                    Test_HuntAndKill_Algorithm();
                    Console.WriteLine("\n\n");
                    Test_Sidewinder_Algorithm();
                    break;
            }
        }
        public static void TimeTest(int algorithm)
        {
            switch (algorithm)
            {
                //primo
                case 1:
                    container.isAll = false;
                    PrimTimeTest(container);
                    break;
                case 2:
                    container.isAll = false;
                    GrowingTreeTimeTest(container);
                    break;
                case 3:
                    container.isAll = false;
                    HuntKillTimeTest(container);
                    break;
                case 4:
                    container.isAll = false;
                    SidewinderTimeTest(container);
                    break;
                case 5:
                    Analysis(container);
                    break;
            }
        }
        public static void SaveAlgorithm(int algorithm)
        {
            switch (algorithm)
            {
                case 1:
                    SaveMapOption(1);
                    break;
                case 2:
                    SaveMapOption(2);
                    break;
                case 3:
                    SaveMapOption(3);
                    break;
                case 4:
                    SaveMapOption(4);
                    break;
                case 5:
                    SaveMapOption(5);
                    break;
            }
        }
        public static void RamTest(int algorithm)
        {
            switch (algorithm)
            {
                //primo
                case 1:
                    PrimRamTest(container);
                    break;
                case 2:
                    GrowingTreeRamTest(container);
                    break;
                case 3:
                    HuntKillRamTest(container);
                    break;
                case 4:
                    SidewinderRamTest(container);
                    break;
                case 5:
                    AnalysisRam(container);
                    break;
            }
        }
        public static void ComplexityTest(int algorithm)
        {
            switch (algorithm)
            {
                //primo
                case 1:

                    break;
                case 2:

                    break;
                case 3:

                    break;
                case 4:

                    break;
                case 5:

                    break;
            }
        }
        
        static public void DisplayMenu(int n)
        {
            switch(n)
            {
                case 1:
                    Console.WriteLine("Main Menu\n");
                    Console.WriteLine(" Choose an algorithm\n");
                    Console.WriteLine(">1. Prim's");
                    Console.WriteLine(">2. Growing Tree");
                    Console.WriteLine(">3. Hunt&Kill");
                    Console.WriteLine(">4. Sidewinder");
                    Console.WriteLine(">5. All algorithms");
                    Console.WriteLine(">6. Exit \n");
                    Console.Write(">");
                    break;
                case 2:
                    Console.WriteLine("\n Choose an option\n");
                    Console.WriteLine(">1. Compare");
                    Console.WriteLine(">2. Portray");
                    Console.WriteLine(">3. Save");
                    Console.WriteLine(">4. Main menu");
                    Console.WriteLine(">5. Exit \n");
                    Console.Write(">");
                    break;
                case 3:
                    Console.WriteLine("\n Compare by\n");
                    Console.WriteLine(">1. Ram");
                    Console.WriteLine(">2. Time");
                    Console.WriteLine(">3. Complexity");
                    Console.WriteLine(">4. Main Menu");
                    Console.WriteLine(">5. Exit \n");
                    Console.Write(">");
                    break;
            }

        }
        //----------------------------------------------------------------------------------------------------------




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
        public static void Test_Growing_Tree_Algorithm()
        {
            char[][] mapArray = new char[size][];
            Console.WriteLine("\n Double array generated with Growing Tree algorithm \n");
            mapArray = GrowingTree.GenerateMap();
            GrowingTree.print(mapArray);
        }
        public static void Test_Sidewinder_Algorithm()
        {
            char[][] mapArray = new char[size][];
            Console.WriteLine("\n Double array generated with Sidewinder algorithm \n");
            mapArray = Sidewinder.GenerateMap();
            Sidewinder.print(mapArray);
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


        public static void PrimTimeTest(MapsContainer container)
        {
            int size = TimeTestStart();
            if (size == -1)
            {
                return;
            }
            Process proc = Process.GetCurrentProcess();
            availableMemory = 1800000000;// (new Microsoft.VisualBasic.Devices.ComputerInfo().AvailablePhysicalMemory) * 0.7;
            double memory = 0;
            Console.WriteLine("\n Prim's algorithm \n\n N        Runtime\n");

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
                Console.WriteLine(" {0,-5}    {1}", generatedMapNumber * j, myTimer.Elapsed);
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
        }
        public static void HuntKillTimeTest(MapsContainer container)
        {
            int size = TimeTestStart();
            if (size == -1)
            {
                return;
            }
            Process proc = Process.GetCurrentProcess();
            availableMemory = 1800000000;// (new Microsoft.VisualBasic.Devices.ComputerInfo().AvailablePhysicalMemory) * 0.7;
            double memory = 0;

            Stopwatch myTimer = new Stopwatch();
            Stopwatch allTime = new Stopwatch();

            allTime.Start();
            lastMemory = proc.PrivateMemorySize64 / 1000;
            Console.WriteLine("\n Hunt and kill algorithm \n\n N       Runtime\n");
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
                Console.WriteLine(" {0, -5}    {1}", generatedMapNumber * j, myTimer.Elapsed);
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
        public static void GrowingTreeTimeTest(MapsContainer container)
        {
            int size = TimeTestStart();
            if (size == -1)
            {
                return;
            }
            Process proc = Process.GetCurrentProcess();
            availableMemory = 1800000000;// (new Microsoft.VisualBasic.Devices.ComputerInfo().AvailablePhysicalMemory) * 0.7;
            double memory = 0;

            Stopwatch myTimer = new Stopwatch();
            Stopwatch allTime = new Stopwatch();

            allTime.Start();
            lastMemory = proc.PrivateMemorySize64 / 1000;
            Console.WriteLine("\n GrowingTree algorithm \n\n N       Runtime\n");
            for (int j = 1; j <= testTimes; j++)
            {
                myTimer.Start();
                for (int i = 0; i < generatedMapNumber * j; i++)
                {
                    MapClass mapArray = new MapClass(size);
                    mapArray.GrowingTreeArray = GrowingTree.GenerateMap();
                    container.addMap(mapArray, 2);
                }
                myTimer.Stop();
                proc.Refresh();
                memory = (proc.PrivateMemorySize64 / 1000) - lastMemory;
                Console.WriteLine(" {0,-5}    {1}", generatedMapNumber * j, myTimer.Elapsed);
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
        }
        public static void SidewinderTimeTest(MapsContainer container)
        {
            int size = TimeTestStart();
            if (size == -1)
            {
                return;
            }
            Process proc = Process.GetCurrentProcess();
            availableMemory = 1800000000;// (new Microsoft.VisualBasic.Devices.ComputerInfo().AvailablePhysicalMemory) * 0.7;
            double memory = 0;

            Stopwatch myTimer = new Stopwatch();
            Stopwatch allTime = new Stopwatch();

            allTime.Start();
            lastMemory = proc.PrivateMemorySize64 / 1000;
            Console.WriteLine("\n Sidewinder algorithm \n\n N       Runtime\n");
            for (int j = 1; j <= testTimes; j++)
            {
                myTimer.Start();
                for (int i = 0; i < generatedMapNumber * j; i++)
                {
                    MapClass mapArray = new MapClass(size);
                    mapArray.SidewinderArray = Sidewinder.GenerateMap();
                    container.addMap(mapArray, 4);
                }
                myTimer.Stop();
                proc.Refresh();
                memory = (proc.PrivateMemorySize64 / 1000) - lastMemory;
                Console.WriteLine(" {0,-5}    {1}", generatedMapNumber * j, myTimer.Elapsed);
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
        }
        public static int TimeTestStart()
        {
            if (container.isAll)
            {
                return container.size;
            }
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
                    return -1;
                }
            }

            while (size < 5)
            {
                Console.WriteLine("\nError: Too small map size: {0}", size);
                Console.WriteLine("Enter valid integer number for map size.");
                Console.WriteLine("Enter \"exit\" to go back to the main menu.");
                Console.Write("\n >> ");
                consoleSize = Console.ReadLine();
                if (consoleSize.ToLower().Trim() == "exit")
                {
                    return -1;
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
                        return -1;
                    }
                }
            }
            return size;
        }
        public static void Analysis(MapsContainer container)
        {
            container.size = TimeTestStart();
            container.isAll = true;
            PrimTimeTest(container);
            GrowingTreeTimeTest(container);
            HuntKillTimeTest(container);
            SidewinderTimeTest(container);
        }


        public static void PrimRamTest(MapsContainer container)
        {
            int size = TimeTestStart();
            if (size == -1)
            {
                return;
            }
            Process proc = Process.GetCurrentProcess();
            availableMemory = 1800000000;// (new Microsoft.VisualBasic.Devices.ComputerInfo().AvailablePhysicalMemory) * 0.7;
            double memory = 0;
            Console.WriteLine("\n Prim's algorithm \n\n N        RAM\n");

            Stopwatch allTime = new Stopwatch();

            allTime.Start();
            lastMemory = proc.PrivateMemorySize64 / 1000;
            for (int j = 1; j <= testTimes; j++)
            {
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
                proc.Refresh();
                memory = (proc.PrivateMemorySize64 / 1000) - lastMemory;
                Console.WriteLine(" {0,-5}    {1}B", generatedMapNumber * j,  memory);
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
        }
        public static void GrowingTreeRamTest(MapsContainer container)
        {
            int size = TimeTestStart();
            if (size == -1)
            {
                return;
            }
            Process proc = Process.GetCurrentProcess();
            availableMemory = 1800000000;// (new Microsoft.VisualBasic.Devices.ComputerInfo().AvailablePhysicalMemory) * 0.7;
            double memory = 0;
            Console.WriteLine("\n GrowingTree algorithm \n\n N        RAM\n");

            Stopwatch allTime = new Stopwatch();

            allTime.Start();
            lastMemory = proc.PrivateMemorySize64 / 1000;
            for (int j = 1; j <= testTimes; j++)
            {
                for (int i = 0; i < generatedMapNumber * j; i++)
                {
                    MapClass mapArray = new MapClass(size);
                    mapArray.GrowingTreeArray = GrowingTree.GenerateMap();
                    container.addMap(mapArray, 2);
                    if (i % 10 == 0)
                    {
                        if (proc.PrivateMemorySize64 >= availableMemory)
                        {
                            Console.WriteLine("\nError: Program used too much RAM: {0}MB {1}MB", proc.PrivateMemorySize64 / 1000000, availableMemory / 1000000);
                            return;
                        }
                    }
                }
                proc.Refresh();
                memory = (proc.PrivateMemorySize64 / 1000) - lastMemory;
                Console.WriteLine(" {0,-5}    {1}B", generatedMapNumber * j, memory);
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
        }
        public static void HuntKillRamTest(MapsContainer container)
        {
            int size = TimeTestStart();
            if (size == -1)
            {
                return;
            }
            Process proc = Process.GetCurrentProcess();
            availableMemory = 1800000000;// (new Microsoft.VisualBasic.Devices.ComputerInfo().AvailablePhysicalMemory) * 0.7;
            double memory = 0;
            Console.WriteLine("\n Hunt and kill algorithm \n\n N        RAM\n");

            Stopwatch allTime = new Stopwatch();

            allTime.Start();
            lastMemory = proc.PrivateMemorySize64 / 1000;
            for (int j = 1; j <= testTimes; j++)
            {
                for (int i = 0; i < generatedMapNumber * j; i++)
                {
                    MapClass mapArray = new MapClass(size);
                    mapArray.huntKillArray = HuntAndKill.GenerateMap();
                    container.addMap(mapArray, 3);
                    if (i % 10 == 0)
                    {
                        if (proc.PrivateMemorySize64 >= availableMemory)
                        {
                            Console.WriteLine("\nError: Program used too much RAM: {0}MB {1}MB", proc.PrivateMemorySize64 / 1000000, availableMemory / 1000000);
                            return;
                        }
                    }
                }
                proc.Refresh();
                memory = (proc.PrivateMemorySize64 / 1000) - lastMemory;
                Console.WriteLine(" {0,-5}    {1}B", generatedMapNumber * j, memory);
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
        }
        public static void SidewinderRamTest(MapsContainer container)
        {
            int size = TimeTestStart();
            if (size == -1)
            {
                return;
            }
            Process proc = Process.GetCurrentProcess();
            availableMemory = 1800000000;// (new Microsoft.VisualBasic.Devices.ComputerInfo().AvailablePhysicalMemory) * 0.7;
            double memory = 0;
            Console.WriteLine("\n Sidewinder algorithm \n\n N        RAM\n");

            Stopwatch allTime = new Stopwatch();

            allTime.Start();
            lastMemory = proc.PrivateMemorySize64 / 1000;
            for (int j = 1; j <= testTimes; j++)
            {
                for (int i = 0; i < generatedMapNumber * j; i++)
                {
                    MapClass mapArray = new MapClass(size);
                    mapArray.SidewinderArray = Sidewinder.GenerateMap();
                    container.addMap(mapArray, 4);
                    if (i % 10 == 0)
                    {
                        if (proc.PrivateMemorySize64 >= availableMemory)
                        {
                            Console.WriteLine("\nError: Program used too much RAM: {0}MB {1}MB", proc.PrivateMemorySize64 / 1000000, availableMemory / 1000000);
                            return;
                        }
                    }
                }
                proc.Refresh();
                memory = (proc.PrivateMemorySize64 / 1000) - lastMemory;
                Console.WriteLine(" {0,-5}    {1}B", generatedMapNumber * j, memory);
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
        }
        public static void AnalysisRam(MapsContainer container)
        {
            container.size = TimeTestStart();
            container.isAll = true;
            PrimRamTest(container);
            GrowingTreeRamTest(container);
            HuntKillRamTest(container);
            SidewinderRamTest(container);
        }

        public static void SaveMapOption(int algorithm)
        {
            for (int i = 0; i < savedMapsNumber; i++)
            {
                char[][] mapArray = new char[size][];

                switch (algorithm)
                {
                    case 1:
                        MapClass primas = new MapClass(size);
                        primas.generateMapPrim();
                        mapArray = primas.PrimArray;
                        GenerateItems(mapArray);
                        break;
                    case 2:
                        mapArray = GrowingTree.GenerateMap();
                        GenerateItemsAll(mapArray, GrowingTree.mapSize, GrowingTree.xs, GrowingTree.ys );
                        break;
                    case 3:
                        mapArray = HuntAndKill.GenerateMap();
                        GenerateItemsAll(mapArray, HuntAndKill.mapSize, HuntAndKill.StartingPoint.x, HuntAndKill.StartingPoint.y);
                        break;
                    case 4:
                        mapArray = Sidewinder.GenerateMap();
                        GenerateItemsAll(mapArray, Sidewinder.mapSize, Sidewinder.xs, Sidewinder.ys);
                        break;
                    case 5:
                        if ((savedMapsNumber / 4) > i)
                        {
                            MapClass primasA1 = new MapClass(size);
                            primasA1.generateMapPrim();
                            mapArray = primasA1.PrimArray;
                            GenerateItems(mapArray);
                        }
                        else if (((savedMapsNumber / 4) < i)&&((savedMapsNumber / 2) > i))
                        {
                            mapArray = GrowingTree.GenerateMap();
                            GenerateItemsAll(mapArray, GrowingTree.mapSize, GrowingTree.xs, GrowingTree.ys );
                        }
                        else if (((savedMapsNumber / 2) <= i) && ((savedMapsNumber / 4 * 3) > i))
                        {
                            mapArray = HuntAndKill.GenerateMap();
                            GenerateItemsAll(mapArray, HuntAndKill.mapSize, HuntAndKill.StartingPoint.x, HuntAndKill.StartingPoint.y);
                        }
                        else
                        {
                            mapArray = Sidewinder.GenerateMap();
                            GenerateItemsAll(mapArray, Sidewinder.mapSize, Sidewinder.xs, Sidewinder.ys);
                        }
                        break;
                }
                //GenerateItems(mapArray);

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
                FileStream file = new FileStream("maps\\" + i + ";" + i + ".json", FileMode.OpenOrCreate);
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
        public static void GenerateItemsAll(char[][] mapArray, int MapSize, int StartingPointX, int StartingPointY)
        {

            List<TilePoint> validPos = new List<TilePoint>();

            for (int x = 1; x < size - 1; x++)
            {
                for (int y = 1; y < MapSize - 1; y++)
                {
                    if (StartingPointX == x && StartingPointY == y)
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