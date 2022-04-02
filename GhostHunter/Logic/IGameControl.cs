using static GhostHunter.Logic.GhostHunterLogic;

namespace GhostHunter.Logic
{
    internal interface IGameControl
    {
        void Move(Direction direction);
    }
}