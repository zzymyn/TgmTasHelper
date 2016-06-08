using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgmTasHelper.Simulation
{
    /// <summary>
    /// Two tetrominos are equal if they have the same type and the same points for a given rotation system.
    /// </summary>
    public class TetrominoComparer : IEqualityComparer<ITetromino>
    {
        private IGameRules m_GameRules;

        public TetrominoComparer(IGameRules gameRules)
        {
            m_GameRules = gameRules;
        }

        public bool Equals(ITetromino x, ITetromino y)
        {
            if (x == null || y == null)
                return x == null && y == null;

            return x.Type == y.Type
                && x.Locked == y.Locked
                && Enumerable.SequenceEqual(m_GameRules.GetTetrominoPoints(x), m_GameRules.GetTetrominoPoints(y));
        }

        public int GetHashCode(ITetromino obj)
        {
            int h = 17;
            h = h * 31 + obj.Type.GetHashCode();
            h = h * 31 + obj.Locked.GetHashCode();
            foreach (var p in m_GameRules.GetTetrominoPoints(obj))
                h = h * 31 + p.GetHashCode();
            return h;
        }
    }
}
