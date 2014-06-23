using NUnit.Framework;
using RobotWars.Command;
using RobotWars.Command.Parser;

namespace RobotWars.UnitTests.Command
{
    public class CommandMatcherTests
    {
        [TestFixture]
        public class CommandMatcher_Match
        {
            [TestCase("")]
            [TestCase(null)]
            [TestCase("sdflkgj")]
            [TestCase("345")]
            [TestCase("-3 5")]
            [TestCase("4 4 4 4 4 4 ")]
            [TestCase("L L L L L")]
            [TestCase("4 4 Y")]
            [TestCase("4 -4 E")]
            [TestCase("3 45 N J")]
            [TestCase("LLLXLLMMRR")]
            public void When_given_invalid_input_throws_MatchNotFoundException(string toMatch)
            {
                var commandMatcher = new CommandMatcher();

                Assert.Throws<MatchNotFoundException>(() => commandMatcher.Match(toMatch),
                                                      string.Format(
                                                          "Failed to throw match exception for invalid input: '{0}'",
                                                          toMatch));
            }

            [TestCase("5 5", CommandType.SetBattleArenaDimensions)]
            [TestCase("5 5 N", CommandType.PlaceRobot)]
            [TestCase("LLLRRMM", CommandType.MoveRobot)]
            [TestCase("23 23", CommandType.SetBattleArenaDimensions)]
            [TestCase("1 1 E", CommandType.PlaceRobot)]
            [TestCase("MMMMLLLRR", CommandType.MoveRobot)]
            public void When_given_valid_input_identifies_the_correct_command_type(string toMatch,
                                                                                   CommandType expectedCommandType)
            {
                var commandMatcher = new CommandMatcher();

                CommandType actualCommandType = commandMatcher.Match(toMatch);

                Assert.AreEqual(expectedCommandType, actualCommandType,
                                "The command matcher failed to match the input to the correct command type.");
            }
        }
    }
}