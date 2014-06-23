using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using RobotWars.Arena;
using RobotWars.Robot;

namespace RobotWars.UnitTests
{
    public class BattleRobotTests
    {
        public class BattleRobot_Constructor
        {
            [Test]
            public void Initializes_with_IsDeployed_equal_to_false()
            {
                var robot = new BattleRobot();
                Assert.That(!robot.IsPlaced());
            }
        }

        [TestFixture]
        public class BattleRobot_Place
        {
            Mock<IBattleArena> _mockBattleArena;

            [SetUp]
            public void SetUp()
            {
                _mockBattleArena = new Mock<IBattleArena>();
            }

            [TestCase(1, 2, Heading.East)]
            [TestCase(3, 4, Heading.South)]
            public void Given_valid_point_and_heading_assigns_to_properties(int expectedX, int expectedY, Heading expectedHeading)
            {
                var expectedPoint = new Point(expectedX, expectedY);
                _mockBattleArena.Setup(x => x.IsValid(expectedPoint)).Returns(true);

                var robot = new BattleRobot();
                robot.Place(_mockBattleArena.Object, expectedPoint, expectedHeading);

                Assert.AreEqual(expectedPoint, robot.Position);
                Assert.AreEqual(expectedHeading, robot.Heading);
            }

            [Test]
            public void Confirms_place_point_is_valid_for_arena()
            {
                var aPoint = new Point(0, 0);
                _mockBattleArena.Setup(x => x.IsValid(aPoint)).Returns(true);
                var robot = new BattleRobot();
                robot.Place(_mockBattleArena.Object, aPoint, Heading.South);
                _mockBattleArena.Verify(x => x.IsValid(aPoint), Times.Once());
            }

            [Test]
            public void Given_invalid_place_point_throws_RobotPlaceException()
            {
                var point = new Point(0, 0);
                var dimension = new Dimension(0, 0);

                _mockBattleArena.Setup(x => x.IsValid(point)).Returns(false);
                _mockBattleArena.Setup(x => x.GetDimension()).Returns(dimension);
                var robot = new BattleRobot();

                Assert.Throws<RobotPlaceException>(() =>
                    robot.Place(_mockBattleArena.Object, point, Heading.West));
            }
        }

        [TestFixture]
        public class BattleRobot_Move
        {
            [TestCase(1, 1, Heading.South, Movement.Right, Movement.Right, Movement.Forward, 1, 2, Heading.North)]
            [TestCase(2, 4, Heading.East, Movement.Forward, Movement.Forward, Movement.Forward, 5, 4, Heading.East)]
            [TestCase(2, 2, Heading.West, Movement.Left, Movement.Forward, Movement.Forward, 2, 0, Heading.South)]
            [TestCase(4, 5, Heading.North, Movement.Left, Movement.Left, Movement.Left, 4, 5, Heading.East)]
            [TestCase(0, 0, Heading.South, Movement.Left, Movement.Forward, Movement.Forward, 2, 0, Heading.East)]
            public void Alters_position_and_direction_in_response_to_movement_list(int startX, int startY,
                Heading startHeading, Movement firstMove, Movement secondMove, Movement thirdMove,
                int expectedX, int expectedY, Heading expectedHeading)
            {
                var startPosition = new Point(startX, startY);
                var expectedPosition = new Point(expectedX, expectedY);
                var movements = new List<Movement> { firstMove, secondMove, thirdMove };

                var mockBattleArena = new Mock<IBattleArena>();
                mockBattleArena.Setup(x => x.IsValid(startPosition)).Returns(true);

                var robot = new BattleRobot();
                robot.Place(mockBattleArena.Object, startPosition, startHeading);
                robot.Move(movements);

                Assert.AreEqual(expectedPosition.X, robot.Position.X);
                Assert.AreEqual(expectedPosition.Y, robot.Position.Y);
                Assert.AreEqual(expectedHeading, robot.Heading);
            }
        }

        [TestFixture]
        public class BattleRobot_IsDeployed
        {
            Mock<IBattleArena> _mockBattleArena;

            [SetUp]
            public void SetUp()
            {
                _mockBattleArena = new Mock<IBattleArena>();
            }

            [Test]
            public void After_Robot_has_been_placed_returns_true()
            {
                var point = new Point(0, 0);
                _mockBattleArena.Setup(x => x.IsValid(point)).Returns(true);
                var robot = new BattleRobot();
                robot.Place(_mockBattleArena.Object, point, Heading.North);

                var isPlaced = robot.IsPlaced();

                Assert.That(isPlaced);
            }

            [Test]
            public void Before_Robot_has_been_placed_returns_false()
            {
                var point = new Point(0, 0);
                _mockBattleArena.Setup(x => x.IsValid(point)).Returns(true);
                var robot = new BattleRobot();

                var isPlaced = robot.IsPlaced();

                Assert.That(!isPlaced);
            }
        }
    }
}
