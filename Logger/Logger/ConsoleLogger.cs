using System;

namespace Util.Logger
{
    public class ConsoleLogger : ILogType
    {
        public void Log(string logMessage)
        {
           Console.WriteLine(logMessage);
        }
    }
}
