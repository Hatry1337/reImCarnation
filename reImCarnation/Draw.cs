using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace reImCarnation
{
    public static class Draw
    {
        [DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, int dx, int dy, uint dwData, int dwExtraInfo);

        [Flags]
        private enum MouseEventFlags : uint
        {
            LEFTDOWN = 0x00000002,
            LEFTUP = 0x00000004,
            MIDDLEDOWN = 0x00000020,
            MIDDLEUP = 0x00000040,
            MOVE = 0x00000001,
            ABSOLUTE = 0x00008000,
            RIGHTDOWN = 0x00000008,
            RIGHTUP = 0x00000010,
            WHEEL = 0x00000800,
            XDOWN = 0x00000080,
            XUP = 0x00000100
        }

        public static void Point(Point pt)
        {
            mouse_event((int)(MouseEventFlags.LEFTUP), 0, 0, 0, 0);
            Cursor.Position = new Point(pt.X, pt.Y);
            mouse_event((int)(MouseEventFlags.LEFTDOWN), 0, 0, 0, 0);
            mouse_event((int)(MouseEventFlags.LEFTUP), 0, 0, 0, 0);
        }

        public static void Line(Point start, Point end)
        {
            mouse_event((int)(MouseEventFlags.LEFTUP), 0, 0, 0, 0);
            Cursor.Position = new Point(start.X, start.Y);
            mouse_event((int)(MouseEventFlags.LEFTDOWN), 0, 0, 0, 0);
            Cursor.Position = new Point(end.X, end.Y);
            mouse_event((int)(MouseEventFlags.LEFTUP), 0, 0, 0, 0);
        }

        public static void Rect(Point pt, Size sz)
        {
            mouse_event((int)(MouseEventFlags.LEFTUP), 0, 0, 0, 0);
            Cursor.Position = pt;
            mouse_event((int)(MouseEventFlags.LEFTDOWN), 0, 0, 0, 0);
            Cursor.Position = new Point(pt.X + sz.Width, pt.Y);
            Cursor.Position = new Point(pt.X + sz.Width, pt.Y + sz.Height);
            Cursor.Position = new Point(pt.X, pt.Y + sz.Height);
            Cursor.Position = new Point(pt.X, pt.Y);
            mouse_event((int)(MouseEventFlags.LEFTUP), 0, 0, 0, 0);
        }
    }
}
