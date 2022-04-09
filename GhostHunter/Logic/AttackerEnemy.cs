using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GhostHunter.Logic
{
    public class AttackerEnemy : Enemy
    {
        public AttackerEnemy(int enemy_i, int enemy_j, MapItem[,] GameMatrix) : base(enemy_i, enemy_j, GameMatrix)
        {
        }
        public override void MoveEnemy(int player_i, int player_j)
        {
            int new_i = enemy_i;
            int new_j = enemy_j;
            if (PlayerIsInSight(player_i, player_j))
            {
                int dx = player_j - enemy_j;
                int dy = player_i - enemy_i;
                if (dx < 0) new_j--;
                if (dx > 0) new_j++;
                if (dy < 0) new_i--;
                if (dy > 0) new_i++;
            }
            else
            {
                int directionX = Randomize(-1, 2);
                int directionY = Randomize(-1, 2);
                if (directionY == -1 && new_i - 1 >= 0)
                    new_i--;
                if (directionY == 1 && new_i + 1 < GameMatrix.GetLength(0))
                    new_i++;
                if (directionX == -1 && new_j - 1 >= 0)
                {
                    new_j--;
                    Angle = 180;
                }
                    
                if (directionX == 1 && new_j + 1 < GameMatrix.GetLength(1))
                {
                    new_j++;
                    Angle = 0;
                }
                    
            }
            if (GameMatrix[new_i, new_j] == MapItem.ground)
            {
                GameMatrix[enemy_i, enemy_j] = MapItem.ground;
                enemy_i = new_i;
                enemy_j = new_j;
                GameMatrix[enemy_i, enemy_j] = MapItem.enemy;
            }

        }
    }
}
