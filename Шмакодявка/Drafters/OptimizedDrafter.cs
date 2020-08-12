using Shmak.Drafters;
using Shmak.Properties;
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

namespace Shmak
{
    public class OptimizedDrafter : IDrafter
    {
        public OptimizedDrafter(int mode)
        {
            Mode = mode;
        }
        public int Mode;

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

            Metrics metrics;
            switch (this.Mode)
            {
                case 1:
                    metrics = new Metrics("OptimizedDrafter Horizontal");
                    break;
                case 2:
                    metrics = new Metrics("OptimizedDrafter Vertical");
                    break;
                case 3:
                    metrics = new Metrics("OptimizedDrafter 2D");
                    break;
                case 4:
                    metrics = new Metrics("OptimizedDrafter 4D");
                    break;
                default:
                    metrics = new Metrics("OptimizedDrafter");
                    break;
            }
            

            List<List<bool>> img_c = new List<List<bool>>();

            for (int i = 0; i < img.Width;  i++)
            {
                img_c.Add(new List<bool>());
                for (int j = 0; j < img.Height; j++)
                {
                    img_c[i].Add(false);
                }
            }

            for (int x = 0; x < img.Width; x++)
            {
                for (int y = 0; y < img.Height; y++)
                {
                    Color clr = img.GetPixel(x, y);
                    if (clr.GetBrightness() < Settings.Default.sensitivity)
                    {
                        img_c[x][y] = true;
                        metrics.TotalPixels++;
                    }
                }
            }

            Point st_pos = Cursor.Position;

            while (!findPixel(img_c).IsEmpty)
            {
                Point px = findPixel(img_c);
                Cursor.Position = new Point(st_pos.X + px.X, st_pos.Y + px.Y);
                metrics.MouseClicks++;
                mouse_event((int)(MouseEventFlags.LEFTDOWN), 0, 0, 0, 0);
                img_c[px.X][px.Y] = false;
                while (toDrawExists(px, img_c, Mode) != 0)
                {
                    switch (toDrawExists(px, img_c, Mode))
                    {
                        case 1:
                            px.X++;
                            Cursor.Position = new Point(st_pos.X + px.X, st_pos.Y + px.Y);
                            img_c[px.X][px.Y] = false;
                            break;
                        case 2:
                            px.Y++;
                            Cursor.Position = new Point(st_pos.X + px.X, st_pos.Y + px.Y);
                            img_c[px.X][px.Y] = false;
                            break;
                        case 3:
                            px.X--;
                            Cursor.Position = new Point(st_pos.X + px.X, st_pos.Y + px.Y);
                            img_c[px.X][px.Y] = false;
                            break;
                        case 4:
                            px.Y--;
                            Cursor.Position = new Point(st_pos.X + px.X, st_pos.Y + px.Y);
                            img_c[px.X][px.Y] = false;
                            break;
                    }
                    metrics.PixelsDrafted++;
                }
                mouse_event((int)(MouseEventFlags.LEFTUP), 0, 0, 0, 0);
                Thread.Sleep(Settings.Default.wait_delay);
            }
            metrics.EndTime = DateTime.Now;
            if (Settings.Default.metrics)
            {
                Application.Run(new MetricsForm(metrics));
            }
        }

        private Point findPixel(List<List<bool>> img)
        {
            Point point = new Point();
            for (int x = 0; x < img.Count; x++)
            {
                for (int y = 0; y < img[0].Count; y++)
                {
                    if (img[x][y])
                    {
                        point.X = x;
                        point.Y = y;
                        break;
                    }
                }
                if (!point.IsEmpty)
                {
                    break;
                }
            }
            return point;
        }

        private int toDrawExists(Point px, List<List<bool>> img, int mode)
        {
            switch (mode)
            {
                case 1:
                    if (px.X < img.Count - 1)
                    {
                        if (img[px.X + 1][px.Y])
                        {
                            return 1;
                        }
                    }
                    break;
                case 2:
                    if (px.Y < img[0].Count - 1)
                    {
                        if (img[px.X][px.Y + 1])
                        {
                            return 2;
                        }
                    }
                    break;
                case 3:
                    if (px.X < img.Count - 1)
                    {
                        if (img[px.X + 1][px.Y])
                        {
                            return 1;
                        }
                    }
                    if (px.Y < img[0].Count - 1)
                    {
                        if (img[px.X][px.Y + 1])
                        {
                            return 2;
                        }
                    }
                    break;
                case 4:
                    if (px.X < img.Count - 1)
                    {
                        if (img[px.X + 1][px.Y])
                        {
                            return 1;
                        }
                    }
                    if (px.Y < img[0].Count - 1)
                    {
                        if (img[px.X][px.Y + 1])
                        {
                            return 2;
                        }
                    }
                    if (px.X >= 1)
                    {
                        if (img[px.X - 1][px.Y])
                        {
                            return 3;
                        }
                    }
                    if (px.Y >= 1)
                    {
                        if (img[px.X][px.Y - 1])
                        {
                            return 4;
                        }
                    }
                    break;
            }
            return 0;
        }
    }
}
