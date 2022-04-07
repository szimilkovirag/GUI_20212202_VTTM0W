using System.Collections.Generic;

namespace GhostHunter.Logic
{
    public interface IGameModel
    {
        MapItem[,] GameMatrix { get; set; }
        List<Arrow> Arrows { get; set; }
    }
}