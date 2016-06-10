using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TgmTasHelper.Simulation
{
    public interface IGameState
    {
        TetrominoType NextTetromino { get; }
        IGameRules GameRules { get; }
        IRng Rng { get; }
        IBoard Board { get; }
        int Time { get; }
        string TimeString { get; }
        int Level { get; }

        IGameState Next(ITetromino tetromino, List<Input> inputs);
    }
}
