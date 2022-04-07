﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostHunter.Logic
{
    public abstract class Enemy : IGameModel
    {
        public MapItem[,] GameMatrix { get; set; }
        protected int enemy_i;
        protected int enemy_j;
        public int HP { get; set; }
        public List<Arrow> Arrows { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Enemy(int enemy_i, int enemy_j, MapItem[,] GameMatrix)
        {
            this.GameMatrix = GameMatrix;
            this.enemy_i = enemy_i;
            this.enemy_j = enemy_j;
            this.HP = 100;
        }
        public abstract void MoveEnemy();

        protected int Randomize(int min, int max)
        {
            int rnd = 0;
            do
            {
                rnd = Util.r.Next(min, max);
            } while (rnd == 0);
            return rnd;
        }
    }
}
