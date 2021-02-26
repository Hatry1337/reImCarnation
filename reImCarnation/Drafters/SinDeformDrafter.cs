using reImCarnation.Forms;
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
    class SinDeformDrafter : IDrafter
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

            Metrics metrics = new Metrics("SinDeform");

            List<List<bool>> img_c = new List<List<bool>>();

            for (int i = 0; i < img.Width; i++)
            {
                img_c.Add(new List<bool>());
                for (int j = 0; j < img.Height; j++)
                {
                    img_c[i].Add(false);
                }
            }

            int rx;
            for (int x = 0; x < img.Width; x++)
            {
                for (int y = 0; y < img.Height; y++)
                {
                    Color clr = img.GetPixel(x, y);
                    if (((clr.R + clr.G + clr.B) / 3) < Settings.Default.sensitivity)
                    {
                        rx = (int)Math.Round(x+Math.Sin(y/10)*8);
                        if (rx < img.Width && rx >= 0)
                        {
                            img_c[rx][y] = true;
                        }
                        metrics.TotalPixels++;
                    }
                }
            }

            Point st_pos = Cursor.Position;

            OptimizedDrafter.DrawArray(img_c, st_pos, 1, metrics);

            metrics.EndTime = DateTime.Now;
            if (Settings.Default.metrics)
            {
                Application.Run(new MetricsForm(metrics));
            }
        }
    }
}
