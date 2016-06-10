using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TgmTasHelper.Simulation
{
    [DataContract]
    public enum TetrominoType
    {
        [EnumMember]
        Empty = -1,

        [EnumMember]
        I = 0,
        [EnumMember]
        Z = 1,
        [EnumMember]
        S = 2,
        [EnumMember]
        J = 3,
        [EnumMember]
        L = 4,
        [EnumMember]
        O = 5,
        [EnumMember]
        T = 6,

        OutOfBounds = 99,
    }
}
