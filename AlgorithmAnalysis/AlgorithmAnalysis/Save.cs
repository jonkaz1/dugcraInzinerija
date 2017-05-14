using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace AlgorithmAnalysis
{
    [DataContract]
    public class Save
    {
        [DataMember]
        public Tiles[] tiles;

        [DataContract]
        public struct Tiles
        {
            [DataMember(Order = 0)]
            public GridTile tile;
            [DataMember(Order = 1)]
            public WorldPos pos;
        }
    }
}
