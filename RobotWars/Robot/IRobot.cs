using System.Collections.Generic;
using RobotWars.Arena;

namespace RobotWars.Robot
{
    public interface IRobot
    {
        Point Position { get; set; }
        Heading Heading { get; set; }
        void Place(IBattleArena battleArena, Point point, Heading heading);
        void Move(IEnumerable<Movement> movements);
        bool IsPlaced();
    }
}