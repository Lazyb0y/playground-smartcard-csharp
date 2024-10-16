namespace SmartCard.Core.Internal
{
    /// <summary>
    /// Represents the state of a WinSCard.
    /// </summary>
    internal struct WinSCardState
    {
        // ReSharper disable InconsistentNaming

        /// <summary>
        /// The unaware state of a WinSCard.
        /// </summary>
        public const uint SCARD_STATE_UNAWARE = 0x00000000;

        /// <summary>
        /// The changed state of a WinSCard.
        /// </summary>
        public const uint SCARD_STATE_CHANGED = 0x00000002;

        /// <summary>
        /// The not present state of a WinSCard.
        /// </summary>
        public const uint SCARD_STATE_EMPTY = 0x00000010;

        /// <summary>
        /// The present state of a WinSCard.
        /// </summary>
        public const uint SCARD_STATE_PRESENT = 0x00000020;
    }
}