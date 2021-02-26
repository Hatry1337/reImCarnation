using reImCarnation.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace reImCarnation
{
    public partial class MetricsForm : Form
    {
        public MetricsForm(Metrics met)
        {
            this.metrics = met;
            InitializeComponent();
            this.metBox.Text = 
                $"Drafter Type: {met.DrafterName}\n\n" +
                $"Delay: {Settings.Default.wait_delay}ms.\n\n" +
                $"Sensitivity: {Settings.Default.sensitivity}\n\n" +
                $"Total Pixels: {met.TotalPixels}\n\n" +
                $"Pixels Drafted: {met.PixelsDrafted}\n\n" +
                $"Total Mouse Clicks:{met.MouseClicks}\n\n" +
                $"Started at: {met.StartTime.ToString()}\n\n" +
                $"Ended at: {met.EndTime.ToString()}\n\n" +
                $"Time Used: {(met.EndTime - met.StartTime).ToString()}";
            this.ShowDialog();
        }
        private Metrics metrics;

        private void saveToFileBtn_Click(object sender, EventArgs e)
        {
            string path = metrics.StartTime.ToString().Replace(":", "-") + ".txt";
            System.IO.File.WriteAllText(path, this.metBox.Text);
            notifyIcon1.Icon = SystemIcons.Information;
            notifyIcon1.BalloonTipText = $"Файл сохранен под именем {path}";
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon1.BalloonTipClicked += new EventHandler(MetricsForm_NotyClick);

            notifyIcon1.Visible = true;
            notifyIcon1.ShowBalloonTip(5000);
        }

        private void MetricsForm_NotyClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(this.metrics.StartTime.ToString().Replace(":", "-") + ".txt");
        }
        private void copyMetrics_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(this.metBox.Text);
        }

    }
}
