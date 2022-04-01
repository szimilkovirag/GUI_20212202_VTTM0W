using static GhostHunter.Logic.GhostHunterLogic;

namespace GhostHunter.Logic
{
    public interface IGameModel
    {
        MapItem[,] GameMatrix { get; set; }
    }
}