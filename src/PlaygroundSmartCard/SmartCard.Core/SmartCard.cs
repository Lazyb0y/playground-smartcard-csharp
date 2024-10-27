using System;
using System.Collections.Generic;
using System.Linq;
using SmartCard.Core.WinSCard;

namespace SmartCard.Core
{
    /// <summary>
    /// Represents an abstract base class for smart card operations.
    /// </summary>
    /// <remarks>
    /// This class provides methods for connecting, disconnecting, and communicating with a smart card.
    /// It also implements the <see cref="IDisposable"/> interface to ensure proper resource management.
    /// </remarks>
    public abstract class SmartCard : IDisposable
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

        /// <summary>
        /// Gets the handle of the connected smart card.
        /// </summary>
        /// <remarks>
        /// This property returns the handle of the smart card that was obtained during the connection.
        /// </remarks>
        public IntPtr CardHandle => _cardHandle;

        /// <summary>
        /// Gets the protocol used for communication with the smart card.
        /// </summary>
        /// <remarks>
        /// This property returns the protocol that was negotiated during the connection to the smart card.
        /// </remarks>
        public int Protocol => _protocol;

        #endregion

        #region Abstruction

        /// <summary>
        /// Gets the maximum size of the APDU data.
        /// </summary>
        public abstract int MaxAPDUDataSize { get; }

        /// <summary>
        /// Gets the chaining bit for APDU commands.
        /// </summary>
        public abstract byte ChainingBit { get; }

