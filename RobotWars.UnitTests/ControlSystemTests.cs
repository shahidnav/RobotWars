using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using RobotWars.Arena;
using RobotWars.Command;
using RobotWars.Command.Parser;
using RobotWars.Report;
using RobotWars.Robot;

namespace RobotWars.UnitTests
{
    public class ControlSystemTests
    {
        [TestFixture]
        public class ControlSystem_Constructor
        {
            [Test]
            public void Sets_the_battlearena_on_the_command_invoker()
            {
                var mockedBattleArena = new Mock<IBattleArena>();
                var mockedCommandInvoker = new Mock<ICommandInvoker>();
                var controlSystem = new ControlSystem(mockedBattleArena.Object, null, mockedCommandInvoker.Object, null);

                mockedCommandInvoker.Verify(invoker => invoker.SetBattleArena(mockedBattleArena.Object), Times.Once());
            }

            [Test]
            public void Sets_the_robots_on_the_command_invoker()
            {
                var mockedCommandInvoker = new Mock<ICommandInvoker>();
                var controlSystem = new ControlSystem(null, null, mockedCommandInvoker.Object, null);

                mockedCommandInvoker.Verify(invoker => invoker.SetRobots(It.IsAny<IList<IRobot>>()), Times.Once());
            }
        }

        [TestFixture]
        public class ControlSystem_Execute
        {
            [Test]
            public void Parses_input_into_commands_and_invokes_them_all()
            {
                var commands = new List<ICommand>();
                var mockedBattleArena = Mock.Of<IBattleArena>();
                var mockedCommandParser = new Mock<ICommandParser>();
                mockedCommandParser.Setup(parser => parser.Execute(null)).Returns(commands);
                var mockedCommandInvoker = new Mock<ICommandInvoker>();
                var controlSystem = new ControlSystem(mockedBattleArena, mockedCommandParser.Object,
                                                      mockedCommandInvoker.Object, null);

                controlSystem.Execute(null);

                mockedCommandInvoker.Verify(invoker => invoker.SetCommands(commands), Times.Once());
                mockedCommandInvoker.Verify(invoker => invoker.Invoke(), Times.Once());
            }
        }

        [TestFixture]
        public class ControlSystem_GetBattleArena
        {
            [Test]
            public void Returns_the_injected_battle_arena()
            {
                var mockedBattleArena = Mock.Of<IBattleArena>();
                var controlSystem = new ControlSystem(mockedBattleArena, null, Mock.Of<ICommandInvoker>(), null);

                IBattleArena actualBattleArena = controlSystem.GetBattleArena();

                Assert.AreEqual(mockedBattleArena, actualBattleArena);
            }
        }

        [TestFixture]
        public class ControlSystem_ProduceRobotsReport
        {
            [Test]
            public void Calls_GetReports_on_reporter()
            {
                var mockedReporter = new Mock<IReporter>();
                var controlSystem = new ControlSystem(null, null, Mock.Of<ICommandInvoker>(), mockedReporter.Object);

                controlSystem.ProduceRobotsReport();

                mockedReporter.Verify(reporter => reporter.GetReports(It.IsAny<IList<IRobot>>()), Times.Once());
            }
        }
    }
}