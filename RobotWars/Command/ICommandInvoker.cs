using System.Collections.Generic;
using RobotWars.Arena;
using RobotWars.Robot;

namespace RobotWars.Command
{
    public interface ICommandInvoker
    {
        void SetBattleArena(IBattleArena paramBattleArena);
        void SetCommands(IEnumerable<ICommand> commands);
        void Invoke();
        void SetRobots(IList<IRobot> paramRobots);
    }
}
