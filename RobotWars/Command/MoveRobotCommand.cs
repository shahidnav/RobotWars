using System.Collections.Generic;
using RobotWars.Robot;

namespace RobotWars.Command
{
    public class MoveRobotCommand : IMoveRobotCommand
    {
        private IRobot _robot;

        public MoveRobotCommand(IEnumerable<Movement> movements)
        {
            Movements = movements;
        }

        public IEnumerable<Movement> Movements { get; private set; }

        public void SetReceiver(IRobot paramRobot)
        {
            _robot = paramRobot;
        }

        public CommandType GetCommandType()
        {
            return CommandType.MoveRobot;
        }

        public void Execute()
        {
            _robot.Move(Movements);
        }
    }
}