using System.Collections.Generic;
using RobotWars.Arena;
using RobotWars.Robot;

namespace RobotWars.Report
{
    public interface IReporter
    {
        string GetReports(IEnumerable<IRobot> paramRobots);
        string ProduceReport(Point position, Heading heading);
    }
}