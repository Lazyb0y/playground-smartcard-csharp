using System;
using SmartCard.Core.WinSCard;

namespace SmartCard.Core
{
    /// <summary>
    /// Represents a smart card.
    /// </summary>
    public class SmartCard
    {
        #region Declaration(s)

        private bool _disposed;
        private readonly SmartCardContext _context;
        private IntPtr _cardHandle;
        private int _protocol;

        #endregion

        #region Property(s)

        /// <summary>
        /// Gets or sets the name of the smart card reader.
        /// </summary>
        /// <remarks>
        /// This property is mandatory to set before performing any operation.
        /// </remarks>
        public string ReaderName { get; set; }

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="SmartCard"/> class with the specified <see cref="SmartCardScope"/>.
        /// </summary>
        /// <param name="scope">The scope of the smart card reader.</param>
        public SmartCard(SmartCardScope scope)
        {
            _context = new SmartCardContext(scope);

            var result = _context.Establish();
            if (!result.Success)
            {
                throw new SmartCardException(result.ErrorMessage, result.ErrorCode);
            }
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="SmartCard"/> class.
        /// </summary>
        ~SmartCard()
        {
            Dispose(false);
        }

        #endregion

        #region Method(s)

        /// <summary>
        /// Connects to the smart card.
        /// </summary>
        /// <returns>A <see cref="SmartCardResult"/> representing the result of the connection.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the smart card is not connected.</exception>
        public SmartCardResult Connect()
        {
            ValidateReaderName();

            var cardHandle = IntPtr.Zero;
            var protocol = 0;

            var result = WinSCardAPI.SCardConnect(
                _context.Context,
                ReaderName,
                WinSCardShare.SCARD_SHARE_SHARED,
                WinSCardProtocol.SCARD_PROTOCOL_T0 | WinSCardProtocol.SCARD_PROTOCOL_T0,
                ref cardHandle,
                ref protocol
            );

            if (result != WinSCardError.SCARD_S_SUCCESS)
            {
                return SmartCardResult.CreateFailure(result);
            }

            _cardHandle = cardHandle;
            _protocol = protocol;

            return SmartCardResult.CreateSuccess();
        }

        /// <summary>
        /// Disconnects from the smart card.
        /// </summary>
        /// <returns>A <see cref="SmartCardResult"/> representing the result of the disconnection.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the smart card is not connected.</exception>
        public SmartCardResult Disconnect()
        {
            ValidateCardHandler();

            var result = WinSCardAPI.SCardDisconnect(_cardHandle, WinSCardDisposition.SCARD_RESET_CARD);
            if (result != WinSCardError.SCARD_S_SUCCESS)
            {
                return SmartCardResult.CreateFailure(result);
            }

            _cardHandle = IntPtr.Zero;
            _protocol = default;

            return SmartCardResult.CreateSuccess();
        }

        /// <summary>
        /// Gets the Answer to Reset (ATR) string of the smart card.
        /// </summary>
        /// <returns>A <see cref="SmartCardResult{T}"/> representing the result of getting the ATR string.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the smart card is not connected or the reader name is not set.</exception>
        public SmartCardResult<string> GetATRString()
        {
            ValidateReaderName();
            ValidateCardHandler();

            var atr = new byte[64];
            var atrLen = atr.Length;
            int state = 0, activeProtocol = 0;
            var readerNameBuffer = new byte[512];
            var readerLen = readerNameBuffer.Length;

            var result = WinSCardAPI.SCardStatus(
                _cardHandle,
                readerNameBuffer,
                ref readerLen,
                ref state,
                ref activeProtocol,
                atr,
                ref atrLen
            );

            if (result != WinSCardError.SCARD_S_SUCCESS)
            {
                return SmartCardResult<string>.CreateFailure(result);
            }

            var atrString = BitConverter.ToString(atr, 0, atrLen);
            return SmartCardResult<string>.CreateSuccess(atrString);
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
                    if (_cardHandle != IntPtr.Zero)
                    {
                        var result = Disconnect();
                        if (!result.Success)
                        {
                            Console.WriteLine(
                                $"An error occurred during disconnecting the card: [{result.ErrorCode}] {result.ErrorMessage}");
                        }
                    }
                }
                catch (Exception x)
                {
                    Console.WriteLine($"An error occurred during disconnecting the card: {x}");
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

        private void ValidateCardHandler()
        {
            if (_cardHandle == IntPtr.Zero)
            {
                throw new InvalidOperationException("The smart card is not connected.");
            }
        }

        private void ValidateReaderName()
        {
            if (string.IsNullOrEmpty(ReaderName))
            {
                throw new InvalidOperationException("The reader name is not set.");
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