using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using SmartCard.Core.Internal;

namespace SmartCard.Core
{
    /// <summary>
    /// Represents a smart card reader used to interact with smart cards.
    /// </summary>
    /// <remarks>
    /// This class provides functionality for discovering available smart card readers, 
    /// communicating with smart cards, and handling related operations.
    /// </remarks>
    public class SmartCardReader : IDisposable
    {
        #region Declearation(s)

        private bool _disposed;
        private readonly SmartCardContext _context;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="SmartCardReader"/> class with the specified <see cref="SmartCardScope"/>.
        /// </summary>
        /// <param name="scope">The scope of the smart card reader.</param>
        public SmartCardReader(SmartCardScope scope)
        {
            _context = new SmartCardContext(scope);

            var result = _context.Establish();
            if (!result.Success)
            {
                throw new SmartCardException(result.ErrorMessage, result.ErrorCode);
            }
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="SmartCardReader"/> class.
        /// </summary>
        ~SmartCardReader()
        {
            Dispose(false);
        }

        #endregion

        #region Method(s)

        /// <summary>
        /// Retrieves a list of smart card reader names.
        /// </summary>
        /// <returns>A <see cref="SmartCardResult{T}"/> containing a list of smart card reader names.</returns>
        public SmartCardResult<List<string>> GetReaderNames()
        {
            uint pcchReaders = 0;

            // First call to determine the buffer size for the readers list
            var result = WinSCardAPI.SCardListReaders(_context.Context, null, IntPtr.Zero, ref pcchReaders);
            if (result != WinSCardError.SCARD_S_SUCCESS)
            {
                return SmartCardResultHelper<List<string>>.CreateErrorResult(result);
            }

            if (pcchReaders == 0)
            {
                return SmartCardResultHelper<List<string>>.CreateSuccessResult(new List<string>());
            }

            var readerNames = Marshal.AllocHGlobal((int)pcchReaders);

            try
            {
                // Second call to actually get the readers list
                result = WinSCardAPI.SCardListReaders(_context.Context, null, readerNames, ref pcchReaders);
                if (result != WinSCardError.SCARD_S_SUCCESS)
                {
                    return SmartCardResultHelper<List<string>>.CreateErrorResult(result);
                }

                var readerList = new List<string>();

                // Read the multi-string from the unmanaged buffer
                var offset = 0;
                while (offset < pcchReaders)
                {
                    var readerName = Marshal.PtrToStringAnsi(readerNames + offset);
                    if (string.IsNullOrEmpty(readerName))
                    {
                        break;
                    }

                    readerList.Add(readerName);

                    // Move offset by the length of the string plus the null terminator
                    offset += readerName.Length + 1;
                }

                return SmartCardResultHelper<List<string>>.CreateSuccessResult(readerList);
            }
            finally
            {
                Marshal.FreeHGlobal(readerNames);
            }
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        /// <param name="disposing">A boolean value indicating whether to dispose of managed resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Free managed resources here
                }

                try
                {
                    var result = _context.Release();
                    if (!result.Success)
                    {
                        Console.WriteLine(
                            $"An error occurred during disposal of context: [{result.ErrorCode}] {result.ErrorMessage}");
                    }
                }
                catch (Exception x)
                {
                    Console.WriteLine($"An error occurred during disposal: {x}");
                }

                _disposed = true;
            }
        }

        #endregion

        #region IDisposable

        /// <summary>
        /// Disposes the object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}