using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgmTasHelper
{
    [DebuggerDisplay("({x}, {y})")]
    public struct Vec2F
    {
        public float x;
        public float y;

        public Vec2F(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vec2F operator +(Vec2F a, Vec2F b)
        {
            return new Vec2F(a.x + b.x, a.y + b.y);
        }

        public static Vec2F operator *(float a, Vec2F b)
        {
            return new Vec2F(a * b.x, a * b.y);
        }

        public static Vec2F operator *(Vec2F b, float a)
        {
            return new Vec2F(a * b.x, a * b.y);
        }

        public static Vec2F operator +(float a, Vec2F b)
        {
            return new Vec2F(a + b.x, a + b.y);
        }

        public static Vec2F operator +(Vec2F b, float a)
        {
            return new Vec2F(a + b.x, a + b.y);
        }

        public static Vec2F operator -(Vec2F a, Vec2F b)
        {
            return new Vec2F(a.x - b.x, a.y - b.y);
        }

        public static implicit operator PointF(Vec2F v)
        {
            return new PointF(v.x, v.y);
        }
    }
}
