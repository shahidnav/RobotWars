using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using RobotWars.Arena;
using RobotWars.Command;
using RobotWars.Command.Parser;
using RobotWars.Robot;

namespace RobotWars.UnitTests.Command
{
    public class CommandParserTests
    {
        [TestFixture]
        public class CommandParser_Execute
        {
            [TestCase("5 5", 5, 5)]
            [TestCase("1 6", 1, 6)]
            [TestCase("15 54", 15, 54)]
            public void
                When_called_with_set_battle_arena_dimension_instruction_returns_SetBattleArenaDimensionsCommand_with_same_values
                (string input, int expectedX, int expectedY)
            {
                var expectedDimension = new Dimension(expectedX, expectedY);
                Mock<ICommandMatcher> mockedCommandMatcher =
                    CreateMockedCommandMatcher(CommandType.SetBattleArenaDimensions);
                var mockedSetBattleArenaDimensionsCommand = new Mock<ISetBattleArenaDimensionsCommand>();
                Func<Dimension, ISetBattleArenaDimensionsCommand> factory = dimension =>
                    {
                        mockedSetBattleArenaDimensionsCommand.Setup(command => command.Dimension).Returns(dimension);
                        return mockedSetBattleArenaDimensionsCommand.Object;
                    };
                var commandParser = new CommandParser(mockedCommandMatcher.Object, factory, null, null);

                var actualCommand = (ISetBattleArenaDimensionsCommand) commandParser.Execute(input).First();

                Assert.AreEqual(mockedSetBattleArenaDimensionsCommand.Object, actualCommand);
                Assert.AreEqual(expectedDimension, actualCommand.Dimension);
            }

            private static Mock<ICommandMatcher> CreateMockedCommandMatcher(CommandType commandType)
            {
                var mockedCommandMatcher = new Mock<ICommandMatcher>();
                mockedCommandMatcher.Setup(matcher => matcher.Match(It.IsAny<string>()))
                                    .Returns(commandType);
                return mockedCommandMatcher;
            }

            [TestCase("1 2 N", 1, 2, Heading.North)]
            [TestCase("1 3 E", 1, 3, Heading.East)]
            [TestCase("6 6 S", 6, 6, Heading.South)]
            [TestCase("2 7 W", 2, 7, Heading.West)]
            public void When_called_with_place_robot_instruction_returns_PlaceRobotCommand_with_same_values(
                string input, int expectedX, int expectedY, Heading expectedHeading)
            {
                var expectedPoint = new Point(expectedX, expectedY);
                Mock<ICommandMatcher> mockedCommandMatcher = CreateMockedCommandMatcher(CommandType.PlaceRobot);
                var mockedPlaceRobotCommand = new Mock<IPlaceRobotCommand>();
                Func<Point, Heading, IPlaceRobotCommand> factory = (point, heading) =>
                    {
                        mockedPlaceRobotCommand.Setup(command => command.Position).Returns(point);
                        mockedPlaceRobotCommand.Setup(command => command.Heading).Returns(heading);
                        return mockedPlaceRobotCommand.Object;
                    };
                var commandParser = new CommandParser(mockedCommandMatcher.Object, null, factory, null);

                var actualCommand = (IPlaceRobotCommand) commandParser.Execute(input).First();

                Assert.AreEqual(mockedPlaceRobotCommand.Object, actualCommand);
                Assert.AreEqual(expectedPoint, actualCommand.Position);
                Assert.AreEqual(expectedHeading, actualCommand.Heading);
            }

            [TestCase("LML", Movement.Left, Movement.Forward, Movement.Left)]
            [TestCase("MLM", Movement.Forward, Movement.Left, Movement.Forward)]
            [TestCase("LMM", Movement.Left, Movement.Forward, Movement.Forward)]
            public void When_called_with_move_robot_instructions_returns_MoveRobotCommand_with_same_values(string input,
                                                                                                           Movement
                                                                                                               firstMove,
                                                                                                           Movement
                                                                                                               secondMove,
                                                                                                           Movement
                                                                                                               thirdMove)
            {
                var expectedMovements = new List<Movement> {firstMove, secondMove, thirdMove};
                Mock<ICommandMatcher> mockedCommandMatcher = CreateMockedCommandMatcher(CommandType.MoveRobot);
                var mockedMoveRobotCommand = new Mock<IMoveRobotCommand>();
                Func<List<Movement>, IMoveRobotCommand> factory = (movements) =>
                    {
                        mockedMoveRobotCommand.Setup(command => command.Movements).Returns(movements);
                        return mockedMoveRobotCommand.Object;
                    };
                var commandParser = new CommandParser(mockedCommandMatcher.Object, null, null, factory);

                var actualCommand = (IMoveRobotCommand) commandParser.Execute(input).First();

                Assert.AreEqual(mockedMoveRobotCommand.Object, actualCommand);
                Assert.AreEqual(expectedMovements, actualCommand.Movements);
            }

            [Test]
            public void When_called_with_no_input_returns_no_commands()
            {
                var expectedCommands = new List<ICommand>();
                var commandParser = new CommandParser(null, null, null, null);

                IEnumerable<ICommand> actualCommands = commandParser.Execute(null);

                Assert.AreEqual(expectedCommands, actualCommands);
            }
        }
    }
}