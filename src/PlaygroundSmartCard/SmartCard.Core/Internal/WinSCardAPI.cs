using System;
using System.Runtime.InteropServices;

namespace SmartCard.Core.Internal
{
    /// <summary>
    /// A class that wraps the functionality of winscard.dll and its methods.
    /// This class is internal to prevent external access to the SmartCard API.
    /// </summary>
    internal class WinSCardAPI
    {
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
    }
}