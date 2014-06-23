using System.Collections.Generic;
using System.Text;
using RobotWars.Arena;
using RobotWars.Robot;

namespace RobotWars.Report
{
    public class Reporter : IReporter
    {
        private readonly IDictionary<Heading, char> _headingDictionary;

        public Reporter()
        {
            _headingDictionary = new Dictionary<Heading, char>
                {
                    {Heading.North, 'N'},
                    {Heading.South, 'S'},
                    {Heading.East, 'E'},
                    {Heading.West, 'W'}
                };
        }

        public string ProduceReport(Point position, Heading heading)
        {
            int reportItem1 = position.X;
            int reportItem2 = position.Y;
            char reportItem3 = _headingDictionary[heading];
            var report = new StringBuilder();
            report.AppendFormat("{0} {1} {2}", reportItem1, reportItem2, reportItem3);
            return report.ToString();
        }

        public string GetReports(IEnumerable<IRobot> robots)
        {
            StringBuilder reports = ProduceAllReports(robots);
            return reports.ToString().TrimEnd('\n', '\r');
        }

        private StringBuilder ProduceAllReports(IEnumerable<IRobot> robots)
        {
            var reports = new StringBuilder();
            foreach (var robot in robots)
            {
                ConfirmRobotIsPlaced(robot);
                string report = ProduceReport(robot.Position, robot.Heading);
                reports.AppendLine(report);
            }
            return reports;
        }

        private static void ConfirmRobotIsPlaced(IRobot robot)
        {
            if (!robot.IsPlaced())
            {
                throw new ReporterException("Cannot create report because one or more robots are not placed");
            }
        }
    }
}