namespace Util.Logger
{
    /// <summary>
    /// Represent Logger interface
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="logLevel">The log level.</param>
        void LogMessage(string message, LogLevel logLevel);
    }
}