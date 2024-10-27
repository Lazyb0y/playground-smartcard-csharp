using System;

namespace SmartCard.Core.Logging
{
    /// <summary>
    /// Interface for log listeners to handle log messages and exceptions.
    /// </summary>
    public interface ILogListener
    {
        /// <summary>
        /// Logs a message with a specified log level.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="level">The level of the log message.</param>
        void Log(string message, LogLevel level);

        /// <summary>
        /// Logs an exception with a specified log level.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="level">The level of the log message.</param>
        void LogException(string message, Exception exception, LogLevel level);
    }
}