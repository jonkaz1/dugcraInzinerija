using System;
using System.Diagnostics;

namespace AlgorithmAnalysis
{
    class Program
    {
        public static int generatedMapNumber = 1000;    //Generated maps number
        public static int size = 17;                    //Map array size

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
                        Console.WriteLine(">3. Analysis");
                        Analysis(container);
                        break;
                    case "5":
                        exit = true;
                        break;
                }
            } while (exit == false);
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
            Console.WriteLine(">5. Exit \n");
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
            MapClass mapArray = new MapClass(size);
            Console.WriteLine("\n Double array generated with recursive backtracing algorithm \n");
            mapArray.generateMapRecursiveBacktracing();
            mapArray.print();


        }
        /// <summary>
        /// Hunt and kill algorithm test
        /// </summary>
        public static void Test_HuntAndKill_Algorithm()
        {
            MapClass mapArray = new MapClass(size);
            Console.WriteLine("\n Double array generated with Hunt&Kill algorithm \n");
            mapArray.generateMapHuntAndKill();
            mapArray.print();


        }


        /// <summary>
        /// Algorithm analysis (only generating time at this moment)
        /// </summary>
        /// <param name="container"></param>
        public static void Analysis(MapsContainer container)
        {
            Console.WriteLine("\n Prim's algorithm \n\n N       Runtime\n");
            Stopwatch myTimer = new Stopwatch();
            myTimer.Start();
            for (int i = 0; i < generatedMapNumber; i++)
            {
                MapClass mapArray = new MapClass(size);
                mapArray.generateMapPrim();
                container.addMap(mapArray, 1);
            }
            myTimer.Stop();
            Console.WriteLine(" {0}    {1}", generatedMapNumber, myTimer.Elapsed);
            GC.Collect();


            Console.WriteLine("\n Recursive backtracing algorithm \n\n N       Runtime\n");
            myTimer.Reset();
            myTimer.Start();
            for (int i = 0; i < generatedMapNumber; i++)
            {
                MapClass mapArray = new MapClass(size);
                mapArray.generateMapRecursiveBacktracing();
                container.addMap(mapArray, 2);
            }
            myTimer.Stop();
            Console.WriteLine(" {0}    {1}", generatedMapNumber, myTimer.Elapsed);
            GC.Collect();


            Console.WriteLine("\n Hunt and kill algorithm \n\n N       Runtime\n");
            myTimer.Reset();
            myTimer.Start();
            for (int i = 0; i < generatedMapNumber; i++)
            {
                MapClass mapArray = new MapClass(size);
                mapArray.generateMapHuntAndKill();
                container.addMap(mapArray, 3);
            }
            myTimer.Stop();
            Console.WriteLine(" {0}    {1}", generatedMapNumber, myTimer.Elapsed);
            GC.Collect();
        }
    }
}