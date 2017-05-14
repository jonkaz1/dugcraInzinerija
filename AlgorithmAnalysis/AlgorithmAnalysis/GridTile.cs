using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmAnalysis
{
    [DataContract]
    public class GridTile
    {
        
        public enum TileTypes { Ground, Wall, Empty, Fog }
        [DataMember(Order = 0)]
        public TileTypes type;
        public enum ContainedObject { Empty, Ladder, Pit, Spear, Enemy, Chest, StartingPoint }
        [DataMember(Order = 1)]
        public ContainedObject containedObject = ContainedObject.Empty;
    }
}
