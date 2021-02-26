using reImCarnation.Forms;
using reImCarnation.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace reImCarnation.Drafters
{
    public class ChunkedDrafter : IDrafter
    {
        public ChunkedDrafter(int mode)
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

        private List<Point> ChunksPix;
        private Point ChunkCur;

        public void Collibrate(Bitmap img)
        {
            new Thread(() => Application.Run(new IndicatorForm(img.Width, img.Height))).Start();
            Thread.Sleep(5000);

            ChunkCur = Cursor.Position;
            ChunksPix = new List<Point>();

            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    Color clr = img.GetPixel(x, y);
                    if (((clr.R + clr.G + clr.B) / 3) < Settings.Default.sensitivity)
                    {
                        ChunksPix.Add(new Point(x, y));
                    }
                }
            }

            SystemSounds.Asterisk.Play();
        }

        public void Draw(Bitmap img)
        {
            int lng = Settings.Default.chunk_size;
            if (ChunksPix.Count < 500)
            {
                lng = ChunksPix.Count;
            }

            if(Mode == 1)
            {
                Random rand = new Random();
                for (int i = ChunksPix.Count - 1; i >= 1; i--)
                {
                    int j = rand.Next(i + 1);
                    var temp = ChunksPix[j];
                    ChunksPix[j] = ChunksPix[i];
                    ChunksPix[i] = temp;
                }
            }

            for (int i = 0; i < lng; i++)
            {
                Point px = ChunksPix[i];
                ChunksPix.RemoveAt(i);
                Cursor.Position = new Point(ChunkCur.X + px.X, ChunkCur.Y + px.Y);
                mouse_event((int)(MouseEventFlags.LEFTDOWN), 0, 0, 0, 0);
                mouse_event((int)(MouseEventFlags.LEFTUP), 0, 0, 0, 0);
                Thread.Sleep(20);
            }
        }
    }
}
