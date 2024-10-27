using System.Runtime.InteropServices;

namespace SmartCard.Core.WinSCard
{
    /// <summary>
    /// Represents the I/O request structure for smart card communication.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct WinSCardIORequest
    {
        /// <summary>
        /// Protocol identifier.
        /// </summary>
        public int dwProtocol;

        /// <summary>
        /// Length of the protocol control information.
        /// </summary>
        public int cbPciLength;
    }
}