using RobotWars.Arena;
using RobotWars.Robot;

namespace RobotWars.Command
{
    public interface IPlaceRobotCommand : ICommand
    {
        Point Position { get; set; }
        Heading Heading { get; set; }
        void SetReceivers(IRobot paramRobot, IBattleArena paramBattleArena);
    }
}