namespace SmartCard.Core.Logging
{
    /// <summary>
    /// Specifies the log level for logging messages.
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// Debug level, used for detailed debugging information.
        /// </summary>
        Debug,

        /// <summary>
        /// Info level, used for informational messages.
        /// </summary>
        Info,

        /// <summary>
        /// Warning level, used for potentially harmful situations.
        /// </summary>
        Warning,

        /// <summary>
        /// Error level, used for error events that might still allow the application to continue running.
        /// </summary>
        Error,

        /// <summary>
        /// Fatal level, used for very severe error events that will presumably lead the application to abort.
        /// </summary>
        Fatal
    }
}