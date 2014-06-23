using RobotWars.Arena;

namespace RobotWars.Command
{
    public class SetBattleArenaDimensionsCommand : ISetBattleArenaDimensionsCommand
    {
        private IBattleArena _battleArena;

        public SetBattleArenaDimensionsCommand(Dimension dimension)
        {
            Dimension = dimension;
        }

        public Dimension Dimension { get; private set; }

        public void SetReceiver(IBattleArena paramBattleArena)
        {
            _battleArena = paramBattleArena;
        }

        public CommandType GetCommandType()
        {
            return CommandType.SetBattleArenaDimensions;
        }

        public void Execute()
        {
            _battleArena.SetDimension(Dimension);
        }
    }
}