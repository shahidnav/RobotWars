using RobotWars.Arena;
using RobotWars.Robot;

namespace RobotWars.Command
{
    public class PlaceRobotCommand : IPlaceRobotCommand
    {
        private IBattleArena _battleArena;
        private IRobot _robot;

        public PlaceRobotCommand(Point paramPoint, Heading paramHeading)
        {
            Position = paramPoint;
            Heading = paramHeading;
        }

        public Point Position { get; set; }
        public Heading Heading { get; set; }

        public void SetReceivers(IRobot paramRobot, IBattleArena paramBattleArena)
        {
            _robot = paramRobot;
            _battleArena = paramBattleArena;
        }

        public CommandType GetCommandType()
        {
            return CommandType.PlaceRobot;
        }

        public void Execute()
        {
            _robot.Place(_battleArena, Position, Heading);
        }
    }
}