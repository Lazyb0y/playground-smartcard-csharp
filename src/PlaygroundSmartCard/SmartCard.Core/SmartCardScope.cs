namespace SmartCard.Core
{
    /// <summary>
    /// Represents the scope of a smart card.
    /// </summary>
    public enum SmartCardScope : uint
    {
        /// <summary>
        /// The smart card is accessible only to the current user.
        /// </summary>
        User = 0,

        /// <summary>
        /// The smart card is accessible only to the current terminal.
        /// </summary>
        Terminal = 1,

        /// <summary>
        /// The smart card is accessible to the entire system.
        /// </summary>
        System = 2
    }
}