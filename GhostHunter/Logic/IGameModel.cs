using System.Collections.Generic;
using System.Windows;

namespace GhostHunter.Logic
{
    public interface IGameModel
    {
        MapItem[,] GameMatrix { get; set; }
        List<Arrow> Arrows { get; set; }
        List<Arrow> Enemy_Arrows { get; set; }
        Player Player { get; set; }
        List<Enemy> Enemies { get; set; }
        string[] Levels { get; set; }
        string Current { get; set; }
    }
}