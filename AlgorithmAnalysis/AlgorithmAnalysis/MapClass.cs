using System;

namespace AlgorithmAnalysis
{
    class MapClass
    {
        public int[,] data;

        //Integer number means generated algorithm number (1-Prim's; 2-RecursiveBacktracing, etc.)
        public int algorithm;

        public MapClass(int size)
        {
            data = new int[size, size];
        }
        public int getData(int i, int j)
        {
            return data[i, j];
        }
        public void setData(int i, int j, int field)
        {
            data[i, j] = field;
        }
        /// <summary>
        /// Prints map double array and size
        /// </summary>
        public void print()
        {
            Console.WriteLine(" size: {0}x{0}\n", data.GetLength(0));
            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    Console.Write(" {0} ", data[i, j]);
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
            //Generate double array with prim algorithm
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
    }
}
