using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgmTasHelper.Simulation
{
    public class Tetromino
    {
        public TetrominoType TetrominoType { get; private set; }
        public Vec2 Position { get; private set; }
        public int Rotation { get; private set; }

        public Tetromino(TetrominoType tetrominoType, Vec2 position, int rotation)
        {
            TetrominoType = tetrominoType;
            Position = position;
            Rotation = rotation;
        }

        public Vec2[] GetPoints()
        {
            var points = RotationSystem.GetTetrominoPoints(TetrominoType, Rotation);
            var r = new Vec2[points.Length];
            for (int i = 0; i < points.Length; ++i)
            {
                r[i] = points[i] + Position;
            }
            return r;
        }
    }
}
