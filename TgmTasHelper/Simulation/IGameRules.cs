using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgmTasHelper.Simulation
{
    public interface IGameRules
    {
        IEnumerable<int> GetAllowedKicks(IBoard board, ITetromino tetromino, int targetAngle);

        IEnumerable<Vec2> GetTetrominoPoints(ITetromino tetromino);
        IEnumerable<Vec2> GetTetrominoPoints(TetrominoType tetrominoType, Vec2 pos, int angle);

        int GetNextTime(int time, int level, List<Input> inputs, int linesCleared);
        int GetNextLevel(int level, int linesCleared);
    }
}
