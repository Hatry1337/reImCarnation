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
using Shmak.Drafters;
using Shmak.Properties;

namespace Shmak
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadedImage = (Bitmap)Image.FromFile(Settings.Default.image_path);
            update_GUI();

            NotifyIcon icon = new NotifyIcon();
            icon.ShowBalloonTip(1000, "Balloon title", "Balloon text", ToolTipIcon.Info);
        }
        public Bitmap LoadedImage;
        private Thread DrawThread;
        private void update_GUI()
        {
            comboBox1.SelectedIndex = Settings.Default.draft_mode;
            textBox1.Text = Settings.Default.wait_delay.ToString();
            textBox2.Text = Settings.Default.image_path;
            textBox3.Text = Settings.Default.sensitivity.ToString();
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
                LoadedImage = (Bitmap)Image.FromFile(Settings.Default.image_path = fd.FileName);
                //if (i.Size.Width > 512 || i.Size.Height > 512)
                //{
                //    MessageBox.Show("Картинка должна быть размером не больше 512x512px", "Error");
                //    return;
                //}
                Settings.Default.image_path = fd.FileName;
                Settings.Default.Save();
                update_GUI();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IDrafter drafter;
            switch (Settings.Default.draft_mode)
            {
                case 0:
                    drafter = new LineDrafter();
                    DrawThread = new Thread(new ThreadStart(drafter.Draw));
                    DrawThread.Start();
                    break;
                case 1:
                    drafter = new RandomDrafter();
                    DrawThread = new Thread(new ThreadStart(drafter.Draw));
                    DrawThread.Start();
                    break;
                case 2:
                    drafter = new OptimizedDrafter(1);
                    DrawThread = new Thread(new ThreadStart(drafter.Draw));
                    DrawThread.Start();
                    break;
                case 3:
                    drafter = new OptimizedDrafter(2);
                    DrawThread = new Thread(new ThreadStart(drafter.Draw));
                    DrawThread.Start();
                    break;
                case 4:
                    drafter = new OptimizedDrafter(3);
                    DrawThread = new Thread(new ThreadStart(drafter.Draw));
                    DrawThread.Start();
                    break;
                case 5:
                    drafter = new OptimizedDrafter(4);
                    DrawThread = new Thread(new ThreadStart(drafter.Draw));
                    DrawThread.Start();
                    break;
            }

        }

        private ChunkedDrafter chunkedDrafter;

        private void button3_Click(object sender, EventArgs e)
        {
            switch (Settings.Default.draft_mode)
            {
                case 0:
                    if(!(chunkedDrafter == null))
                    {
                        chunkedDrafter.Mode = 0;
                        DrawThread = new Thread(new ThreadStart(chunkedDrafter.Draw));
                        DrawThread.Start();
                    }
                    else
                    {
                        MessageBox.Show("Сперва нужно откалибровать курсор!", "Error");
                    }
                    break;

                case 1:
                    if (!(chunkedDrafter == null))
                    {
                        chunkedDrafter.Mode = 1;
                        DrawThread = new Thread(new ThreadStart(chunkedDrafter.Draw));
                        DrawThread.Start();
                    }
                    else
                    {
                        MessageBox.Show("Сперва нужно откалибровать курсор!", "Error");
                    }
                    break;
                default:
                    MessageBox.Show("Отрисовка чанками поддерживается только в режимах Line Mode и Random Mode!", "Error");
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            chunkedDrafter = new ChunkedDrafter(0);
            DrawThread = new Thread(new ThreadStart(chunkedDrafter.Collibrate));
            DrawThread.Start();
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

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            double num;
            if (!(double.TryParse(textBox3.Text, out num))) { return; }
            Settings.Default.sensitivity = num;
            Settings.Default.Save();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            prev pr = new prev((Bitmap)Image.FromFile(Settings.Default.image_path));
            pr.Show();
        }
    }
}
