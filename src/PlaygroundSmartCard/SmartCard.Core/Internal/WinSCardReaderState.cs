using System;
using System.Runtime.InteropServices;

namespace SmartCard.Core.Internal
{
    /// <summary>
    /// Represents the state of a smart card reader.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct WinSCardReaderState
    {
        /// <summary>
        /// The name of the smart card reader.
        /// </summary>
        public string ReaderName;

        /// <summary>
        /// User-defined data associated with the reader.
        /// </summary>
        public IntPtr UserData;

        /// <summary>
        /// The current state of the reader.
        /// </summary>
        public uint CurrentState;

        /// <summary>
        /// The state of the reader after a state change event.
        /// </summary>
        public uint EventState;

        /// <summary>
        /// The length of the ATR (Answer To Reset) of the smart card.
        /// </summary>
        public uint AtrLength;

        /// <summary>
        /// The ATR (Answer To Reset) of the smart card.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 36)]
        public byte[] Atr;
    }
}