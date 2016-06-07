using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgmTasHelper.Simulation
{
    public interface IReadOnlyBoard : IEquatable<IReadOnlyBoard>
    {
        int Width { get; }
        int HeightVisible { get; }
        int HeightLogical { get; }

        IReadWriteBoard CreateCopy();

        void ForEachVisible(Action<int, int, TetrominoType> action);
        void ForEachLogical(Action<int, int, TetrominoType> action);

        TetrominoType GetVisible(int x, int y);
        TetrominoType GetLogical(int x, int y);

        Vec2 GetSpawnPos();
    }
}
