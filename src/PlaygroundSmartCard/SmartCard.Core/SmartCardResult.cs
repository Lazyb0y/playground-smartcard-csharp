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
    public class SmartCardResult<T>
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

        /// <summary>
        /// Gets or sets the data associated with the result.
        /// </summary>
        public T Data { get; set; }
    }
}