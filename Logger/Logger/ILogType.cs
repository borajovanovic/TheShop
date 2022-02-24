namespace Util.Logger
{
    /// <summary>
    /// Represetn LogType Interface
    /// </summary>
    public interface ILogType
    {
        /// <summary>
        /// Logs the specified log message.
        /// </summary>
        /// <param name="logMessage">The log message.</param>
        void Log(string logMessage);
        
    }
}
