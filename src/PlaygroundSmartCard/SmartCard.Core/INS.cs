namespace SmartCard.Core
{
    // ReSharper disable once InconsistentNaming

    /// <summary>
    /// Represents instruction bytes for various smart card operations.
    /// </summary>
    public struct INS
    {
        // ReSharper disable InconsistentNaming

        /// <summary>
        /// Instruction byte for verifying the PIN.
        /// </summary>
        public const byte VERIFY_PIN = 0x20;

        /// <summary>
        /// Instruction byte for changing the PIN.
        /// </summary>
        public const byte CHANGE_PIN = 0x24;

        /// <summary>
        /// Instruction byte for selecting a file.
        /// </summary>
        public const byte SELECT_FILE = 0xA4;

        /// <summary>
        /// Instruction byte for reading binary data.
        /// </summary>
        public const byte READ_BINARY = 0xB0;

        /// <summary>
        /// Instruction byte for writing binary data.
        /// </summary>
        public const byte WRITE_BINARY = 0xD0;
    }
}