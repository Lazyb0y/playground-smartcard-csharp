using System;
using System.Runtime.InteropServices;

namespace SmartCard.Core.WinSCard
{
    /// <summary>
    /// A class that wraps the functionality of winscard.dll and its methods.
    /// This class is internal to prevent external access to the SmartCard API.
    /// </summary>
    internal class WinSCardAPI
    {
        /// <summary>
        /// Establishes a connection to a smart card in a reader.
        /// </summary>
        /// <param name="hContext">A handle to the established resource manager context.</param>
        /// <param name="szReader">The name of the reader that contains the target card.</param>
        /// <param name="dwShareMode">The type of connection that you want to establish to the reader.</param>
        /// <param name="dwPreferredProtocols">The protocol(s) to use for communication with the card.</param>
        /// <param name="phCard">A handle that identifies the connection to the card.</param>
        /// <param name="pdwActiveProtocol">The actual protocol that is being used for communication with the card.</param>
        /// <returns>Returns zero on success; otherwise, returns a nonzero error code defined in WinError.h.</returns>
        [DllImport("winscard.dll", SetLastError = true)]
        internal static extern int SCardConnect(
            IntPtr hContext,
            string szReader,
            uint dwShareMode,
            uint dwPreferredProtocols,
            ref IntPtr phCard,
            ref int pdwActiveProtocol
        );

        /// <summary>
        /// Disconnects a connection from a smart card in a reader.
        /// </summary>
        /// <param name="hCard">A handle to the established connection to the card.</param>
        /// <param name="dwDisposition">Action to take on the card in the connected reader on close.</param>
        /// <returns>Returns zero on success; otherwise, returns a nonzero error code defined in WinError.h.</returns>
        [DllImport("winscard.dll", SetLastError = true)]
        internal static extern int SCardDisconnect(
            IntPtr hCard,
            uint dwDisposition
        );

        /// <summary>
        /// Establishes a connection to the smart card resource manager.
        /// </summary>
        /// <param name="dwScope">The scope of the resource manager context.</param>
        /// <param name="pvReserved1">Reserved for future use and must be set to IntPtr.Zero.</param>
        /// <param name="pvReserved2">Reserved for future use and must be set to IntPtr.Zero.</param>
        /// <param name="phContext">A handle to the established resource manager context.</param>
        /// <returns>Returns zero on success; otherwise, returns a nonzero error code defined in WinError.h.</returns>
        [DllImport("winscard.dll", SetLastError = true)]
        internal static extern int SCardEstablishContext(
            uint dwScope,
            IntPtr pvReserved1,
            IntPtr pvReserved2,
            ref IntPtr phContext
        );

        /// <summary>
        /// Retrieves an attribute from the specified smart card.
        /// </summary>
        /// <param name="hCard">A handle to the established connection to the card.</param>
        /// <param name="dwAttrId">The identifier of the attribute to retrieve.</param>
        /// <param name="pbAttr">A buffer that receives the attribute value.</param>
        /// <param name="pcbAttrLen">The length of the pbAttr buffer in bytes.</param>
        /// <returns>Returns zero on success; otherwise, returns a nonzero error code defined in WinError.h.</returns>
        [DllImport("winscard.dll", SetLastError = true)]
        internal static extern int SCardGetAttrib(
            IntPtr hCard,
            uint dwAttrId,
            [Out] byte[] pbAttr,
            ref uint pcbAttrLen
        );

        /// <summary>
        /// Retrieves the current status of the specified smart card readers.
        /// </summary>
        /// <param name="hContext">A handle to the established resource manager context.</param>
        /// <param name="dwTimeout">The maximum amount of time, in milliseconds, to wait for an action.</param>
        /// <param name="rgReaderStates">An array of <see cref="WinSCardReaderState"/> structures that specify the readers to watch, and that receives the current status of each reader.</param>
        /// <param name="cReaders">The number of elements in the rgReaderStates array.</param>
        /// <returns>Returns zero on success; otherwise, returns a nonzero error code defined in WinError.h.</returns>
        [DllImport("winscard.dll", SetLastError = true)]
        internal static extern int SCardGetStatusChange(
            IntPtr hContext,
            uint dwTimeout,
            [In, Out] WinSCardReaderState[] rgReaderStates,
            uint cReaders
        );

