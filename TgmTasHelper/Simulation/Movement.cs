using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgmTasHelper.Simulation
{
    public enum Movement
    {
        None,
        Left,
        Right,
    }

    public static class MovementExtensions
    {
        public static bool IsLeftRight(this Movement move)
        {
            return move == Movement.Left || move == Movement.Right;
        }
    }
}
