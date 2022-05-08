using System;
using System.Collections.Generic;
using System.Windows;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostHunter.Logic
{
    public enum MapItem
    {
        flower, rocks, mushroom, grass, player1, player2, enemy, enemy2, boss, tree1, tree2, trees, ground, woods, winter, desert, starter,
    }

    public enum Direction
    {
        Up, Down, Left, Right
    }
    public class GhostHunterLogic : IGameModel, IGameControl
    {
        private Size size;
        public MapItem[,] GameMatrix { get; set; }
        public List<Enemy> Enemies { get; set; }
        public List<Arrow> Arrows { get; set; }
        public Player Player { get; set; }
        private string[] levels;
        private double rectWidth;
        private double rectHeight;

        public event EventHandler GameOver;

        public void Resize(Size area)
        {
            this.size = area;
        }

        public GhostHunterLogic()
        {
            Enemies = new List<Enemy>();
            Arrows = new List<Arrow>();
            levels = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "Maps"), "*.txt");
            LoadNext(levels[0]);
        }
        public void LoadNext(string path)
        {
            string[] lines = File.ReadAllLines(path);
            GameMatrix = new MapItem[int.Parse(lines[1]), int.Parse(lines[0])];
            for (int i = 0; i < GameMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < GameMatrix.GetLength(1); j++)
                {
                    GameMatrix[i, j] = ConvertToEnum(lines[i + 2][j]);
                    if (GameMatrix[i, j] == MapItem.player1)
                    {
                        Player = new AttackerPlayer(i, j);
                    }
                    if (GameMatrix[i, j] == MapItem.player2)
                    {
                        Player = new ArcherPlayer(i, j);
                    }
                    if (GameMatrix[i, j] == MapItem.enemy)
                    {
                        Enemies.Add(new AttackerEnemy(i, j, GameMatrix));
                    }
                    if (GameMatrix[i, j] == MapItem.enemy2)
                    {
                        Enemies.Add(new ArcherEnemy(i, j, GameMatrix));
                    }
                    if (GameMatrix[i, j] == MapItem.boss)
                    {
                        Enemies.Add(new BossEnemy(i, j, GameMatrix));
                    }
                }
            }
        }
        public void Move(Direction direction)
        {
            int new_i = Player.I;
            int new_j = Player.J;
            switch (direction)
            {
                case Direction.Up:
                    if (new_i - 1 >= 0)
                    {
                        new_i--;
                        Player.Direction = Direction.Up;
                        //Angle = 270;
                    }
                    break;
                case Direction.Down:
                    if (new_i + 1 < GameMatrix.GetLength(0))
                    {
                        new_i++;
                        Player.Direction = Direction.Down;
                        //Angle = 90;
                    }
                    break;
                case Direction.Left:
                    if (new_j - 1 >= 0)
                    {
                        new_j--;
                        Player.Direction = Direction.Left;
                        Player.Scale = -1;
                    }
                    break;
                case Direction.Right:
                    if (new_j + 1 < GameMatrix.GetLength(1))
                    {
                        new_j++;
                        Player.Direction = Direction.Right;
                        Player.Scale = 1;
                    }
                    break;
                default:
                    break;
            }
            if (GameMatrix[new_i, new_j] == MapItem.ground)
            {
                GameMatrix[Player.I, Player.J] = MapItem.ground;
                Player.I = new_i;
                Player.J = new_j;
                if (Player is AttackerPlayer)
                    GameMatrix[Player.I, Player.J] = MapItem.player1;
                if (Player is ArcherPlayer)
                    GameMatrix[Player.I, Player.J] = MapItem.player2;
            }
            else if (GameMatrix[new_i, new_j] == MapItem.woods)
            {
                LoadNext(levels[1]);
            }
            else if (GameMatrix[new_i, new_j] == MapItem.winter)
            {
                LoadNext(levels[2]);
            }
            else if (GameMatrix[new_i, new_j] == MapItem.desert)
            {
                LoadNext(levels[3]);
            }
            else if (GameMatrix[new_i, new_j] == MapItem.starter)
            {
                LoadNext(levels[0]);
            }
        }

        public void NewShoot()
        {
             rectWidth = size.Width / GameMatrix.GetLength(1);
             rectHeight = size.Height / GameMatrix.GetLength(0);

            Vector vector;
            Arrow arrow = new Arrow();
            arrow.Center = new Point(Player.J * rectWidth + 30, Player.I * rectHeight + 30);
            if (Player is ArcherPlayer)
            {
                switch (Player.Direction)
                {
                    case Direction.Up:
                        vector = new Vector(0, -90);
                        arrow.Angle = 270;
                        break;
                    case Direction.Down:
                        vector = new Vector(0, 90);
                        arrow.Angle = 90;
                        break;
                    case Direction.Right:
                        vector = new Vector(90, 0);
                        arrow.Angle = 0;
                        break;
                    case Direction.Left:
                        vector = new Vector(-90, 0);
                        arrow.Angle = 180;
                        break;
                }
                arrow.Speed = vector;
                Arrows.Add(arrow);
            }
        }

        

        public void Switch()
        {
            if (Player is AttackerPlayer)
            {
                Player = new ArcherPlayer(Player.I, Player.J);
                GameMatrix[Player.I, Player.J] = MapItem.player2;
            }
            else
            {
                Player = new AttackerPlayer(Player.I, Player.J);
                GameMatrix[Player.I, Player.J] = MapItem.player1;
            }
        }
        public void MoveItems()
        {
            for (int i = 0; i < Arrows.Count; i++)
            {
                bool inside = Arrows[i].Move(size);
                if (!inside)
                    Arrows.RemoveAt(i);
            }

            //System.Drawing.Rectangle enemyrect = item.Rectangle;
            foreach (var item in Enemies)
            {
                item.MoveEnemy(Player.I, Player.J);
                System.Drawing.Rectangle enemyrect = item.Rectangle;
                if (item is AttackerEnemy && enemyrect.IntersectsWith(Player.Rectangle))
                {
                    Player.Shield -= 50;
                    if(Player.Shield <= 0)
                    {
                        Player.HP -= 50;
                        if (Player.HP <= 0)
                        {
                            GameOver?.Invoke(this, null);
                        }
                    }
                }
                foreach (var arrow in Arrows)
                {
                    System.Drawing.Rectangle arrowRect = arrow.Rectangle;
                    System.Drawing.Rectangle arrowRectseged = new System.Drawing.Rectangle((int)(arrow.Center.X / rectWidth -1.5), (int)(arrow.Center.Y / rectHeight +4), 3, 3);

                    if (arrowRectseged.IntersectsWith(enemyrect))
                    {
                        //Arrows.RemoveAt()
                        GameOver?.Invoke(this, null);
                        //item.HP -= 30;
                        //if (item.HP <= 0)
                        //{
                        //    GameOver?.Invoke(this, null);
                        //}
                    }
                    //else if (arrowRect.IntersectsWith(Player.Rectangle))
                    //{
                    //    Player.HP -= 50;
                    //    if (Player.HP <= 0)
                    //    {
                    //        GameOver?.Invoke(this, null);
                    //    }
                    //}
                }

            }


        }
        //public void MoveArrow()
        //{
            
        //}
        private MapItem ConvertToEnum(char v)
        {
            switch (v)
            {
                case 't':
                    return MapItem.tree2;
                case 'T':
                    return MapItem.trees;
                case 'F':
                    return MapItem.flower;
                case 'B':
                    return MapItem.woods;
                case 'W':
                    return MapItem.winter;
                case 'D':
                    return MapItem.desert;
                case 'S':
                    return MapItem.starter;
                case 'G':
                    return MapItem.grass;
                case 'R':
                    return MapItem.rocks;
                case 'M':
                    return MapItem.mushroom;
                case 'E':
                    return MapItem.tree1;
                case 'P':
                    return MapItem.player1;
                case 'p':
                    return MapItem.player2;
                case 'X':
                    return MapItem.enemy;
                case 'Y':
                    return MapItem.enemy2;
                case 'Z':
                    return MapItem.boss;
                default:
                    return MapItem.ground;
            }
        }

        //public void Dead()
        //{
        //    for (int i = 0; i < Enemies.Count; i++)
        //    {
        //        Rect enemyRect = new Rect(Enemies[i].enemy_i, Enemies[i].enemy_j, 45, 60);
        //        Rect playerRect = new Rect(Player.I, Player.J, 60, 60);
        //        if (Enemies[i] is AttackerEnemy && enemyRect.IntersectsWith(playerRect) && Player.Shield > 0)
        //        {
        //            Player.Shield -= 50;
        //        }
        //        else if (Enemies[i] is AttackerEnemy && enemyRect.IntersectsWith(playerRect) && Player.Shield <= 0)
        //        {
        //            Player.HP -= 50;
        //            if (Player.HP <= 0)
        //            {
        //                GameOver?.Invoke(this, null);
        //            }
        //        }
        //        foreach (var item in Arrows)
        //        {
        //            Rect arrowRect = new Rect(item.Center.X, item.Center.Y, 10, 5);

        //            if (arrowRect.IntersectsWith(enemyRect))
        //            {
        //                Enemies[i].HP -= 30;
        //                if (Enemies[i].HP <= 0)
        //                {
        //                    Enemies.RemoveAt(i);
        //                }
        //            }
        //            else if (arrowRect.IntersectsWith(playerRect))
        //            {
        //                Player.HP -= 50;
        //                if (Player.HP <= 0)
        //                {
        //                    GameOver?.Invoke(this, null);
        //                }
        //            }
        //        }
        //    }
        //}
    }
}

