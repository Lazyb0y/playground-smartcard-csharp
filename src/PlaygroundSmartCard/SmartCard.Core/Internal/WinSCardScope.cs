namespace SmartCard.Core.Internal
{
    /// <summary>
    /// Represents the scope of a WinSCard operation.
    /// </summary>
    internal struct WinSCardScope
    {
        // ReSharper disable InconsistentNaming

        /// <summary>
        /// The scope is limited to the current user.
        /// </summary>
        public const uint SCARD_SCOPE_USER = 0x00000000;

        /// <summary>
        /// The scope is limited to the current terminal.
        /// </summary>
        public const uint SCARD_SCOPE_TERMINAL = 0x00000001;

        /// <summary>
        /// The scope is limited to the system.
        /// </summary>
        public const uint SCARD_SCOPE_SYSTEM = 0x00000002;

        /// <summary>
        /// The scope is global.
        /// </summary>
        public const uint SCARD_SCOPE_GLOBAL = 0x00000003;
    }
}