using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GhostHunter.Logic
{
    public class BossEnemy : Enemy
    {
        public BossEnemy(int enemy_i, int enemy_j, MapItem[,] GameMatrix) : base(enemy_i, enemy_j, GameMatrix)
        {
        }

        public override void MoveEnemy(int player_i, int player_j, Size size)
        {
            int new_i = Enemy_i;
            int new_j = Enemy_j;
            if (PlayerIsInSight(player_i, player_j))
            {
                int dx = player_j - Enemy_j;
                int dy = player_i - Enemy_i;
                if (dx < 0)
                {
                    new_j--;
                    Scale = -1;
                }
                if (dx > 0) 
                { 
                    new_j++;
                    Scale = 1;
                }
                if (dy < 0) new_i--;
                if (dy > 0) new_i++;
            }
            if (GameMatrix[new_i, new_j] == MapItem.ground)
            {
                GameMatrix[Enemy_i, Enemy_j] = MapItem.ground;
                Enemy_i = new_i;
                Enemy_j = new_j;
                GameMatrix[Enemy_i, Enemy_j] = MapItem.boss;
            }
        }
    }
}
