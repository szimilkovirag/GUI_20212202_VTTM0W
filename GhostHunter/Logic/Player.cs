using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GhostHunter.Logic
{
    public abstract class Player
    {
        public int I { get; set; }
        public int J { get; set; }
        public int HP { get; set; }
        public int Shield { get; set; }
        public double Scale { get; set; }
        public Direction Direction { get; set; }
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle(J, I, 2, 2);
            }
        }
        
        public Player(int player_i, int player_j)
        {
            this.I = player_i;
            this.J = player_j;
            HP = 100;
            Shield = 50;
            this.Scale = 1;
        }
    }
}
