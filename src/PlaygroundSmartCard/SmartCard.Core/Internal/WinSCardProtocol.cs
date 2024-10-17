namespace SmartCard.Core.Internal
{
    /// <summary>
    /// Represents the WinSCard protocol constants.
    /// </summary>
    internal struct WinSCardProtocol
    {
        // ReSharper disable InconsistentNaming

        /// <summary>
        /// The undefined protocol.
        /// </summary>
        public const int SCARD_PROTOCOL_UNDEFINED = 0x00000000;

        /// <summary>
        /// The T=0 protocol.
        /// </summary>
        public const int SCARD_PROTOCOL_T0 = 0x00000001;

        /// <summary>
        /// The T=1 protocol.
        /// </summary>
        public const int SCARD_PROTOCOL_T1 = 0x00000002;

        /// <summary>
        /// The raw protocol.
        /// </summary>
        public const int SCARD_PROTOCOL_RAW = 0x00000004;

        /// <summary>
        /// The T=15 protocol.
        /// </summary>
        public const int SCARD_PROTOCOL_T15 = 0x00000008;
    }
}