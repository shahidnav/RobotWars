namespace RobotWars.Arena
{
    public class BattleArena : IBattleArena
    {
        private Dimension _dimension;

        public void SetDimension(Dimension paramDimension)
        {
            _dimension = paramDimension;
        }

        public Dimension GetDimension()
        {
            return _dimension;
        }

        public bool IsValid(Point point)
        {
            var isValidX = point.X >= 0 && point.X <= _dimension.Width;
            var isValidY = point.Y >= 0 && point.Y <= _dimension.Length;
            return isValidX && isValidY;
        }
    }
}