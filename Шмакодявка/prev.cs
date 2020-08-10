using Shmak.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shmak
{
    public partial class prev : Form
    {
        public Bitmap image;
        public Bitmap wh_bit;
        public prev(Bitmap image)
        {
            this.image = image;
            wh_bit = new Bitmap(image.Width, image.Height);
            for (int y = 0; y < this.wh_bit.Height; y++)
            {
                for (int x = 0; x < this.wh_bit.Width; x++)
                {
                    this.wh_bit.SetPixel(x, y, Color.White);
                }
            }
            InitializeComponent();
            update_gui();
        }

        private void draw_img()
        {
            Bitmap tmp = (Bitmap)wh_bit.Clone();

            for (int y = 0; y < this.image.Height; y++)
            {
                for (int x = 0; x < this.image.Width; x++)
                {
                    Color clr = this.image.GetPixel(x, y);
                    if (clr.GetBrightness() < Settings.Default.sensitivity)
                    {
                        tmp.SetPixel(x, y, Color.Black);
                    }
                }
            }

            pictureBox1.Image = tmp;
        }

        private void update_gui()
        {
            label1.Text = "Sensitivity: " + Settings.Default.sensitivity.ToString();
            trackBar1.Value = (int)(Settings.Default.sensitivity * 10);
            draw_img();
        }

        private void prev_Load(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Settings.Default.sensitivity = 0.1 * trackBar1.Value;
            Settings.Default.Save();
            update_gui();
        }

    }
}
