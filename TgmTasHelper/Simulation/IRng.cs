using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgmTasHelper.Simulation
{
    /// <summary>
    /// Represents the immutable state of the RNG.
    /// </summary>
    public interface IRng
    {
        IRng Next();
        IEnumerable<TetrominoType> Peek();
    }
}
