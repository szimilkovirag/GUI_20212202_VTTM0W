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
            flower, grass, player, tree2, trees, signboard, ground
        }

        public MapItem[,] GameMatrix { get; set; }
        private string[] levels;

        public GhostHunterLogic()
        {
            levels = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(),"Maps"),"*.txt");
            LoadNext(levels.First());
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
                    return MapItem.signboard;
                default:
                    return MapItem.ground;
            }
        }
    }
}
