namespace SmartCard.Core
{
    // ReSharper disable once InconsistentNaming

    /// <summary>
    /// Represents the Class byte (CLA) in the APDU command.
    /// </summary>
    public struct CLA
    {
        // ReSharper disable InconsistentNaming

        /// <summary>
        /// Standard CLA byte value.
        /// </summary>
        public const byte STANDARD = 0x00;

        /// <summary>
        /// Proprietary CLA byte value.
        /// </summary>
        public const byte PROPRIETARY = 0x80;
    }
}