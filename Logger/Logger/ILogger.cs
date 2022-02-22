namespace Util.Logger
{
    public interface ILogger
    {
        void LogMessage(string message, LogLevel logLevel);
    }
}