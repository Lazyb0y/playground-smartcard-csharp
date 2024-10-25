namespace SmartCard.Core
{
    /// <summary>
    /// Represents the result of an authentication attempt.
    /// </summary>
    public enum AuthenticationStatus
    {
        /// <summary>
        /// Authentication was successful.
        /// </summary>
        Success,

        /// <summary>
        /// A failure occurred during authentication.
        /// </summary>
        Failure,

        /// <summary>
        /// The provided PIN was incorrect.
        /// </summary>
        IncorrectPin,

        /// <summary>
        /// The PIN has been blocked due to multiple incorrect attempts.
        /// </summary>
        PinBlocked,
    }
}