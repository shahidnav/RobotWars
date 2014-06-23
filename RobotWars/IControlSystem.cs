using RobotWars.Arena;

namespace RobotWars
{
    public interface IControlSystem
    {
        void Execute(string paramInput);
        IBattleArena GetBattleArena();
        string ProduceRobotsReport();
    }
}