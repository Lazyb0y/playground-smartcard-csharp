using System;

namespace SmartCard.Core
{
    /// <summary>
    /// Represents an exception that occurs in the SmartCard.Core namespace.
    /// </summary>
    public class SmartCardException : Exception
    {
        #region Property(s)

        /// <summary>
        /// Gets the error code associated with the exception.
        /// </summary>
        public string ErrorCode { get; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="SmartCardException"/> class with a specified error message and error code.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="errorCode">The error code associated with the exception.</param>
        public SmartCardException(string message, string errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SmartCardException"/> class with a specified error message, error code, and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="errorCode">The error code associated with the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public SmartCardException(string message, string errorCode, Exception innerException) : base(message, innerException)
        {
            ErrorCode = errorCode;
        }
    }
}