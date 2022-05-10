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
        public static List<Arrow> Enemy_arrow { get; set; }
        private int dx;
        private int dy;
        public override System.Drawing.Rectangle Rectangle
        {
            get
            {
                return new System.Drawing.Rectangle(Enemy_j, Enemy_i, 2, 4);
            }
        }

        public ArcherEnemy(int enemy_i, int enemy_j, MapItem[,] GameMatrix) : base(enemy_i, enemy_j, GameMatrix)
        {
            Enemy_arrow = new List<Arrow>();
        }

        public override void MoveEnemy(int player_i, int player_j, Size size)
        {
            if (PlayerIsInSight(player_i, player_j))
            {
                dx = player_j - Enemy_j;
                dy = player_i - Enemy_i;
                if (dx < 0)
                {
                    Scale = -1;
                }
                if (dx > 0)
                {
                    Scale = 1;
                }
                Shoot(size);
            }
        }

        public void Shoot(Size size)
        {
            double rectWidth = size.Width / GameMatrix.GetLength(1);
            double rectHeight = size.Height / GameMatrix.GetLength(0);

            Vector vector;
            Arrow arrow = new Arrow();
            arrow.Center = new Point(Enemy_j * rectWidth + 22, Enemy_i * rectHeight + 30);

            vector = new Vector(dx, dy);

            arrow.Speed = vector;
            Enemy_arrow.Add(arrow);
        }
    }
}
