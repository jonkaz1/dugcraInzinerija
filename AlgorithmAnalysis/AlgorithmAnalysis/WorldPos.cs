using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmAnalysis
{
    [DataContract]
    public class WorldPos
    {
        [DataMember]
        public int x, y;

        public WorldPos(int x = 0, int y = 0)
        {
            this.x = x;
            this.y = y;
        }
    }
}
