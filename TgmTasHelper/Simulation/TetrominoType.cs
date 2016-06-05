using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgmTasHelper.Simulation
{
    public enum TetrominoType
    {
        Empty = -1,

        I = 0,
        Z = 1,
        S = 2,
        J = 3,
        L = 4,
        O = 5,
        T = 6,

        OutOfBounds = 99,
    }
}
