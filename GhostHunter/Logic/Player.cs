using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostHunter.Logic
{
    public abstract class Player
    {
        public int I { get; set; }
        public int J { get; set; }
        public int HP { get; set; }
        public Player(int player_i, int player_j)
        {
            this.I = player_i;
            this.J = player_j;
        }
    }
}
