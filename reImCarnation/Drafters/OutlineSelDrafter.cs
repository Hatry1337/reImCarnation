using reImCarnation.Drafters;
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

namespace reImCarnation
{
    public class OutlineSelDrafter : IDrafter
    {


        public void Draw(Bitmap img)
        {
            new Thread(() => Application.Run(new IndicatorForm(img.Width, img.Height))).Start();
            Thread.Sleep(5000);

            Metrics metrics = new Metrics("OutlineSel");

            var kernel = new double[,]
                 {{0.1, 0.1, 0.1},
                  {0.1, 0.1, 0.1},
                  {0.1, 0.1, 0.1}};
            img = Rgb.RgbToBitmapQ(Rgb.Convolution(Rgb.BitmapToByteRgbQ(img), kernel));

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
                    if (((clr.R + clr.G + clr.B) / 3) < Settings.Default.sensitivity)
                    {
                        img_c[x][y] = true;
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
