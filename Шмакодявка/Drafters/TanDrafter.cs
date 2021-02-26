using reImCarnation.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace reImCarnation.Drafters
{
    public class TanDrafter : IDrafter
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


        public void Draw(Bitmap img)
        {
            Thread.Sleep(5000);
            Point spos = new Point(Cursor.Position.X, Cursor.Position.Y);

            mouse_event((int)(MouseEventFlags.LEFTDOWN), 0, 0, 0, 0);
            for (int i = 0; i < 150; i++)
            {
                Cursor.Position = new Point(Cursor.Position.X + 10, Cursor.Position.Y);
                Thread.Sleep(Settings.Default.wait_delay * 2);
            }
            mouse_event((int)(MouseEventFlags.LEFTUP), 0, 0, 0, 0);

            mouse_event((int)(MouseEventFlags.LEFTDOWN), 0, 0, 0, 0);
            for (double i = 0; i < 1000; i+=0.5)
            {
                Cursor.Position = new Point(spos.X + (int)(i), (int)(spos.Y + Math.Tan(i/128)*5));
                Thread.Sleep(Settings.Default.wait_delay);
            }
            mouse_event((int)(MouseEventFlags.LEFTUP), 0, 0, 0, 0);            
        }
    }
}
