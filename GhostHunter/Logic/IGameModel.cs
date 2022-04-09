using System.Collections.Generic;
using System.Windows;

namespace GhostHunter.Logic
{
    public interface IGameModel
    {
        MapItem[,] GameMatrix { get; set; }
        List<Arrow> Arrows { get; set; }
        double Angle { get; set; }
        Player Player { get; set; }
    }
}