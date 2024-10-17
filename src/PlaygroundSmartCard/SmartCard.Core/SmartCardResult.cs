using System;

namespace SmartCard.Core
{
    /// <summary>
    /// Represents the result of a smart card operation.
    /// </summary>
    public class SmartCardResult
    {
        /// <summary>
        /// Gets or sets a value indicating whether the operation was successful.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the error code associated with the operation.
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// Gets or sets the error message associated with the operation.
        /// </summary>
        public string ErrorMessage { get; set; }
    }

    /// <summary>
    /// Represents the result of a smart card operation.
    /// </summary>
    /// <typeparam name="T">The type of data associated with the result.</typeparam>
    public class SmartCardResult<T> : SmartCardResult
    {
        /// <summary>
        /// Gets or sets the data associated with the result.
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Converts the current <see cref="SmartCardResult{T}"/> to a new <see cref="SmartCardResult{TResult}"/> using the specified converter function.
        /// </summary>
        /// <typeparam name="TResult">The type of data associated with the new result.</typeparam>
        /// <param name="converter">The converter function to convert the data.</param>
        /// <returns>A new <see cref="SmartCardResult{TResult}"/> with the converted data.</returns>
        public SmartCardResult<TResult> Convert<TResult>(Func<T, TResult> converter = null)
        {
            var convertedResult = new SmartCardResult<TResult>
            {
                Success = Success,
                ErrorCode = ErrorCode,
                ErrorMessage = ErrorMessage
            };

            if (converter != null)
            {
                convertedResult.Data = converter(Data);
            }

            return convertedResult;
        }
    }
}