using System.Collections.Generic;
using RobotWars.Robot;

namespace RobotWars.Command
{
    public interface IMoveRobotCommand : ICommand
    {
        IEnumerable<Movement> Movements { get;}
        void SetReceiver(IRobot paramRobot);
    }
}