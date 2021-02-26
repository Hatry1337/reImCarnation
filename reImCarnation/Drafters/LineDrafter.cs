using reImCarnation.Forms;
using reImCarnation.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace reImCarnation.Drafters
{
    public class LineDrafter : IDrafter
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
            new Thread(() => Application.Run(new IndicatorForm(img.Width, img.Height))).Start();
            Thread.Sleep(5000);

            Metrics metrics = new Metrics("LineDrafter");

            Point st_pos = Cursor.Position;
            List<Point> pixels = new List<Point>();

            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    Color clr = img.GetPixel(x, y);
                    if (((clr.R + clr.G + clr.B) / 3) < Settings.Default.sensitivity)
                    {
                        pixels.Add(new Point(x, y));
                        metrics.TotalPixels++;
                    }
                }
            }

            for (int i = 0; i < pixels.Count; i++)
            {
                Point px = pixels[i];
                Cursor.Position = new Point(st_pos.X + px.X, st_pos.Y + px.Y);
                metrics.MouseClicks++;
                mouse_event((int)(MouseEventFlags.LEFTDOWN), 0, 0, 0, 0);
                mouse_event((int)(MouseEventFlags.LEFTUP), 0, 0, 0, 0);
                metrics.PixelsDrafted++;
                Thread.Sleep(Settings.Default.wait_delay);
            }
            metrics.EndTime = DateTime.Now;
            if (Settings.Default.metrics)
            {
                Application.Run(new MetricsForm(metrics));
            }
        }
    }
}
