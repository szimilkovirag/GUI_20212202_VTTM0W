namespace GhostHunter.Logic
{
    public interface IGameControl
    {
        void Move(Direction direction);
        void Switch();
    }
}