namespace SmartCard.Core.Internal
{
    /// <summary>
    /// Represents the sharing mode for a smart card.
    /// </summary>
    internal struct WinSCardShare
    {
        // ReSharper disable InconsistentNaming

        /// <summary>
        /// Specifies that the smart card is exclusively accessed.
        /// </summary>
        public const uint SCARD_SHARE_EXCLUSIVE = 0x00000001;

        /// <summary>
        /// Specifies that the smart card is shared.
        /// </summary>
        public const uint SCARD_SHARE_SHARED = 0x00000002;

        /// <summary>
        /// Specifies that the smart card is accessed directly.
        /// </summary>
        public const uint SCARD_SHARE_DIRECT = 0x00000003;
    }
}