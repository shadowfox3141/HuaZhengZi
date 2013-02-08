using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HuaZhengZi
{
    public static class ExtensionMethods
    {
        public static bool IsInside(this Point point, Rect refRect) {
            if (point.X < refRect.X || point.Y < refRect.Y ||
                point.X > (refRect.X + refRect.Width) || point.Y > (refRect.Y + refRect.Height)) {
                return false;
            } else {
                return true;
            }
        }
    }
}
