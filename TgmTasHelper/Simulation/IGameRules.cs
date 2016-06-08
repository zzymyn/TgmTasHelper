using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgmTasHelper.Simulation
{
    public interface IGameRules
    {
        IEnumerable<Vec2> GetTetrominoPoints(ITetromino tetromino);
        IEnumerable<Vec2> GetTetrominoPoints(TetrominoType tetrominoType, Vec2 pos, int angle);
    }
}
