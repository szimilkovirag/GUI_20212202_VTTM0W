using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostHunter.Logic
{
    public class GhostHunterLogic: IGameModel, IGameControl
    {
        public enum MapItem
        {
            flower, rocks, mushroom, grass, player, tree1, tree2, trees, ground, woods, winter, desert, starter, 
        }

        public enum Direction
        {
            Up, Down, Left, Right
        }

        public MapItem[,] GameMatrix { get; set; }
        private int player_i;
        private int player_j;
        private string[] levels;

        public GhostHunterLogic()
        {
            levels = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(),"Maps"),"*.txt");
            LoadNext(levels[0]);
        }

        private void LoadNext(string path)
        {
            string[] lines = File.ReadAllLines(path);
            GameMatrix = new MapItem[int.Parse(lines[1]), int.Parse(lines[0])];
            for (int i = 0; i < GameMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < GameMatrix.GetLength(1); j++)
                {
                    GameMatrix[i, j] = ConvertToEnum(lines[i + 2][j]);
                    if (GameMatrix[i, j] == MapItem.player)
                    {
                        player_i = i;
                        player_j = j;
                    }
                }
            }
        }
        public void Move(Direction direction)
        {
            int new_i = player_i;
            int new_j = player_j;
            switch (direction)
            {
                case Direction.Up:
                    if (new_i - 1 >= 0) new_i--;
                    break;
                case Direction.Down:
                    if (new_i + 1 < GameMatrix.GetLength(0))
                        new_i++;
                    break;
                case Direction.Left:
                    if (new_j - 1 >= 0) new_j--;
                    break;
                case Direction.Right:
                    if (new_j + 1 < GameMatrix.GetLength(1))
                        new_j++;
                    break;
                default:
                    break;
            }
            if (GameMatrix[new_i, new_j] == MapItem.ground)
            {
                GameMatrix[player_i, player_j] = MapItem.ground;
                player_i = new_i;
                player_j = new_j;
                GameMatrix[player_i, player_j] = MapItem.player;
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
                    return MapItem.player;
                default:
                    return MapItem.ground;
            }
        }
    }
}