        /// <summary>
        /// Retrieves the list of smart card readers within a set of named reader groups.
        /// </summary>
        /// <param name="hContext">A handle to the established resource manager context.</param>
        /// <param name="mszGroups">A list of reader groups, where each string is terminated by a null character and the list is terminated with an additional null character.</param>
        /// <param name="mszReaders">A list of reader names, where each string is terminated by a null character and the list is terminated with an additional null character.</param>
        /// <param name="pcchReaders">The length of the mszReaders buffer in characters.</param>
        /// <returns>Returns zero on success; otherwise, returns a nonzero error code defined in WinError.h.</returns>
        [DllImport("winscard.dll", SetLastError = true)]
        internal static extern int SCardListReaders(
            IntPtr hContext,
            string mszGroups,
            IntPtr mszReaders,
            ref uint pcchReaders
        );

        /// <summary>
        /// Releases a connection to the smart card resource manager.
        /// </summary>
        /// <param name="hContext">A handle to the established resource manager context.</param>
        /// <returns>Returns zero on success; otherwise, returns a nonzero error code defined in WinError.h.</returns>
        [DllImport("winscard.dll", SetLastError = true)]
        internal static extern int SCardReleaseContext(
            IntPtr hContext
        );

        /// <summary>
        /// Retrieves the current status of a smart card.
        /// </summary>
        /// <param name="hCard">A handle to the established connection to the card.</param>
        /// <param name="mszReaderNames">A buffer that receives the names of the readers that contain the card.</param>
        /// <param name="pcchReaderLen">The length of the mszReaderNames buffer in characters.</param>
        /// <param name="pdwState">A buffer that receives the current state of the card.</param>
        /// <param name="pdwProtocol">A buffer that receives the current protocol, if any, being used for communication with the card.</param>
        /// <param name="pbAtr">A buffer that receives the ATR string of the card.</param>
        /// <param name="pcbAtrLen">The length of the pbAtr buffer in bytes.</param>
        /// <returns>Returns zero on success; otherwise, returns a nonzero error code defined in WinError.h.</returns>
        [DllImport("winscard.dll", SetLastError = true)]
        internal static extern int SCardStatus(
            IntPtr hCard,
            byte[] mszReaderNames,
            ref int pcchReaderLen,
            ref int pdwState,
            ref int pdwProtocol,
            byte[] pbAtr,
            ref int pcbAtrLen
        );

        /// <summary>
        /// Sends a command to the smart card and receives the response.
        /// </summary>
        /// <param name="hCard">A handle to the established connection to the card.</param>
        /// <param name="pioSendPci">A pointer to the protocol control information structure for the instruction.</param>
        /// <param name="pbSendBuffer">A buffer that contains the command to send to the card.</param>
        /// <param name="cbSendLength">The length, in bytes, of the pbSendBuffer buffer.</param>
        /// <param name="pioRecvPci">A pointer to the protocol control information structure for the response.</param>
        /// <param name="pbRecvBuffer">A buffer that receives the response from the card.</param>
        /// <param name="pcbRecvLength">The length, in bytes, of the pbRecvBuffer buffer. On input, this value specifies the size of the pbRecvBuffer buffer. On output, this value specifies the number of bytes received from the card.</param>
        /// <returns>Returns zero on success; otherwise, returns a nonzero error code defined in WinError.h.</returns>
        [DllImport("winscard.dll", SetLastError = true)]
        internal static extern int SCardTransmit(
            IntPtr hCard,
            ref WinSCardIORequest pioSendPci,
            byte[] pbSendBuffer,
            int cbSendLength,
            IntPtr pioRecvPci,
            byte[] pbRecvBuffer,
            ref int pcbRecvLength
        );
    }
}