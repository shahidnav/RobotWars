using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using RobotWars.Command;
using RobotWars.Robot;

namespace RobotWars.UnitTests.Command
{
    public class MoveRobotCommandTests
    {
        [TestFixture]
        public class MoveRobotCommand_Constructor
        {
            [Test]
            public void Given_list_of_movements_sets_property()
            {
                var expectedMovements = new List<Movement> { Movement.Left, Movement.Right };

                var moveRobotCommand = new MoveRobotCommand(expectedMovements);

                Assert.AreEqual(expectedMovements, moveRobotCommand.Movements);
            }
        }

        [TestFixture]
        public class MoveRobotCommand_SetReceiver
        {
            [Test]
            public void Accepts_Receiver()
            {
                var mockRobot = new Mock<IRobot>();
                var moveRobotCommand = new MoveRobotCommand(null);
                Assert.DoesNotThrow(() =>
                    moveRobotCommand.SetReceiver(mockRobot.Object));
            }
        }

        [TestFixture]
        public class MoveRobotCommand_Execute
        {
            [Test]
            public void Invokes_Robot_Move()
            {
                var expectedMovements = new List<Movement> { Movement.Left, Movement.Right };
                var mockRobot = new Mock<IRobot>();
                var moveRobotCommand = new MoveRobotCommand(expectedMovements);
                moveRobotCommand.SetReceiver(mockRobot.Object);

                moveRobotCommand.Execute();

                mockRobot.Verify(x =>
                    x.Move(expectedMovements), Times.Once());
            }
        }

        [TestFixture]
        public class MoveRobotCommand_GetCommandType
        {
            [Test]
            public void Returns_MoveRobotCommand_type()
            {
                var moveRobotCommand = new MoveRobotCommand(null);
                Assert.AreEqual(moveRobotCommand.GetCommandType(), CommandType.MoveRobot);
            }
        }
    }
}
