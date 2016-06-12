using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TgmTasHelper
{
    public static class Extensions
    {
        public static PointF GetCenter(this RectangleF rect)
        {
            return new PointF(GetCenterX(rect), GetCenterY(rect));
        }

        public static float GetCenterX(this RectangleF rect)
        {
            return rect.X + 0.5f * rect.Width;
        }

        public static float GetCenterY(this RectangleF rect)
        {
            return rect.Y + 0.5f * rect.Height;
        }

        public static void FillRectangle(this Graphics g, Brush brush, Vec2F center, Vec2F size)
        {
            g.FillRectangle(brush, center.x - 0.5f * size.x, center.y - 0.5f * size.y, size.x, size.y);
        }

        public static IEnumerable<T> Append<T>(this IEnumerable<T> e, T value)
        {
            foreach (var cur in e)
            {
                yield return cur;
            }
            yield return value;
        }

        public static void RemoveLast<T>(this IList<T> l)
        {
            l.RemoveAt(l.Count - 1);
        }

        public static void Invoke(this Control control, Action action)
        {
            control.Invoke((Delegate)action);
        }
    }
}
