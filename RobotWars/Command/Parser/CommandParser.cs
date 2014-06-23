using System;
using System.Collections.Generic;
using System.Linq;
using RobotWars.Arena;
using RobotWars.Robot;

namespace RobotWars.Command.Parser
{
    public class CommandParser : ICommandParser
    {
        private readonly ICommandMatcher _commandMatcher;
        private readonly IDictionary<CommandType, Func<string, ICommand>> _commandParserDictionary;
        private readonly Dictionary<string, Heading> _headingDictionary;
        private readonly Func<List<Movement>, IMoveRobotCommand> _moveRobotCommandfactory;
        private readonly Dictionary<char, Movement> _movementDictionary;
        private readonly Func<Point, Heading, IPlaceRobotCommand> _placeRobotCommandFactory;
        private readonly Func<Dimension, ICommand> _setBattleArenaDimensionCommandFactory;

        public CommandParser(ICommandMatcher commandMatcher,
                             Func<Dimension, ISetBattleArenaDimensionsCommand> setBattleArenaDimensionCommandFactory,
                             Func<Point, Heading, IPlaceRobotCommand> placeRobotCommandFactory,
                             Func<List<Movement>, IMoveRobotCommand> moveRobotCommandfactory)
        {
            _commandMatcher = commandMatcher;
            _setBattleArenaDimensionCommandFactory = setBattleArenaDimensionCommandFactory;
            _placeRobotCommandFactory = placeRobotCommandFactory;
            _moveRobotCommandfactory = moveRobotCommandfactory;
            _commandParserDictionary = new Dictionary<CommandType, Func<string, ICommand>>
                {
                    {CommandType.SetBattleArenaDimensions, ParseSetBattleArenaDimensionsCommand},
                    {CommandType.PlaceRobot, ParsePlaceRobotCommand},
                    {CommandType.MoveRobot, ParseMoveRobotCommand}
                };

            _headingDictionary = new Dictionary<string, Heading>
                {
                    {"N", Heading.North},
                    {"E", Heading.East},
                    {"S", Heading.South},
                    {"W", Heading.West}
                };

            _movementDictionary = new Dictionary<char, Movement>
                {
                    {'L', Movement.Left},
                    {'R', Movement.Right},
                    {'M', Movement.Forward}
                };
        }

        public IEnumerable<ICommand> Execute(string input)
        {
            var commands = new List<ICommand>();

            if (input != null)
            {
                string[] toParse = input.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                return toParse.Select(s => _commandParserDictionary[_commandMatcher.Match(s)].Invoke(s)).ToList();
            }

            return commands;
        }

        private ICommand ParseMoveRobotCommand(string input)
        {
            char[] parameters = input.ToCharArray();
            List<Movement> movements = parameters.Select(c => _movementDictionary[c]).ToList();
            return _moveRobotCommandfactory(movements);
        }

        private ICommand ParsePlaceRobotCommand(string input)
        {
            string[] parameters = input.Split(' ');
            int x = int.Parse(parameters[0]);
            int y = int.Parse(parameters[1]);
            var point = new Point(x, y);
            Heading heading = _headingDictionary[parameters[2]];
            return _placeRobotCommandFactory(point, heading);
        }

        private ICommand ParseSetBattleArenaDimensionsCommand(string input)
        {
            string[] parameters = input.Split(' ');
            int width = int.Parse(parameters[0]);
            int length = int.Parse(parameters[1]);
            var dimension = new Dimension(width, length);
            return _setBattleArenaDimensionCommandFactory(dimension);
        }
    }
}