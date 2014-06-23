using System.Collections.Generic;
using RobotWars.Arena;
using RobotWars.Command;
using RobotWars.Command.Parser;
using RobotWars.Report;
using RobotWars.Robot;

namespace RobotWars
{
    public class ControlSystem : IControlSystem
    {
        private readonly IBattleArena _battleArena;
        private readonly ICommandParser _commandParser;
        private readonly ICommandInvoker _commandInvoker;
        private readonly IReporter _reporter;
        private readonly IList<IRobot> _robots;

        public ControlSystem(IBattleArena battleArena, ICommandParser commandParser, ICommandInvoker commandInvoker, IReporter reporter)
        {
            _battleArena = battleArena;
            _robots = new List<IRobot>();
            _commandParser = commandParser;
            _commandInvoker = commandInvoker;
            _reporter = reporter;
            _commandInvoker.SetBattleArena(_battleArena);
            _commandInvoker.SetRobots(_robots);
        }

        public void Execute(string paramInput)
        {
            var commands = _commandParser.Execute(paramInput);
            _commandInvoker.SetCommands(commands);
            _commandInvoker.Invoke();
        }

        public IBattleArena GetBattleArena()
        {
            return _battleArena;
        }

        public string ProduceRobotsReport()
        {
            return _reporter.GetReports(_robots);
        }
    }
}