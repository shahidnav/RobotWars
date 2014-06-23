using System.Collections.Generic;

namespace RobotWars.Command.Parser
{
    public interface ICommandParser
    {
        IEnumerable<ICommand> Execute(string input);
    }
}
