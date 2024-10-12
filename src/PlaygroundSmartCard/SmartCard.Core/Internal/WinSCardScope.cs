namespace SmartCard.Core.Internal
{
    /// <summary>
    /// Represents the scope of a WinSCard operation.
    /// </summary>
    internal struct WinSCardScope
    {
        /// <summary>
        /// The scope is limited to the current user.
        /// </summary>
        public const uint SCARD_SCOPE_USER = 0;

        /// <summary>
        /// The scope is limited to the current terminal.
        /// </summary>
        public const uint SCARD_SCOPE_TERMINAL = 1;

        /// <summary>
        /// The scope is limited to the system.
        /// </summary>
        public const uint SCARD_SCOPE_SYSTEM = 2;
    }
}