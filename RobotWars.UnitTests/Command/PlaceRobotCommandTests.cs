using Moq;
using NUnit.Framework;
using RobotWars.Arena;
using RobotWars.Command;
using RobotWars.Robot;

namespace RobotWars.UnitTests.Command
{
    public class PlaceRobotCommandTests
    {
        [TestFixture]
        public class PlaceRobotCommand_Constructor
        {
            [TestCase(0, 1, Heading.North)]
            [TestCase(2, 3, Heading.West)]
            public void Given_position_and_heading_exposes_sets_properties(int expectedX, int expectedY, Heading expectedHeading)
            {
                var expectedPosition = new Point(expectedX, expectedY);

                var placeRobotCommand = new PlaceRobotCommand(expectedPosition, expectedHeading);

                Assert.AreEqual(expectedPosition, placeRobotCommand.Position);
                Assert.AreEqual(expectedHeading, placeRobotCommand.Heading);
            }
        }

        [TestFixture]
        public class PlaceRobotCommand_SetReceivers
        {
            [Test]
            public void Accepts_Receivers()
            {
                const Heading heading = Heading.South;
                var position = new Point(0, 0);
                var mockRobot = new Mock<IRobot>();
                var mockBattleArena = new Mock<IBattleArena>();
                var placeRobotCommand = new PlaceRobotCommand(position, heading);
                Assert.DoesNotThrow(() =>
                    placeRobotCommand.SetReceivers(mockRobot.Object, mockBattleArena.Object));
            }
        }

        [TestFixture]
        public class PlaceRobotCommand_Execute
        {
            [Test]
            public void Invokes_PlaceRobot()
            {
                const Heading heading = Heading.North;
                var position = new Point(0, 0);

                var mockRobot = new Mock<IRobot>();
                var mockBattleArena = new Mock<IBattleArena>();
                var placeRobotCommand = new PlaceRobotCommand(position, heading);
                placeRobotCommand.SetReceivers(mockRobot.Object, mockBattleArena.Object);

                placeRobotCommand.Execute();

                mockRobot.Verify(x =>
                    x.Place(mockBattleArena.Object, position, heading), Times.Once());
            }
        }

        [TestFixture]
        public class PlaceRobotCommand_GetCommandType
        {
            [Test]
            public void Returns_PlaceRobotCommand_type()
            {
                var position = new Point(0, 0);
                var roverDeployCommand = new PlaceRobotCommand(position, Heading.West);
                Assert.AreEqual(roverDeployCommand.GetCommandType(), CommandType.PlaceRobot);
            }
        }
    }
}
