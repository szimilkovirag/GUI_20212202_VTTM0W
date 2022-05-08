using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Windows;

namespace GhostHunter.Logic
{
    public abstract class Enemy
    {
        public MapItem[,] GameMatrix { get; set; }
        public int Enemy_i { get; set; }
        public int Enemy_j { get; set; }
        public int HP { get; set; }
        public double Scale { get; set; }
        public virtual Rectangle Rectangle
        {
            get
            {
                return new Rectangle(Enemy_j, Enemy_i, 2, 2);
            }
        }

        public Enemy(int enemy_i, int enemy_j, MapItem[,] GameMatrix)
        {
            this.GameMatrix = GameMatrix;
            this.Enemy_i = enemy_i;
            this.Enemy_j = enemy_j;
            this.HP = 100;
            this.Scale = 1;
        }
        public abstract void MoveEnemy(int player_i, int player_j);

        public bool PlayerIsInSight(int player_i, int player_j)
        {
            if (Math.Abs(player_i - Enemy_i) <= 10 && Math.Abs(player_j - Enemy_j) <= 20)
                return true;
            else
                return false;
        }

        protected int Randomize(int min, int max)
        {
            int rnd = 0;
            do
            {
                rnd = Util.r.Next(min, max);
            } while (rnd == 0);
            return rnd;
        }

        //public void Dead()
        //{
        //    Rect enemyRect = new Rect(enemy_i,enemy_j,45,60);
        //    Rect arrowRect = new Rect();
        //}
    }
}
