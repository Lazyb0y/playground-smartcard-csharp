namespace SmartCard.Core.WinSCard
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
        /// The ignore state of a WinSCard.
        /// </summary>
        public const uint SCARD_STATE_IGNORE = 0x00000001;

        /// <summary>
        /// The changed state of a WinSCard.
        /// </summary>
        public const uint SCARD_STATE_CHANGED = 0x00000002;

        /// <summary>
        /// The unknown state of a WinSCard.
        /// </summary>
        public const uint SCARD_STATE_UNKNOWN = 0x00000004;

        /// <summary>
        /// The unavailable state of a WinSCard.
        /// </summary>
        public const uint SCARD_STATE_UNAVAILABLE = 0x00000008;

        /// <summary>
        /// The not present state of a WinSCard.
        /// </summary>
        public const uint SCARD_STATE_EMPTY = 0x00000010;

        /// <summary>
        /// The present state of a WinSCard.
        /// </summary>
        public const uint SCARD_STATE_PRESENT = 0x00000020;

        /// <summary>
        /// The ATR match state of a WinSCard.
        /// </summary>
        public const uint SCARD_STATE_ATRMATCH = 0x00000040;

        /// <summary>
        /// The exclusive state of a WinSCard.
        /// </summary>
        public const uint SCARD_STATE_EXCLUSIVE = 0x00000080;

        /// <summary>
        /// The in use state of a WinSCard.
        /// </summary>
        public const uint SCARD_STATE_INUSE = 0x00000100;

        /// <summary>
        /// The mute state of a WinSCard.
        /// </summary>
        public const uint SCARD_STATE_MUTE = 0x00000200;

        /// <summary>
        /// The unpowered state of a WinSCard.
        /// </summary>
        public const uint SCARD_STATE_UNPOWERED = 0x00000400;
    }
}