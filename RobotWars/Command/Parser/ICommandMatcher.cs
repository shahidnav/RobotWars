namespace RobotWars.Command.Parser
{
    public interface ICommandMatcher
    {
        CommandType Match(string inputLineToMatch);
    }
}