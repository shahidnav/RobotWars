namespace RobotWars.Arena
{
    public interface IBattleArena
    {
        void SetDimension(Dimension paramDimension);
        Dimension GetDimension();
        bool IsValid(Point point);
    }
}