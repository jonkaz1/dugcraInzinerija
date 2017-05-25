using System;
using System.Collections.Generic;

namespace AlgorithmAnalysis
{
    class MapsContainer
    {
        //Different algorithm lists
        List<MapClass> MapsList1;   //Prim algorithm
        List<MapClass> MapsList2;   //Growing tree algorithm
        List<MapClass> MapsList3;   //Hunt&Kill algorithm
        List<MapClass> MapsList4;   //Sidewinder algorithm
        public bool isAll;
        public int size;

        public MapsContainer()
        {
            MapsList1 = new List<MapClass>();
            MapsList2 = new List<MapClass>();
            MapsList3 = new List<MapClass>();
            MapsList4 = new List<MapClass>();
            isAll = false;
        }
        /// <summary>
        /// Get map by algorithm number and index
        /// </summary>
        /// <param name="algorithmNumber"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public MapClass getData(int algorithmNumber, int index)
        {
            switch (algorithmNumber)
            {
                case 1:
                    return MapsList1[index];
                case 2:
                    return MapsList2[index];
                case 3:
                    return MapsList3[index];
                case 4:
                    return MapsList4[index];
            }
            throw new ArgumentException("This algorithm does not exist.");
        }
        /// <summary>
        /// Get whole map list by algorithm number
        /// </summary>
        /// <param name="algorithmNumber"></param>
        /// <returns></returns>
        public List<MapClass> getMapList(int algorithmNumber)
        {
            switch (algorithmNumber)
            {
                case 1:
                    return MapsList1;
                case 2:
                    return MapsList2;
                case 3:
                    return MapsList3;
                case 4:
                    return MapsList4;
            }
            throw new ArgumentException("This algorithm does not exist.");
        }
        /// <summary>
        /// Add map to list by algorithm number
        /// </summary>
        /// <param name="map"></param>
        /// <param name="algorithmNumber"></param>
        public void addMap(MapClass map, int algorithmNumber)
        {
            switch (algorithmNumber)
            {
                case 1:
                    MapsList1.Add(map);
                    break;
                case 2:
                    MapsList2.Add(map);
                    break;
                case 3:
                    MapsList3.Add(map);
                    break;
                case 4:
                    MapsList4.Add(map);
                    break;
            }
        }
    }
}
