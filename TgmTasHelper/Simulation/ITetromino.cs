using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgmTasHelper.Simulation
{
    public interface ITetromino
    {
        TetrominoType Type { get; }
        Vec2 Pos { get; }
        int Angle { get; }
    }
}
