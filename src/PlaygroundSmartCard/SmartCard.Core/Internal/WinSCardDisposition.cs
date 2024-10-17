namespace SmartCard.Core.Internal
{
    /// <summary>
    /// Represents the actions that can be performed on a smart card.
    /// </summary>
    internal struct WinSCardDisposition
    {
        // ReSharper disable InconsistentNaming

        /// <summary>
        /// Leaves the smart card powered on and active in the card reader.
        /// </summary>
        public const uint SCARD_LEAVE_CARD = 0x00000000;

        /// <summary>
        /// Resets the smart card.
        /// </summary>
        public const uint SCARD_RESET_CARD = 0x00000001;

        /// <summary>
        /// Powers down the smart card.
        /// </summary>
        public const uint SCARD_UNPOWER_CARD = 0x00000002;

        /// <summary>
        /// Ejects the smart card from the card reader.
        /// </summary>
        public const uint SCARD_EJECT_CARD = 0x00000003;
    }
}