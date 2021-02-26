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
    public class SinDrafter : IDrafter
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
            for (int i = 0; i < 100; i++)
            {
                Cursor.Position = new Point(Cursor.Position.X + 10, Cursor.Position.Y);
                Thread.Sleep(Settings.Default.wait_delay * 2);
            }
            mouse_event((int)(MouseEventFlags.LEFTUP), 0, 0, 0, 0);

            int uMin = 0, uMax;



            double degrees = 25;
            double vel = 100;
            double angle = Math.PI * degrees / 180.0;

            Cursor.Position = new Point(spos.X, spos.Y);
            for (double i = 0; i < 10000; i += 1)
            {
                //Math.Sin(i/128)*70)
                double ypos = (i*Math.Tan(angle) - 9.81 * Math.Pow(i, 2)) / (2*Math.Pow(vel, 2)*Math.Pow(Math.Cos(angle), 2));
                mouse_event((int)(MouseEventFlags.LEFTDOWN), 0, 0, 0, 0);
                Cursor.Position = new Point((int)(spos.X + i/20), (int)(spos.Y + ypos/20));
                mouse_event((int)(MouseEventFlags.LEFTUP), 0, 0, 0, 0);

                Thread.Sleep(Settings.Default.wait_delay);
            }
        }
    }
}
