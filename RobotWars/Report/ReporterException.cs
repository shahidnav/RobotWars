using System;

namespace RobotWars.Report
{
    [Serializable]
    public class ReporterException : Exception
    {
        public ReporterException(string message) : base(message)
        {
        }
    }
}
