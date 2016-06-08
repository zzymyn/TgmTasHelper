using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgmTasHelper.Simulation
{
    public interface IBoard : IEquatable<IBoard>
    {
        IGameRules GameRules { get; }
        int Width { get; }
        int HeightVisible { get; }
        int HeightLogical { get; }

        void ForEachVisible(Action<int, int, TetrominoType> action);
        void ForEach(Action<int, int, TetrominoType> action);

        TetrominoType GetVisible(int x, int y);
        TetrominoType Get(int x, int y);

        Vec2 GetSpawnPos();

        IBoard LockTetromino(ITetromino tetromino, out int clearedLines);
    }
}
