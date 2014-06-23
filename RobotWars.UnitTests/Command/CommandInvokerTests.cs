using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using RobotWars.Arena;
using RobotWars.Command;
using RobotWars.Robot;

namespace RobotWars.UnitTests.Command
{
    public class CommandInvokerTests
    {
        [TestFixture]
        public class CommandInvoker_SetCommands
        {
            [Test]
            public void Accepts_commands_list()
            {
                var commands = new List<ICommand>();
                var commandInvoker = new CommandInvoker(null);
                Assert.DoesNotThrow(() =>
                    commandInvoker.SetCommands(commands));
            }
        }

        [TestFixture]
        public class CommandInvoker_SetBattleArena
        {
            [Test]
            public void Accepts_battleArena()
            {
                var mockBattleArena = new Mock<IBattleArena>();
                var commandInvoker = new CommandInvoker(null);
                Assert.DoesNotThrow(() =>
                    commandInvoker.SetBattleArena(mockBattleArena.Object));
            }
        }

        [TestFixture]
        public class CommandInvoker_SetRobots
        {
            [Test]
            public void Accepts_list_of_Robots()
            {
                var robots = new List<IRobot>();
                var commandInvoker = new CommandInvoker(null);
                Assert.DoesNotThrow(() =>
                    commandInvoker.SetRobots(robots));
            }
        }

        [TestFixture]
        public class CommandInvoker_Invoke
        {
            [Test]
            public void When_executing_SetBattleArenaDimensionsCommand_sets_battlearena_as_receiver()
            {
                var expectedBattleArena = new Mock<IBattleArena>();
                var mockSetBattleArenaDimensionsCommand = new Mock<ISetBattleArenaDimensionsCommand>();
                mockSetBattleArenaDimensionsCommand.Setup(x => x.GetCommandType()).Returns(CommandType.SetBattleArenaDimensions);

                var commandInvoker = new CommandInvoker(null);
                commandInvoker.SetCommands(new List<ICommand>() {mockSetBattleArenaDimensionsCommand.Object});
                commandInvoker.SetBattleArena(expectedBattleArena.Object);

                commandInvoker.Invoke();

                mockSetBattleArenaDimensionsCommand.Verify(
                    x => x.SetReceiver(expectedBattleArena.Object), Times.Once());
            }

            [Test]
            public void When_executing_PlaceRobotCommand_sets_battlearena_and_new_battlerobot_as_receivers()
            {
                var expectedRobot = new Mock<IRobot>();
                var expectedBattleArena = new Mock<IBattleArena>();

                var mockPlaceRobotCommand = new Mock<IPlaceRobotCommand>();
                mockPlaceRobotCommand.Setup(x => x.GetCommandType()).Returns(CommandType.PlaceRobot);

                Func<IRobot> mockRobotFactory = () => expectedRobot.Object;

                var commandInvoker = new CommandInvoker(mockRobotFactory);
                commandInvoker.SetCommands(new List<ICommand>() { mockPlaceRobotCommand.Object });
                commandInvoker.SetBattleArena(expectedBattleArena.Object);
                commandInvoker.SetRobots(new List<IRobot>());

                commandInvoker.Invoke();

                mockPlaceRobotCommand.Verify(
                    x => x.SetReceivers(expectedRobot.Object, expectedBattleArena.Object), Times.Once());
            }

            [Test]
            public void When_executing_MoveRobotCommand_sets_most_recently_added_Robot_as_receiver()
            {
                var expectedRobot = new Mock<IRobot>();

                var mockMoveRobotCommand = new Mock<IMoveRobotCommand>();
                mockMoveRobotCommand.Setup(x => x.GetCommandType()).Returns(CommandType.MoveRobot);

                var commandInvoker = new CommandInvoker(null);
                commandInvoker.SetCommands(new List<ICommand>() {mockMoveRobotCommand.Object});
                commandInvoker.SetRobots(new List<IRobot> { null, expectedRobot.Object });

                commandInvoker.Invoke();

                mockMoveRobotCommand.Verify(
                    x => x.SetReceiver(expectedRobot.Object), Times.Once());
            }

            [Test]
            public void Executes_all_commands()
            {
                var mockCommand = new Mock<ISetBattleArenaDimensionsCommand>();
                mockCommand.Setup(x => x.GetCommandType()).Returns(CommandType.SetBattleArenaDimensions);

                var commandInvoker = new CommandInvoker(null);
                commandInvoker.SetCommands(new[] { mockCommand.Object, mockCommand.Object, mockCommand.Object });

                commandInvoker.Invoke();

                mockCommand.Verify(x => x.Execute(), Times.Exactly(3));
            }
        }
    }
}
