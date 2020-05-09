using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Shmak.Properties;

namespace Shmak
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            update_GUI();
        }

        private void update_GUI()
        {
            comboBox1.SelectedIndex = Settings.Default.draft_mode;
            textBox1.Text = Settings.Default.wait_delay.ToString();
            textBox2.Text = Settings.Default.image_path;
            textBox3.Text = Settings.Default.sensitivity.ToString();
        }

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

        private class Px {
            public Color color;
            public int x;
            public int y;
            public Px(int x, int y, Color color)
            {
                this.x = x;
                this.y = y;
                this.color = color;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (Settings.Default.draft_mode)
            {
                case 0:
                    draw_full_line();
                    break;
                case 1:
                    draw_full_rand();
                    break;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            switch (Settings.Default.draft_mode)
            {
                case 0:
                    draw_chunk();
                    break;
                case 1:
                    Random rand = new Random();
                    for (int i = ChunksPix.Count - 1; i >= 1; i--)
                    {
                        int j = rand.Next(i + 1);
                        var temp = ChunksPix[j];
                        ChunksPix[j] = ChunksPix[i];
                        ChunksPix[i] = temp;
                    }
                    draw_chunk();
                    break;
            }
        }


        private void draw_chunk()
        {
            int lng = 500;
            if (ChunksPix.Count < 500)
            {
                lng = ChunksPix.Count;
            }

            for (int i = 0; i < lng; i++)
            {
                Px px = ChunksPix[i];
                ChunksPix.RemoveAt(i);
                Cursor.Position = new Point(ChunkCur.X + px.x, ChunkCur.Y + px.y);
                mouse_event((int)(MouseEventFlags.LEFTDOWN), 0, 0, 0, 0);
                mouse_event((int)(MouseEventFlags.LEFTUP), 0, 0, 0, 0);
                Thread.Sleep(20);
            }
        }

        private void draw_full_line()
        {
            if (!(File.Exists(Settings.Default.image_path))) { MessageBox.Show("Файл картинки не найден! (неверно указан путь или файл был удалён)", "Error");return; }
            Bitmap img = (Bitmap)Image.FromFile(Settings.Default.image_path);
            Thread.Sleep(5000);

            Point st_pos = Cursor.Position;
            List<Px> pixels = new List<Px>();

            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    Color clr = img.GetPixel(x, y);
                    if (clr.GetBrightness() < Settings.Default.sensitivity)
                    {
                        pixels.Add(new Px(x, y, clr));
                    }
                }
            }

            for (int i = 0; i < pixels.Count; i++)
            {
                Px px = pixels[i];
                Cursor.Position = new Point(st_pos.X + px.x, st_pos.Y + px.y);
                mouse_event((int)(MouseEventFlags.LEFTDOWN), 0, 0, 0, 0);
                mouse_event((int)(MouseEventFlags.LEFTUP), 0, 0, 0, 0);
                Thread.Sleep(Settings.Default.wait_delay);
            }
        }

        private void draw_full_rand()
        {
            if (!(File.Exists(Settings.Default.image_path))) { MessageBox.Show("Файл картинки не найден! (неверно указан путь или файл был удалён)", "Error"); return; }
            Bitmap img = (Bitmap)Image.FromFile(Settings.Default.image_path);
            Thread.Sleep(5000);

            Point st_pos = Cursor.Position;
            List<Px> pixels = new List<Px>();

            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    Color clr = img.GetPixel(x, y);
                    if (clr.GetBrightness() < Settings.Default.sensitivity)
                    {
                        pixels.Add(new Px(x, y, clr));
                    }
                }
            }

            Random rand = new Random();
            for (int i = pixels.Count - 1; i >= 1; i--)
            {
                int j = rand.Next(i + 1);
                var temp = pixels[j];
                pixels[j] = pixels[i];
                pixels[i] = temp;
            }

            for (int i = 0; i < pixels.Count; i++)
            {
                Px px = pixels[i];
                Cursor.Position = new Point(st_pos.X + px.x, st_pos.Y + px.y);
                mouse_event((int)(MouseEventFlags.LEFTDOWN), 0, 0, 0, 0);
                mouse_event((int)(MouseEventFlags.LEFTUP), 0, 0, 0, 0);
                Thread.Sleep(Settings.Default.wait_delay);
            }
        }


        private List<Px> ChunksPix;
        private Point ChunkCur;

        private void button2_Click(object sender, EventArgs e)
        {
            if (!(File.Exists(Settings.Default.image_path))) { MessageBox.Show("Файл картинки не найден! (неверно указан путь или файл был удалён)", "Error"); return; }
            Bitmap img = (Bitmap)Image.FromFile(Settings.Default.image_path);
            Thread.Sleep(5000);

            ChunkCur = Cursor.Position;
            ChunksPix = new List<Px>();

            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    Color clr = img.GetPixel(x, y);
                    if (clr.GetBrightness() < Settings.Default.sensitivity)
                    {
                        ChunksPix.Add(new Px(x, y, clr));
                    }
                }
            }

            SystemSounds.Asterisk.Play();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Default.draft_mode = comboBox1.SelectedIndex;
            Settings.Default.Save();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int num;
            if(!(int.TryParse(textBox1.Text, out num))) { return; }
            Settings.Default.wait_delay = num;
            Settings.Default.Save();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Title = "Select Image";
            fd.FileOk += dialog_ok;
            fd.Filter = "Image Files (*.bmp; *.png; *.jpg)|*.bmp;*.png;*.jpg|All files (*.*)|*.*";
            fd.ShowDialog();
            void dialog_ok(object dsndr, CancelEventArgs de)
            {
                Settings.Default.image_path = fd.FileName;
                Settings.Default.Save();
                update_GUI();
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            double num;
            if (!(double.TryParse(textBox3.Text, out num))) { return; }
            Settings.Default.sensitivity = num;
            Settings.Default.Save();
        }
    }
}
