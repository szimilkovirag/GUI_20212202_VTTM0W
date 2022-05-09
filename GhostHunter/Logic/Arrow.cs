using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GhostHunter.Logic
{
    public class Arrow
    {
        public Point Center { get; set; }
        public Vector Speed { get; set; }
        public double Angle { get; set; }
        public int Hit { get; set; }
        public System.Drawing.Rectangle Rectangle
        {
            get
            {
                return new System.Drawing.Rectangle((int)Center.X, (int)Center.Y, 3, 3);
            }
        }
        public Arrow()
        {
            Hit = 50;
        }
        public bool Move(Size area)
        {
            Point newCenter = new Point(Center.X + (int)Speed.X, Center.Y + (int)Speed.Y);
            if (newCenter.X >= 0 && newCenter.Y <= area.Width &&
                newCenter.Y >= 0 && newCenter.Y <= area.Height)
            {
                Center = newCenter;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
