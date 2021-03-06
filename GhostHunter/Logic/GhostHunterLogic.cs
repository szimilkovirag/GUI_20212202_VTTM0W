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
        wintertop, winterbush, winteriglo, wintertree, desertlake, desertrock, desertcactus, woodsdeadtree, woodstree, woodsnest, woods1, woods2, woods3,
        woods4, desert1, desert2, desert3, snow1, snow2, snow3
    }

    public enum Direction
    {
        Up, Down, Left, Right
    }
    public class GhostHunterLogic: IGameModel, IGameControl
    {
        private Size size;
        public MapItem[,] GameMatrix { get; set; }
        public List<Enemy> Enemies { get; set; }
        public List<Arrow> Arrows { get; set; }
        public Player Player { get; set; }
        public List<Arrow> Enemy_Arrows { get; set; }
        public string[] Levels { get; set; }
        public string Current { get; set; }

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
            Enemy_Arrows = new List<Arrow>();
            Levels = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(),"Maps"),"*.txt");
            LoadNext(Levels[0]);
        }
        public void LoadNext(string path)
        {
            Current = path;
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
                    if(GameMatrix[i, j] == MapItem.enemy2)
                    {
                        Enemies.Add(new ArcherEnemy(i, j, GameMatrix));
                    }
                    if(GameMatrix[i, j] == MapItem.boss)
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
                    }
                    break;
                case Direction.Down:
                    if (new_i + 1 < GameMatrix.GetLength(0))
                    {
                        new_i++;
                        Player.Direction = Direction.Down;
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
                if(Player is AttackerPlayer)
                    GameMatrix[Player.I, Player.J] = MapItem.player1;
                if (Player is ArcherPlayer)
                    GameMatrix[Player.I, Player.J] = MapItem.player2;
            }
            else if (GameMatrix[new_i, new_j] == MapItem.desert)
            {
                LoadNext(Levels[1]);
            }
            else if (GameMatrix[new_i, new_j] == MapItem.winter)
            {
                LoadNext(Levels[2]);
            }
            else if (GameMatrix[new_i, new_j] == MapItem.woods)
            {
                LoadNext(Levels[3]);
            }
            else if (GameMatrix[new_i, new_j] == MapItem.starter)
            {
                LoadNext(Levels[0]);
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
                switch(Player.Direction)
                {
                    case Direction.Up:
                        vector = new Vector(0, -20);
                        arrow.Angle = 270;
                        break;
                    case Direction.Down:
                        vector = new Vector(0, 20);
                        arrow.Angle = 90;
                        break;
                    case Direction.Right:
                        vector = new Vector(20, 0);
                        arrow.Angle = 0;
                        break;
                    case Direction.Left:
                        vector = new Vector(-20, 0);
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
            rectWidth = size.Width / GameMatrix.GetLength(1);
            rectHeight = size.Height / GameMatrix.GetLength(0);

            for (int i = 0; i < Arrows.Count; i++)
            {
                bool inside = Arrows[i].Move(size);
                if (!inside)
                    Arrows.RemoveAt(i);
            }
            for (int i = 0; i < Enemy_Arrows.Count; i++)
            {
                bool inside = Enemy_Arrows[i].Move(size);
                if (!inside)
                    Enemy_Arrows.RemoveAt(i);
            }

            for (int e = 0; e < Enemies.Count; e++)
            {
                Enemies[e].MoveEnemy(Player.I, Player.J, size);

                Enemy_Arrows = ArcherEnemy.Enemy_arrow;

                System.Drawing.Rectangle enemyrect = Enemies[e].Rectangle;

                if ((Player is AttackerPlayer) && Player.Rectangle.IntersectsWith(enemyrect))
                {
                    Enemies[e].HP -= 30;
                    if (Enemies[e].HP <= 0)
                    {
                        Enemies.RemoveAt(e);
                        e--;
                    }
                }
                if ((Enemies[e] is AttackerEnemy || Enemies[e] is BossEnemy) && enemyrect.IntersectsWith(Player.Rectangle))
                {
                    Player.Shield -= 10;
                    if (Player.Shield <= 0)
                    {
                        Player.HP -= 10;
                        if (Player.HP <= 0)
                        {
                            GameOver?.Invoke(this, null);
                        }
                    }
                }
                for (int i = 0; i < Arrows.Count; i++)
                {
                    System.Drawing.Rectangle arrowRect = Arrows[i].Rectangle;
                    System.Drawing.Rectangle arrowRectseged = new System.Drawing.Rectangle((int)(Arrows[i].Center.X / rectWidth - 2), (int)(Arrows[i].Center.Y / rectHeight), 2, 2);
                    if (arrowRectseged.IntersectsWith(enemyrect))
                    {
                        Arrows.RemoveAt(i);
                        Enemies[e].HP -= 30;
                        if (Enemies[e].HP <= 0)
                        {
                            Enemies.RemoveAt(e);
                            e--;
                        }
                    }
                }
                for (int j = 0; j < Enemy_Arrows.Count; j++)
                {
                    System.Drawing.Rectangle enemy_arrowRect = Enemy_Arrows[j].Rectangle;
                    System.Drawing.Rectangle enemy_arrowRectseged = new System.Drawing.Rectangle((int)(Enemy_Arrows[j].Center.X / rectWidth - 1), (int)(Enemy_Arrows[j].Center.Y / rectHeight - 2), 2, 2);
                    if (enemy_arrowRectseged.IntersectsWith(Player.Rectangle))
                    {
                        Enemy_Arrows.RemoveAt(j);
                        Player.Shield -= 10;
                        if (Player.Shield <= 0)
                        {
                            Player.HP -= 10;
                            if (Player.HP <= 0)
                            {
                                GameOver?.Invoke(this, null);
                            }
                        }
                    }
                }
            }
        }
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
                case 'Ű':
                    return MapItem.desertrock;
                case 'C':
                    return MapItem.desertcactus;
                case 'L':
                    return MapItem.desertlake;
                case 'Ó':
                    return MapItem.wintertree;
                case 'Ü':
                    return MapItem.winterbush;
                case 'Ö':
                    return MapItem.winteriglo;
                case 'I':
                    return MapItem.wintertop;
                case 'N':
                    return MapItem.woodstree;
                case 'Á':
                    return MapItem.woodsdeadtree;
                case 'Í':
                    return MapItem.woodsnest;
                case 'Q':
                    return MapItem.woods1;
                case 'U':
                    return MapItem.woods2;
                case 'Ő':
                    return MapItem.woods3;
                case 'O':
                    return MapItem.woods4;
                case 'Ú':
                    return MapItem.desert1;
                case 'H':
                    return MapItem.desert2;
                case 'J':
                    return MapItem.desert3;
                case 'V':
                    return MapItem.snow1;
                case 'É':
                    return MapItem.snow2;
                case '!':
                    return MapItem.snow3;
                default:
                    return MapItem.ground;
            }
        }
    }
}
