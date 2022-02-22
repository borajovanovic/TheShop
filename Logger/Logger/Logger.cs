namespace Util.Logger
{
    public class Logger : ILogger
    {
        private readonly ILogType logType;

        public Logger(ILogType logType)
        {
            this.logType = logType;
        }

        public void LogMessage(string message, LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Info:
                    message = $"Info: {message}";
                    break;
                case LogLevel.Error:
                    message = $"Error: {message}";
                    break;
                case LogLevel.Debug:
                    message = $"Debug: {message}";
                    break;
                default:
                    break;
            }

            this.logType.Log(message);
        }

    }

}
