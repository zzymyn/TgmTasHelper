using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TgmTasHelper.Simulation
{
    [DataContract]
    public enum Movement : byte
    {
        [EnumMember]
        None,
        [EnumMember]
        Left,
        [EnumMember]
        Right,
        [EnumMember]
        Down,
    }

    public static class MovementExtensions
    {
        public static bool IsLeftRight(this Movement move)
        {
            return move == Movement.Left || move == Movement.Right;
        }
    }
}
