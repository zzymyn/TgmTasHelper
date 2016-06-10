using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TgmTasHelper.Simulation
{
    [DataContract]
    public class Tetromino : ITetromino
    {
        [DataMember]
        public TetrominoType Type { get; set; }
        [DataMember]
        public Vec2 Pos { get; set; }
        [DataMember]
        public int Angle { get; set; }
        [DataMember]
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
