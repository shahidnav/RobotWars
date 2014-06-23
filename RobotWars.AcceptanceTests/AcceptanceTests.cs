using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using RobotWars.Arena;
using RobotWars.Command;
using RobotWars.Command.Parser;
using RobotWars.Report;
using RobotWars.Robot;

namespace RobotWars.AcceptanceTests
{
    [TestFixture]
    public class AcceptanceTests
    {
        private static IControlSystem ConstructControlSystem()
        {
            var battleArena = new BattleArena();
            var commandMatcher = new CommandMatcher();
            Func<Dimension, ISetBattleArenaDimensionsCommand> setBattleArenaCommandFactory =
                dimension => new SetBattleArenaDimensionsCommand(dimension);
            Func<Point, Heading, IPlaceRobotCommand> placeRobotCommandFactory =
                (point, heading) => new PlaceRobotCommand(point, heading);
            Func<List<Movement>, IMoveRobotCommand> moveRobotCommandFactory = list => new MoveRobotCommand(list);
            var commandParser = new CommandParser(commandMatcher, setBattleArenaCommandFactory, placeRobotCommandFactory,
                                                  moveRobotCommandFactory);
            var commandInvoker = new CommandInvoker(() => new BattleRobot());
            var reporter = new Reporter();
            return new ControlSystem(battleArena, commandParser, commandInvoker, reporter);
        }

        private static string BuildAcceptanceCriteriaOutput()
        {
            var outputBuilder = new StringBuilder();
            outputBuilder.AppendLine("1 3 N");
            outputBuilder.Append("5 1 E");

            return outputBuilder.ToString();
        }

        private static string BuildAcceptanceCriteriaInput()
        {
            var inputBuilder = new StringBuilder();
            inputBuilder.AppendLine("5 5");
            inputBuilder.AppendLine("1 2 N");
            inputBuilder.AppendLine("LMLMLMLMM");
            inputBuilder.AppendLine("3 3 E");
            inputBuilder.Append("MMRMMRMRRM");

            return inputBuilder.ToString();
        }

        [TestCase("1 10", 1, 10)]
        [TestCase("3 6", 3, 6)]
        [TestCase("5 5", 5, 5)]
        public void Given_input_in_the_format_of_coordinates_they_are_used_to_set_the_arena_size(string input,
                                                                                                 int expectedX,
                                                                                                 int expectedY)
        {
            var expectedDimension = new Dimension(expectedX, expectedY);
            IControlSystem controlSystem = ConstructControlSystem();

            controlSystem.Execute(input);
            IBattleArena battleArena = controlSystem.GetBattleArena();
            Dimension actualDimension = battleArena.GetDimension();

            Assert.AreEqual(expectedDimension, actualDimension,
                            "The system did not output the expected battle arena size for the set battle arena size instruction.");
        }

        [TestCase("1 2 N")]
        [TestCase("3 3 W")]
        [TestCase("2 4 S")]
        [TestCase("4 1 E")]
        public void
            Given_input_in_the_format_of_coordinates_and_a_heading_a_robot_is_placed_and_the_reported_final_coordinates_are_the_same
            (string input)
        {
            string finalInput = PrefixInputWithBattleArenaSetSize(input);
            IControlSystem controlSystem = ConstructControlSystem();

            controlSystem.Execute(finalInput);
            string actualOutput = controlSystem.ProduceRobotsReport();

            Assert.AreEqual(input, actualOutput,
                            "The system output did not match the expected output for the place robot instruction.");
        }

        private static string PrefixInputWithBattleArenaSetSize(string input)
        {
            var inputBuilder = new StringBuilder();
            inputBuilder.AppendLine("20 20");
            inputBuilder.Append(input);

            return inputBuilder.ToString();
        }

        [TestCase("LRMLR", "0 1 N")]
        [TestCase("MMMMM", "0 5 N")]
        [TestCase("LLLLL", "0 0 W")]
        public void
            Given_input_in_the_format_of_move_instructions_robots_move_and_reported_final_coordinates_reflect_this(
            string input, string expectedOutput)
        {
            string finalInput = PrefixInputWithBattleArenaSetSizeAndPlaceRobot(input);
            IControlSystem controlSystem = ConstructControlSystem();

            controlSystem.Execute(finalInput);
            string actualOutput = controlSystem.ProduceRobotsReport();

            Assert.AreEqual(expectedOutput, actualOutput,
                            "The system output did not match the expected output for the control robot instruction.");
        }

        private static string PrefixInputWithBattleArenaSetSizeAndPlaceRobot(string input)
        {
            var inputBuilder = new StringBuilder();
            inputBuilder.AppendLine("20 20");
            inputBuilder.AppendLine("0 0 N");
            inputBuilder.Append(input);

            return inputBuilder.ToString();
        }

        [Test]
        public void Given_test_input_solution_produces_expected_output_as_supplied_in_the_acceptance_criteria()
        {
            string input = BuildAcceptanceCriteriaInput();
            string expectedOutput = BuildAcceptanceCriteriaOutput();

            IControlSystem controlSystem = ConstructControlSystem();
            controlSystem.Execute(input);
            string actualOutput = controlSystem.ProduceRobotsReport();

            Assert.AreEqual(expectedOutput, actualOutput,
                            "The system robot report did not match the acceptance criteria.");
        }
    }
}