namespace RobotWars.Command
{
    public interface ICommand
    {
        CommandType GetCommandType();
        void Execute();
    }
}
