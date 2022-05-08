using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GhostHunter.Logic
{
    public class ArcherEnemy : Enemy
    {
        public ArcherEnemy(int enemy_i, int enemy_j, MapItem[,] GameMatrix) : base(enemy_i, enemy_j, GameMatrix)
        {
        }
        public override void MoveEnemy(int player_i, int player_j)
        {
            if (PlayerIsInSight(player_i, player_j))
            {
                int dx = player_j - Enemy_j;
                if (dx < 0)
                {
                    Scale = -1;
                }
                if (dx > 0)
                {
                    Scale = 1;
                }
                //Shoot();
            }
        }

        
    }
}
