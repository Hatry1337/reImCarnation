using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reImCarnation
{
    public class Metrics
    {
        public Metrics(string DrafterName)
        {
            this.DrafterName = DrafterName;
            StartTime = DateTime.Now;
        }
        public string DrafterName;
        public int TotalPixels = 0;
        public int PixelsDrafted = 0;
        public int MouseClicks = 0;

        public DateTime StartTime;
        public DateTime EndTime;
    }
}
