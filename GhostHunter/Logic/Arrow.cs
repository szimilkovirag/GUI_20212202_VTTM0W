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

        public Arrow(Point center, Vector speed)
        {
            this.Center = center;
            this.Speed = speed;
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
