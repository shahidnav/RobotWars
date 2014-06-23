using System;

namespace RobotWars.Command.Parser
{
    [Serializable]
    public class MatchNotFoundException : Exception
    {
        public MatchNotFoundException(string message, Exception innerException)
            :base(message, innerException)
        {
        }
    }
}
