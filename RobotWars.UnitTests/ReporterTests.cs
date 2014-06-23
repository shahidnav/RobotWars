using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using RobotWars.Report;
using RobotWars.Robot;

namespace RobotWars.UnitTests
{
    public class ReporterTests
    {
        [TestFixture]
        public class Reporter_GetReports
        {
            [Test]
            public void Requests_position_details_of_every_robot()
            {
                var mockedRobot = new Mock<IRobot>();
                mockedRobot.Setup(robot => robot.IsPlaced()).Returns(true);
                var robots = new List<IRobot>() { mockedRobot.Object };
                var reporter = new Reporter();

                reporter.GetReports(robots);

                mockedRobot.VerifyGet(robot => robot.Position);
                mockedRobot.VerifyGet(robot => robot.Heading);
                mockedRobot.Verify(robot => robot.IsPlaced(), Times.Once());
            }

            [Test]
            public void Given_a_robot_that_is_not_placed_then_throws_ReportException()
            {
                var mockedRobot = new Mock<IRobot>();
                mockedRobot.Setup(robot => robot.IsPlaced()).Returns(false);
                var robots = new List<IRobot>() {mockedRobot.Object};
                var reporter = new Reporter();

                Assert.Throws<ReporterException>(() => reporter.GetReports(robots));
            }
        }
    }
}
