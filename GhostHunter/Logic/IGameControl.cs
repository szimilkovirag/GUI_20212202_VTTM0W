using static GhostHunter.Logic.GhostHunterLogic;

namespace GhostHunter.Logic
{
    public interface IGameControl
    {
        void Move(Direction direction);
    }
}