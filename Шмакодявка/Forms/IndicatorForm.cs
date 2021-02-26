using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace reImCarnation.Forms
{
    public partial class IndicatorForm : Form
    {
        int Counter = 0;
        public IndicatorForm(int width, int height)
        {
            InitializeComponent();
            this.Height = height;
            this.Width = width;
            this.label1.Font = new Font(label1.Font.FontFamily, height / 8, label1.Font.Style);
            this.label1.Location = new Point(width / 2 - this.label1.Size.Width / 2, height / 2 - this.label1.Size.Height / 2);
            this.Update();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Counter++;
            this.Location = new Point(Cursor.Position.X, Cursor.Position.Y);
            if(Counter >= 200)
            {
                this.Dispose();
            }
        }
    }
}
