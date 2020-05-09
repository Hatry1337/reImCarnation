using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shmak
{
    class OldShit
    {
        [DllImport("user32.dll")]
        static extern void mouse_event(uint dwFlags, int dx, int dy, uint dwData,
        int dwExtraInfo);

        [Flags]
        public enum MouseEventFlags : uint
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

        private void draw_rect_filled(int width, int height, int step)
        {
            Point st_pos = Cursor.Position;
            draw_horiz_line(width);
            draw_vert_line(height);
            mouse_event((int)(MouseEventFlags.MOVE), 0, height, 0, 0);
            mouse_event((int)(MouseEventFlags.MOVE), width, 0, 0, 0);
            draw_horiz_line(0 - width);
            draw_vert_line(0 - height);
            Cursor.Position = st_pos;

            for (int i = 0; i < height / step; i++)
            {
                Cursor.Position = st_pos;
                mouse_event((int)(MouseEventFlags.MOVE), 0, step * i, 0, 0);

                mouse_event((int)(MouseEventFlags.LEFTDOWN), 0, 0, 0, 0);
                mouse_event((int)(MouseEventFlags.MOVE), width, 0, 0, 0);
                mouse_event((int)(MouseEventFlags.LEFTUP), 0, 0, 0, 0);
            }

        }

        private void draw_vert_line(int width)
        {
            Point st_pos = Cursor.Position;
            mouse_event((int)(MouseEventFlags.LEFTDOWN), 0, 0, 0, 0);
            mouse_event((int)(MouseEventFlags.MOVE), 0, width, 0, 0);
            mouse_event((int)(MouseEventFlags.LEFTUP), 0, 0, 0, 0);
            Cursor.Position = st_pos;
        }
        private void draw_horiz_line(int width)
        {
            Point st_pos = Cursor.Position;
            mouse_event((int)(MouseEventFlags.LEFTDOWN), 0, 0, 0, 0);
            mouse_event((int)(MouseEventFlags.MOVE), width, 0, 0, 0);
            mouse_event((int)(MouseEventFlags.LEFTUP), 0, 0, 0, 0);
            Cursor.Position = st_pos;
        }

        private void draw_rect(int width, int height)
        {

            mouse_event((int)(MouseEventFlags.LEFTDOWN), 0, 0, 0, 0);
            mouse_event((int)(MouseEventFlags.MOVE), width, 0, 0, 0);
            mouse_event((int)(MouseEventFlags.LEFTUP), 0, 0, 0, 0);
            mouse_event((int)(MouseEventFlags.LEFTDOWN), 0, 0, 0, 0);
            mouse_event((int)(MouseEventFlags.MOVE), 0, height, 0, 0);
            mouse_event((int)(MouseEventFlags.LEFTUP), 0, 0, 0, 0);
            mouse_event((int)(MouseEventFlags.LEFTDOWN), 0, 0, 0, 0);
            mouse_event((int)(MouseEventFlags.MOVE), 0 - width, 0, 0, 0);
            mouse_event((int)(MouseEventFlags.LEFTUP), 0, 0, 0, 0);
            mouse_event((int)(MouseEventFlags.LEFTDOWN), 0, 0, 0, 0);
            mouse_event((int)(MouseEventFlags.MOVE), 0, 0 - height, 0, 0);
            mouse_event((int)(MouseEventFlags.LEFTUP), 0, 0, 0, 0);
            mouse_event((int)(MouseEventFlags.LEFTDOWN), 0, 0, 0, 0);
            mouse_event((int)(MouseEventFlags.MOVE), width, 0, 0, 0);
            mouse_event((int)(MouseEventFlags.LEFTUP), 0, 0, 0, 0);
        }

        private void draw_rect_dots(int width, int height, int step)
        {
            for (int i = 0; i < width; i++)
            {
                mouse_event((int)(MouseEventFlags.MOVE), step, 0, 0, 0);
                mouse_event((int)(MouseEventFlags.LEFTDOWN), 0, 0, 0, 0);
                mouse_event((int)(MouseEventFlags.LEFTUP), 0, 0, 0, 0);
            }
            for (int i = 0; i < height; i++)
            {
                mouse_event((int)(MouseEventFlags.MOVE), 0, step, 0, 0);
                mouse_event((int)(MouseEventFlags.LEFTDOWN), 0, 0, 0, 0);
                mouse_event((int)(MouseEventFlags.LEFTUP), 0, 0, 0, 0);
            }
            for (int i = 0; i < width; i++)
            {
                mouse_event((int)(MouseEventFlags.MOVE), 0 - step, 0, 0, 0);
                mouse_event((int)(MouseEventFlags.LEFTDOWN), 0, 0, 0, 0);
                mouse_event((int)(MouseEventFlags.LEFTUP), 0, 0, 0, 0);
            }
            for (int i = 0; i < height; i++)
            {
                mouse_event((int)(MouseEventFlags.MOVE), 0, 0 - step, 0, 0);
                mouse_event((int)(MouseEventFlags.LEFTDOWN), 0, 0, 0, 0);
                mouse_event((int)(MouseEventFlags.LEFTUP), 0, 0, 0, 0);
            }
        }
    }
}