        /// <summary>
        /// Transmits the specified command bytes to the smart card.
        /// </summary>
        /// <param name="commandBytes">The command bytes to transmit.</param>
        /// <returns>The response bytes from the smart card.</returns>
        protected abstract byte[] Transmit(byte[] commandBytes);

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="SmartCard"/> class with the specified <see cref="SmartCardScope"/>.
        /// </summary>
        /// <param name="scope">The scope of the smart card reader.</param>
        protected SmartCard(SmartCardScope scope)
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
                WinSCardProtocol.SCARD_PROTOCOL_T0 | WinSCardProtocol.SCARD_PROTOCOL_T1,
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
        /// Gets the Answer to Reset (ATR) of the smart card.
        /// </summary>
        /// <returns>A <see cref="SmartCardResult{T}"/> representing the result of getting the ATR.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the smart card is not connected or the reader name is not set.</exception>
        public SmartCardResult<byte[]> GetATR()
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
                return SmartCardResult<byte[]>.CreateFailure(result);
            }

            var atrBytes = atr.Take(atrLen).ToArray();
            return SmartCardResult<byte[]>.CreateSuccess(atrBytes);
        }

        /// <summary>
        /// Gets the attribute of the smart card with the specified attribute ID.
        /// </summary>
        /// <param name="attributeId">The ID of the attribute to retrieve.</param>
        /// <returns>A <see cref="SmartCardResult{T}"/> representing the result of getting the attribute.</returns>
        public SmartCardResult<byte[]> GetAttribute(uint attributeId)
        {
            var attribute = new byte[512];
            var attributeSize = (uint)attribute.Length;

            var result = WinSCardAPI.SCardGetAttrib(_cardHandle, attributeId, attribute, ref attributeSize);
            if (result != WinSCardError.SCARD_S_SUCCESS)
            {
                return SmartCardResult<byte[]>.CreateFailure(result);
            }

            Array.Resize(ref attribute, (int)attributeSize);
            return SmartCardResult<byte[]>.CreateSuccess(attribute);
        }

        /// <summary>
        /// Checks if a smart card is present in the reader.
        /// </summary>
        /// <returns>
        /// A <see cref="SmartCardResult{T}"/> containing a boolean value indicating whether a smart card is present.
        /// </returns>
        /// <exception cref="InvalidOperationException">Thrown when the smart card is not connected or the reader name is not set.</exception>
        public SmartCardResult<bool> IsCardPresent()
        {
            ValidateReaderName();
            ValidateCardHandler();

            WinSCardReaderState[] readerStates =
            {
                new WinSCardReaderState
                {
                    ReaderName = ReaderName,
                    CurrentState = WinSCardState.SCARD_STATE_UNAWARE
                }
            };

            var result = WinSCardAPI.SCardGetStatusChange(_context.Context, 0, readerStates, (uint)readerStates.Length);
            if (result != WinSCardError.SCARD_S_SUCCESS)
            {
                return SmartCardResult<bool>.CreateFailure(result);
            }

            var cardPresent = (readerStates[0].EventState & WinSCardState.SCARD_STATE_PRESENT) == WinSCardState.SCARD_STATE_PRESENT;
            return SmartCardResult<bool>.CreateSuccess(cardPresent);
        }

        /// <summary>
        /// Sends an APDU command to the smart card.
        /// </summary>
        /// <param name="apduCommand">The APDU command to send.</param>
        /// <returns>An <see cref="APDUResponse"/> representing the response from the smart card.</returns>
        public APDUResponse Send(APDUCommand apduCommand)
        {
            if (apduCommand.NeedsChaining)
            {
                return SendChained(apduCommand);
            }

            var responseBytes = Transmit(apduCommand.ToBytes());
            return HandleResponseChaining(new APDUResponse(responseBytes, MaxAPDUDataSize));
        }

        /// <summary>
        /// Sends a chained APDU command to the smart card.
        /// </summary>
        /// <param name="apduCommand">The APDU command to send.</param>
        /// <returns>The response from the smart card.</returns>
        private APDUResponse SendChained(APDUCommand apduCommand)
        {
            APDUResponse lastResponse = null;

            foreach (var command in apduCommand.GetChainedCommands())
            {
                var responseBytes = Transmit(command.ToBytes());
                var response = new APDUResponse(responseBytes, MaxAPDUDataSize);

                // Note: This code will only consider the very last response for return. This is intentional.
                lastResponse = HandleResponseChaining(response);
            }

            return lastResponse;
        }

        /// <summary>
        /// Handles the response chaining for APDU commands.
        /// </summary>
        /// <param name="initialResponse">The initial response from the smart card.</param>
        /// <returns>The complete response from the smart card.</returns>
        private APDUResponse HandleResponseChaining(APDUResponse initialResponse)
        {
            if (!initialResponse.IsMoreDataAvailable)
            {
                return initialResponse;
            }

            var completeData = new List<byte>(initialResponse.Data);

            while (initialResponse.IsMoreDataAvailable)
            {
                var getResponseCommand = new APDUCommand(
                    0x00,
                    0xC0,
                    0x00,
                    0x00,
                    null,
                    (byte)MaxAPDUDataSize,
                    MaxAPDUDataSize,
                    ChainingBit);

                var responseBytes = Transmit(getResponseCommand.ToBytes());
                initialResponse = new APDUResponse(responseBytes, initialResponse.MaxAPDUDataSize);
                completeData.AddRange(initialResponse.Data);
            }

            return new APDUResponse(
                completeData.ToArray(),
                initialResponse.SW1,
                initialResponse.SW2,
                initialResponse.MaxAPDUDataSize);
        }

        /// <summary>
        /// Verifies the PIN with the smart card.
        /// </summary>
        /// <param name="pin">The PIN to verify.</param>
        /// <returns>An <see cref="APDUResponse"/> representing the response from the smart card.</returns>
        /// <remarks>
        /// This method sends a VERIFY command to the smart card to check the provided PIN.
        /// </remarks>
        public virtual APDUResponse VerifyPIN(byte[] pin)
        {
            var verifyCommand = new APDUCommand(
                CLA.STANDARD,
                INS.VERIFY_PIN,
                0x00, // P1 is usually 0x00 for VERIFY
                0x80, // Reference control for the PIN (commonly 0x80)
                pin,
                0x00,
                MaxAPDUDataSize,
                ChainingBit
            );

            return Send(verifyCommand);
        }

        /// <summary>
        /// Gets the remaining PIN attempts.
        /// </summary>
        /// <returns>
        /// The number of remaining PIN attempts, or -1 if the remaining attempts couldn't be retrieved.
        /// </returns>
        /// <remarks>
        /// This method sends a GET DATA command to the smart card to retrieve the number of remaining PIN attempts.
        /// </remarks>
        public virtual int GetRemainingPINAttempts()
        {
            var command = new APDUCommand(
                CLA.STANDARD,
                INS.GET_DATA,
                0x00,
                0xC0, // P2 for retrieving remaining PIN attempts
                null,
                0x00,
                MaxAPDUDataSize,
                ChainingBit
            );

            var response = Send(command);
            if (response.SW1 == 0x63 && (response.SW2 & 0x0F) > 0)
            {
                return response.SW2 & 0x0F;
            }

            return -1; // Indicate that remaining attempts couldn't be retrieved
        }

        /// <summary>
        /// Evaluates the authentication response from the smart card.
        /// </summary>
        /// <param name="response">The APDU response from the smart card.</param>
        /// <returns>
        /// An <see cref="AuthenticationStatus"/> value indicating the result of the authentication.
        /// </returns>
        /// <remarks>
        /// This method interprets the status words (SW1 and SW2) in the APDU response to determine
        /// the authentication status. It returns <see cref="AuthenticationStatus.Success"/> if the
        /// authentication was successful, <see cref="AuthenticationStatus.IncorrectPin"/> if the PIN
        /// was incorrect, <see cref="AuthenticationStatus.PinBlocked"/> if the PIN is blocked, and
        /// <see cref="AuthenticationStatus.Failure"/> for any other failure.
        /// </remarks>
        public virtual AuthenticationStatus EvaluatePINAuthenticationResponse(APDUResponse response)
        {
            if (response.IsSuccess)
            {
                return AuthenticationStatus.Success;
            }

            if (response.SW1 == 0x63 && (response.SW2 & 0x0F) > 0)
            {
                return AuthenticationStatus.IncorrectPin;
            }

            if (response.SW1 == 0x69 && response.SW2 == 0x83)
            {
                return AuthenticationStatus.PinBlocked;
            }

            return AuthenticationStatus.Failure;
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

        /// <summary>
        /// Validates if the card handler is set.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when the smart card is not connected.</exception>
        private void ValidateCardHandler()
        {
            if (_cardHandle == IntPtr.Zero)
            {
                throw new InvalidOperationException("The smart card is not connected.");
            }
        }

        /// <summary>
        /// Validates if the reader name is set.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when the reader name is not set.</exception>
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