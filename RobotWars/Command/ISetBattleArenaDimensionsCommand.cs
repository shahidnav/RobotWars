using RobotWars.Arena;

namespace RobotWars.Command
{
    public interface ISetBattleArenaDimensionsCommand : ICommand
    {
        Dimension Dimension { get; }
        void SetReceiver(IBattleArena paramBattleArena);
    }
}
