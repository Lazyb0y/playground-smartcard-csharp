using System;
using System.Collections.Generic;

namespace SmartCard.Core.Logging
{
    /// <summary>
    /// Provides logging functionalities for the SmartCard library.
    /// </summary>
    public class Logger
    {
        #region Decleration(s)

        /// <summary>
        /// List of registered log listeners.
        /// </summary>
        private static readonly List<ILogListener> LogListeners = new List<ILogListener>();

        #endregion

        #region Property(s)

        /// <summary>
        /// Gets or sets the current log level.
        /// </summary>
        public static LogLevel CurrentLogLevel { get; set; } = LogLevel.Info;

        #endregion

        #region Method(s)

        /// <summary>
        /// Registers an external log listener.
        /// </summary>
        /// <param name="listener">The log listener to register.</param>
        public static void RegisterListener(ILogListener listener)
        {
            if (!LogListeners.Contains(listener))
            {
                LogListeners.Add(listener);
            }
        }

        /// <summary>
        /// Logs a message with the specified log level.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="level">The log level of the message.</param>
        internal static void Log(string message, LogLevel level)
        {
            if (level < CurrentLogLevel)
            {
                return;
            }

            var timestampedMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} [{level}] - {message}";

            foreach (var listener in LogListeners)
            {
                listener.Log(timestampedMessage, level);
            }
        }

        /// <summary>
        /// Logs an exception with the specified log level.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="e">The exception to log.</param>
        /// <param name="level">The log level of the message. Default is <see cref="LogLevel.Error"/>.</param>
        internal static void LogException(string message, Exception e, LogLevel level = LogLevel.Error)
        {
            foreach (var listener in LogListeners)
            {
                listener.LogException(message, e, level);
            }
        }

        /// <summary>
        /// Logs a debug message.
        /// </summary>
        /// <param name="message">The debug message to log.</param>
        internal static void Debug(string message) => Log(message, LogLevel.Debug);

        /// <summary>
        /// Logs an informational message.
        /// </summary>
        /// <param name="message">The informational message to log.</param>
        internal static void Info(string message) => Log(message, LogLevel.Info);

        /// <summary>
        /// Logs a warning message.
        /// </summary>
        /// <param name="message">The warning message to log.</param>
        internal static void Warning(string message) => Log(message, LogLevel.Warning);

        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <param name="message">The error message to log.</param>
        internal static void Error(string message) => Log(message, LogLevel.Error);

        /// <summary>
        /// Logs a fatal error message.
        /// </summary>
        /// <param name="message">The fatal error message to log.</param>
        internal static void Fatal(string message) => Log(message, LogLevel.Fatal);

        /// <summary>
        /// Logs a fatal error message along with an exception.
        /// </summary>
        /// <param name="message">The fatal error message to log.</param>
        /// <param name="e">The exception to log.</param>
        internal static void Fatal(string message, Exception e) => LogException(message, e, LogLevel.Fatal);

        /// <summary>
        /// Logs an APDU command.
        /// </summary>
        /// <param name="command">The APDU command to log.</param>
        internal static void LogAPDUCommand(APDUCommand command)
        {
            Debug($"APDU Command Sent: {BitConverter.ToString(command.ToBytes())}");
        }

        /// <summary>
        /// Logs an APDU response.
        /// </summary>
        /// <param name="response">The APDU response to log.</param>
        internal static void LogAPDUResponse(APDUResponse response)
        {
            Debug(
                $"APDU Response Received: {BitConverter.ToString(response.Data)}, SW1: {response.SW1:X2}, SW2: {response.SW2:X2}");
        }

        #endregion
    }
}