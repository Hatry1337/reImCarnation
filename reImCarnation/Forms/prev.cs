using reImCarnation.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace reImCarnation
{


    public partial class prev : Form
    {
        public Bitmap image;
        public Bitmap orig;
        byte[,,] in_arr;
        byte[,,] out_arr;
        public prev(Bitmap image)
        {
            InitializeComponent();
            saveFileDialog1.Filter = "Image Files (*.bmp; *.png; *.jpg)|*.bmp;*.png;*.jpg|All files (*.*)|*.*";
            this.orig = image;
            this.image = (Bitmap)resizeImage(image, new Size(256, 256));
            this.in_arr = Rgb.BitmapToByteRgbQ(this.image);
            this.out_arr = new byte[3, this.image.Height, this.image.Width];
            update_gui();
        }


        private void draw_img()
        {
            for (int y = 0; y < this.image.Height; y++)
            {
                for (int x = 0; x < this.image.Width; x++)
                {
                    float br = (in_arr[0, y, x] + in_arr[1, y, x] + in_arr[2, y, x]) / 3;
                    if (br < Settings.Default.sensitivity)
                    {
                        out_arr[0, y, x] = 0;
                        out_arr[1, y, x] = 0;
                        out_arr[2, y, x] = 0;
                    }
                    else
                    {
                        out_arr[0, y, x] = 255;
                        out_arr[1, y, x] = 255;
                        out_arr[2, y, x] = 255;
                    }
                }
            }
            this.pictureBox1.Image = Rgb.RgbToBitmapQ(out_arr); 
        }

        private static Image resizeImage(Image imgToResize, Size size)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)size.Width / (float)sourceWidth);
            nPercentH = ((float)size.Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();

            return (Image)b;
        }

        private void update_gui()
        {
            label1.Text = "Sensitivity: " + Settings.Default.sensitivity.ToString();
            trackBar1.Value = (int)(Settings.Default.sensitivity);
            draw_img();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Settings.Default.sensitivity = trackBar1.Value;
            Settings.Default.Save();
            update_gui();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                this.image = this.orig;
                this.in_arr = Rgb.BitmapToByteRgbQ(this.image);
                this.out_arr = new byte[3, this.image.Height, this.image.Width];
                update_gui();
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Console.WriteLine("GG1");
            if (radioButton2.Checked)
            {
                Console.WriteLine("GG2");
                this.image = (Bitmap)resizeImage(this.orig, new Size(512, 512));
                this.in_arr = Rgb.BitmapToByteRgbQ(this.image);
                this.out_arr = new byte[3, this.image.Height, this.image.Width];
                update_gui();
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                this.image = (Bitmap)resizeImage(this.orig, new Size(256, 256));
                this.in_arr = Rgb.BitmapToByteRgbQ(this.image);
                this.out_arr = new byte[3, this.image.Height, this.image.Width];
                update_gui();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            Rgb.RgbToBitmapQ(this.out_arr).Save(saveFileDialog1.FileName);
        }
    }
}
