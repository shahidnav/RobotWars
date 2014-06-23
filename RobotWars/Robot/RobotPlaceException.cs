using System;

namespace RobotWars.Robot
{
    [Serializable]
    public class RobotPlaceException : Exception
    {
        public RobotPlaceException(string exceptionMessage) : base(exceptionMessage)
        {
        }
    }
}