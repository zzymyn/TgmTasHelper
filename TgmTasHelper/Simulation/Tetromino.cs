using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgmTasHelper.Simulation
{
    public class Tetromino : ITetromino
    {
        public TetrominoType Type { get; set; }
        public Vec2 Pos { get; set; }
        public int Angle { get; set; }
        public bool Locked { get; set; }

        public Tetromino(ITetromino other)
        {
            Type = other.Type;
            Pos = other.Pos;
            Angle = other.Angle;
            Locked = other.Locked;
        }

        public Tetromino(TetrominoType type, Vec2 pos, int angle, bool locked)
        {
            Type = type;
            Pos = pos;
            Angle = angle;
            Locked = locked;
        }
    }
}
