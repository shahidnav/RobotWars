using System;
using System.Collections.Generic;
using RobotWars.Arena;
using RobotWars.Robot;

namespace RobotWars.Command
{
    public class CommandInvoker : ICommandInvoker
    {
        private readonly Func<IRobot> _robotFactory;
        private readonly IDictionary<CommandType, Action<ICommand>> _setReceiversMethodDictionary;

        private IBattleArena _battleArena;
        private IList<IRobot> _robots;
        private IEnumerable<ICommand> _commandList;

        public CommandInvoker(Func<IRobot> robotFactory)
        {
            _robotFactory = robotFactory;

            _setReceiversMethodDictionary = new Dictionary<CommandType, Action<ICommand>>
            {
                {CommandType.SetBattleArenaDimensions, SetReceiversOnSetBattleArenaDimensionsCommand},
                {CommandType.PlaceRobot, SetReceiversOnPlaceRobotCommand},
                {CommandType.MoveRobot, SetReceiversOnMoveRobotCommand}
            };
        }

        public void SetBattleArena(IBattleArena paramBattleArena)
        {
            _battleArena = paramBattleArena;
        }

        public void SetRobots(IList<IRobot> paramRobots)
        {
            _robots = paramRobots;
        }

        public void SetCommands(IEnumerable<ICommand> commands)
        {
            _commandList = commands;
        }

        public void Invoke()
        {
            foreach (var command in _commandList)
            {
                SetReceivers(command);
                command.Execute();
            }
        }

        private void SetReceivers(ICommand command)
        {
            _setReceiversMethodDictionary[command.GetCommandType()]
                .Invoke(command);
        }

        private void SetReceiversOnSetBattleArenaDimensionsCommand(ICommand command)
        {
            var setBattleArenaDimensionsCommand = (ISetBattleArenaDimensionsCommand)command;
            setBattleArenaDimensionsCommand.SetReceiver(_battleArena);
        }

        private void SetReceiversOnPlaceRobotCommand(ICommand command)
        {
            var placeRobotCommand = (IPlaceRobotCommand)command;
            var newRobot = _robotFactory();
            _robots.Add(newRobot);
            placeRobotCommand.SetReceivers(newRobot, _battleArena);
        }

        private void SetReceiversOnMoveRobotCommand(ICommand command)
        {
            var moveRobotCommand = (IMoveRobotCommand)command;
            var latestRobot = _robots[_robots.Count - 1];
            moveRobotCommand.SetReceiver(latestRobot);
        }
    }
}