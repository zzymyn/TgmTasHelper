using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgmTasHelper
{
    [DebuggerDisplay("({x}, {y})")]
    public struct Vec2 : IEquatable<Vec2>
    {
        public int x;
        public int y;

        public static Vec2 Zero = new Vec2(0, 0);

        public Vec2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vec2 operator +(Vec2 a, Vec2 b)
        {
            return new Vec2(a.x + b.x, a.y + b.y);
        }

        public static bool operator ==(Vec2 a, Vec2 b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Vec2 a, Vec2 b)
        {
            return !a.Equals(b);
        }

        public bool Equals(Vec2 other)
        {
            return x == other.x && y == other.y;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Vec2))
                return false;
            return Equals((Vec2)obj);
        }

        public override int GetHashCode()
        {
            int h = 17;
            h = h *= 31 + x.GetHashCode();
            h = h *= 31 + y.GetHashCode();
            return h;
        }
    }
}
