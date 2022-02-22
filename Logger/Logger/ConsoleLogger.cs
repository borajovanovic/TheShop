using System;
using Util.Logger;

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
