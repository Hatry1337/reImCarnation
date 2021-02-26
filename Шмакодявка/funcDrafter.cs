using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace reImCarnation
{
    public delegate double Func(double x);
 
    public class funcDrafter
    {
        public int 
            uMin = 0,
            uMax = 800, 
            vMin = 0, 
            vMax = 600, 
            nPoints;

        public double 
            xMin = -10, 
            xMax = 10, 
            yMin = -1, 
            yMax = 1, 
            xStep, 
            xComp, 
            yComp;

        public void setFuncArea(double xMin, double xMax, double yMin, double yMax)
        {
            this.xMin = xMin;
            this.xMax = xMax;
            this.yMin = yMin;
            this.yMax = yMax;
        }
        public void setScreenArea(int uMin, int uMax, int vMin, int vMax)
        {
            this.uMin = uMin;
            this.uMax = uMax;
            this.vMin = vMin;
            this.vMax = vMax;
        }
        public int xCrdToScr(double x)
        {
            return 0;
        }



        public void DrawFunc(Func f)
        {
            
        }
    }
}
