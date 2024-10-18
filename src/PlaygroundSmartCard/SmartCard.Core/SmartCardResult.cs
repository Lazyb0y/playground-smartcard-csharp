using System;
using SmartCard.Core.Internal;

namespace SmartCard.Core
{
    /// <summary>
    /// Represents the result of a smart card operation.
    /// </summary>
    public class SmartCardResult
    {
        #region Property(s)

        /// <summary>
        /// Gets or sets a value indicating whether the operation was successful.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the error code associated with the operation.
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// Gets or sets the error message associated with the operation.
        /// </summary>
        public string ErrorMessage { get; set; }

        #endregion

        #region Method(s)

        /// <summary>
        /// Creates a new <see cref="SmartCardResult"/> instance representing a successful operation.
        /// </summary>
        /// <returns>A new <see cref="SmartCardResult"/> instance representing a successful operation.</returns>
        public static SmartCardResult CreateSuccess()
        {
            return new SmartCardResult
            {
                Success = true,
                ErrorCode = null,
                ErrorMessage = null
            };
        }

        /// <summary>
        /// Creates a new <see cref="SmartCardResult"/> instance representing a failed operation with the specified error code.
        /// </summary>
        /// <param name="code">The error code associated with the operation.</param>
        /// <returns>A new <see cref="SmartCardResult"/> instance representing a failed operation.</returns>
        public static SmartCardResult CreateFailure(int code)
        {
            return CreateFailure((uint)code);
        }

        /// <summary>
        /// Creates a new <see cref="SmartCardResult"/> instance representing a failed operation with the specified error code.
        /// </summary>
        /// <param name="code">The error code associated with the operation.</param>
        /// <returns>A new <see cref="SmartCardResult"/> instance representing a failed operation.</returns>
        public static SmartCardResult CreateFailure(uint code)
        {
            string errorCode;
            string errorMessage;

            switch (code)
            {
                case WinSCardError.SCARD_E_CANCELLED:
                    errorCode = nameof(WinSCardError.SCARD_E_CANCELLED);
                    errorMessage = "The action was cancelled by an SCardCancel request.";
                    break;
                case WinSCardError.SCARD_E_INVALID_HANDLE:
                    errorCode = nameof(WinSCardError.SCARD_E_INVALID_HANDLE);
                    errorMessage = "The supplied handle was invalid.";
                    break;
                case WinSCardError.SCARD_E_INVALID_PARAMETER:
                    errorCode = nameof(WinSCardError.SCARD_E_INVALID_PARAMETER);
                    errorMessage = "One or more of the supplied parameters could not be properly interpreted.";
                    break;
                case WinSCardError.SCARD_E_INVALID_TARGET:
                    errorCode = nameof(WinSCardError.SCARD_E_INVALID_TARGET);
                    errorMessage = "Registry startup information is missing or invalid.";
                    break;
                case WinSCardError.SCARD_E_NO_MEMORY:
                    errorCode = nameof(WinSCardError.SCARD_E_NO_MEMORY);
                    errorMessage = "Not enough memory available to complete this command.";
                    break;
                case WinSCardError.SCARD_F_WAITED_TOO_LONG:
                    errorCode = nameof(WinSCardError.SCARD_F_WAITED_TOO_LONG);
                    errorMessage = "An internal consistency timer has expired.";
                    break;
                case WinSCardError.SCARD_E_INSUFFICIENT_BUFFER:
                    errorCode = nameof(WinSCardError.SCARD_E_INSUFFICIENT_BUFFER);
                    errorMessage = "The data buffer to receive returned data is too small for the returned data.";
                    break;
                case WinSCardError.SCARD_E_UNKNOWN_READER:
                    errorCode = nameof(WinSCardError.SCARD_E_UNKNOWN_READER);
                    errorMessage = "The specified reader name is not recognized.";
                    break;
                case WinSCardError.SCARD_E_TIMEOUT:
                    errorCode = nameof(WinSCardError.SCARD_E_TIMEOUT);
                    errorMessage = "The user-specified timeout value has expired.";
                    break;
                case WinSCardError.SCARD_E_SHARING_VIOLATION:
                    errorCode = nameof(WinSCardError.SCARD_E_SHARING_VIOLATION);
                    errorMessage = "The smart card cannot be accessed because of other connections outstanding.";
                    break;
                case WinSCardError.SCARD_E_NO_SMARTCARD:
                    errorCode = nameof(WinSCardError.SCARD_E_NO_SMARTCARD);
                    errorMessage = "The operation requires a Smart Card, but no Smart Card is currently in the device.";
                    break;
                case WinSCardError.SCARD_E_UNKNOWN_CARD:
                    errorCode = nameof(WinSCardError.SCARD_E_UNKNOWN_CARD);
                    errorMessage = "The specified smart card name is not recognized.";
                    break;
                case WinSCardError.SCARD_E_CANT_DISPOSE:
                    errorCode = nameof(WinSCardError.SCARD_E_CANT_DISPOSE);
                    errorMessage = "The system could not dispose of the media in the requested manner.";
                    break;
                case WinSCardError.SCARD_E_PROTO_MISMATCH:
                    errorCode = nameof(WinSCardError.SCARD_E_PROTO_MISMATCH);
                    errorMessage =
                        "The requested protocols are incompatible with the protocol currently in use with the smart card.";
                    break;
                case WinSCardError.SCARD_E_NOT_READY:
                    errorCode = nameof(WinSCardError.SCARD_E_NOT_READY);
                    errorMessage = "The reader or smart card is not ready to accept commands.";
                    break;
                case WinSCardError.SCARD_E_INVALID_VALUE:
                    errorCode = nameof(WinSCardError.SCARD_E_INVALID_VALUE);
                    errorMessage = "One or more of the supplied parameters values could not be properly interpreted.";
                    break;
                case WinSCardError.SCARD_E_SYSTEM_CANCELLED:
                    errorCode = nameof(WinSCardError.SCARD_E_SYSTEM_CANCELLED);
                    errorMessage = "The action was cancelled by the system, presumably to log off or shut down.";
                    break;
                case WinSCardError.SCARD_F_COMM_ERROR:
                    errorCode = nameof(WinSCardError.SCARD_F_COMM_ERROR);
                    errorMessage = "An internal communications error has been detected.";
                    break;
                case WinSCardError.SCARD_F_UNKNOWN_ERROR:
                    errorCode = nameof(WinSCardError.SCARD_F_UNKNOWN_ERROR);
                    errorMessage = "An internal error has been detected, but the source is unknown.";
                    break;
                case WinSCardError.SCARD_E_INVALID_ATR:
                    errorCode = nameof(WinSCardError.SCARD_E_INVALID_ATR);
                    errorMessage = "An ATR obtained from the registry is not a valid ATR string.";
                    break;
                case WinSCardError.SCARD_E_NOT_TRANSACTED:
                    errorCode = nameof(WinSCardError.SCARD_E_NOT_TRANSACTED);
                    errorMessage = "An attempt was made to end a non-existent transaction.";
                    break;
                case WinSCardError.SCARD_E_READER_UNAVAILABLE:
                    errorCode = nameof(WinSCardError.SCARD_E_READER_UNAVAILABLE);
                    errorMessage = "The specified reader is not currently available for use.";
                    break;
                case WinSCardError.SCARD_P_SHUTDOWN:
                    errorCode = nameof(WinSCardError.SCARD_P_SHUTDOWN);
                    errorMessage = "The operation has been aborted to allow the server application to exit.";
                    break;
                case WinSCardError.SCARD_E_PCI_TOO_SMALL:
                    errorCode = nameof(WinSCardError.SCARD_E_PCI_TOO_SMALL);
                    errorMessage = "The PCI Receive buffer was too small.";
                    break;
                case WinSCardError.SCARD_E_READER_UNSUPPORTED:
                    errorCode = nameof(WinSCardError.SCARD_E_READER_UNSUPPORTED);
                    errorMessage = "The reader driver does not meet minimal requirements for support.";
                    break;
                case WinSCardError.SCARD_E_DUPLICATE_READER:
                    errorCode = nameof(WinSCardError.SCARD_E_DUPLICATE_READER);
                    errorMessage = "The reader driver did not produce a unique reader name.";
                    break;
                case WinSCardError.SCARD_E_CARD_UNSUPPORTED:
                    errorCode = nameof(WinSCardError.SCARD_E_CARD_UNSUPPORTED);
                    errorMessage = "The smart card does not meet minimal requirements for support.";
                    break;
                case WinSCardError.SCARD_E_NO_SERVICE:
                    errorCode = nameof(WinSCardError.SCARD_E_NO_SERVICE);
                    errorMessage = "The Smart card resource manager is not running.";
                    break;
                case WinSCardError.SCARD_E_SERVICE_STOPPED:
                    errorCode = nameof(WinSCardError.SCARD_E_SERVICE_STOPPED);
                    errorMessage = "The Smart card resource manager has shut down.";
                    break;
                case WinSCardError.SCARD_E_UNEXPECTED:
                    errorCode = nameof(WinSCardError.SCARD_E_UNEXPECTED);
                    errorMessage = "An unexpected card error has occurred.";
                    break;
                case WinSCardError.SCARD_E_ICC_INSTALLATION:
                    errorCode = nameof(WinSCardError.SCARD_E_ICC_INSTALLATION);
                    errorMessage = "No Primary Provider can be found for the smart card.";
                    break;
                case WinSCardError.SCARD_E_ICC_CREATEORDER:
                    errorCode = nameof(WinSCardError.SCARD_E_ICC_CREATEORDER);
                    errorMessage = "The requested order of object creation is not supported.";
                    break;
                case WinSCardError.SCARD_E_UNSUPPORTED_FEATURE:
                    errorCode = nameof(WinSCardError.SCARD_E_UNSUPPORTED_FEATURE);
                    errorMessage = "This smart card does not support the requested feature.";
                    break;
                case WinSCardError.SCARD_E_DIR_NOT_FOUND:
                    errorCode = nameof(WinSCardError.SCARD_E_DIR_NOT_FOUND);
                    errorMessage = "The identified directory does not exist in the smart card.";
                    break;
                case WinSCardError.SCARD_E_FILE_NOT_FOUND:
                    errorCode = nameof(WinSCardError.SCARD_E_FILE_NOT_FOUND);
                    errorMessage = "The identified file does not exist in the smart card.";
                    break;
                case WinSCardError.SCARD_E_NO_DIR:
                    errorCode = nameof(WinSCardError.SCARD_E_NO_DIR);
                    errorMessage = "The supplied path does not represent a smart card directory.";
                    break;
                case WinSCardError.SCARD_E_NO_FILE:
                    errorCode = nameof(WinSCardError.SCARD_E_NO_FILE);
                    errorMessage = "The supplied path does not represent a smart card file.";
                    break;
                case WinSCardError.SCARD_E_NO_ACCESS:
                    errorCode = nameof(WinSCardError.SCARD_E_NO_ACCESS);
                    errorMessage = "Access is denied to this file.";
                    break;
                case WinSCardError.SCARD_E_WRITE_TOO_MANY:
                    errorCode = nameof(WinSCardError.SCARD_E_WRITE_TOO_MANY);
                    errorMessage = "The smart card does not have enough memory to store the information.";
                    break;
                case WinSCardError.SCARD_E_BAD_SEEK:
                    errorCode = nameof(WinSCardError.SCARD_E_BAD_SEEK);
                    errorMessage = "There was an error trying to set the smart card file object pointer.";
                    break;
                case WinSCardError.SCARD_E_INVALID_CHV:
                    errorCode = nameof(WinSCardError.SCARD_E_INVALID_CHV);
                    errorMessage = "The supplied PIN is incorrect.";
                    break;
                case WinSCardError.SCARD_E_UNKNOWN_RES_MNG:
                    errorCode = nameof(WinSCardError.SCARD_E_UNKNOWN_RES_MNG);
                    errorMessage = "An unrecognized error code was returned from a layered component.";
                    break;
                case WinSCardError.SCARD_E_NO_SUCH_CERTIFICATE:
                    errorCode = nameof(WinSCardError.SCARD_E_NO_SUCH_CERTIFICATE);
                    errorMessage = "The requested certificate does not exist.";
                    break;
                case WinSCardError.SCARD_E_CERTIFICATE_UNAVAILABLE:
                    errorCode = nameof(WinSCardError.SCARD_E_CERTIFICATE_UNAVAILABLE);
                    errorMessage = "The requested certificate could not be obtained.";
                    break;
                case WinSCardError.SCARD_E_NO_READERS_AVAILABLE:
                    errorCode = nameof(WinSCardError.SCARD_E_NO_READERS_AVAILABLE);
                    errorMessage = "Cannot find a smart card reader.";
                    break;
                case WinSCardError.SCARD_E_COMM_DATA_LOST:
                    errorCode = nameof(WinSCardError.SCARD_E_COMM_DATA_LOST);
                    errorMessage = "A communications error with the smart card has been detected.";
                    break;
                case WinSCardError.SCARD_E_NO_KEY_CONTAINER:
                    errorCode = nameof(WinSCardError.SCARD_E_NO_KEY_CONTAINER);
                    errorMessage = "The requested key container does not exist on the smart card.";
                    break;
                case WinSCardError.SCARD_E_SERVER_TOO_BUSY:
                    errorCode = nameof(WinSCardError.SCARD_E_SERVER_TOO_BUSY);
                    errorMessage = "The Smart card resource manager is too busy to complete this operation.";
                    break;
                case WinSCardError.SCARD_E_PIN_CACHE_EXPIRED:
                    errorCode = nameof(WinSCardError.SCARD_E_PIN_CACHE_EXPIRED);
                    errorMessage = "The smart card PIN cache has expired.";
                    break;
                case WinSCardError.SCARD_E_NO_PIN_CACHE:
                    errorCode = nameof(WinSCardError.SCARD_E_NO_PIN_CACHE);
                    errorMessage = "The smart card PIN cannot be cached.";
                    break;
                case WinSCardError.SCARD_E_READ_ONLY_CARD:
                    errorCode = nameof(WinSCardError.SCARD_E_READ_ONLY_CARD);
                    errorMessage = "The smart card is read-only and cannot be written to.";
                    break;
                case WinSCardError.SCARD_W_UNSUPPORTED_CARD:
                    errorCode = nameof(WinSCardError.SCARD_W_UNSUPPORTED_CARD);
                    errorMessage =
                        "The reader cannot communicate with the card, due to ATR string configuration conflicts.";
                    break;
                case WinSCardError.SCARD_W_UNRESPONSIVE_CARD:
                    errorCode = nameof(WinSCardError.SCARD_W_UNRESPONSIVE_CARD);
                    errorMessage = "The smart card is not responding to a reset.";
                    break;
                case WinSCardError.SCARD_W_UNPOWERED_CARD:
                    errorCode = nameof(WinSCardError.SCARD_W_UNPOWERED_CARD);
                    errorMessage =
                        "Power has been removed from the smart card, so that further communication is not possible.";
                    break;
                case WinSCardError.SCARD_W_RESET_CARD:
                    errorCode = nameof(WinSCardError.SCARD_W_RESET_CARD);
                    errorMessage = "The smart card has been reset, so any shared state information is invalid.";
                    break;
                case WinSCardError.SCARD_W_REMOVED_CARD:
                    errorCode = nameof(WinSCardError.SCARD_W_REMOVED_CARD);
                    errorMessage = "The smart card has been removed from the reader.";
                    break;
                case WinSCardError.SCARD_W_SECURITY_VIOLATION:
                    errorCode = nameof(WinSCardError.SCARD_W_SECURITY_VIOLATION);
                    errorMessage = "Access was denied because of a security violation.";
                    break;
                case WinSCardError.SCARD_W_WRONG_CHV:
                    errorCode = nameof(WinSCardError.SCARD_W_WRONG_CHV);
                    errorMessage = "The card cannot be accessed because the wrong PIN was presented.";
                    break;
                case WinSCardError.SCARD_W_CHV_BLOCKED:
                    errorCode = nameof(WinSCardError.SCARD_W_CHV_BLOCKED);
                    errorMessage =
                        "The card cannot be accessed because the maximum number of PIN entry attempts has been reached.";
                    break;
                case WinSCardError.SCARD_W_EOF:
                    errorCode = nameof(WinSCardError.SCARD_W_EOF);
                    errorMessage = "The end of the smart card file has been reached.";
                    break;
                case WinSCardError.SCARD_W_CANCELLED_BY_USER:
                    errorCode = nameof(WinSCardError.SCARD_W_CANCELLED_BY_USER);
                    errorMessage = "The action was cancelled by the user.";
                    break;
                case WinSCardError.SCARD_W_CARD_NOT_AUTHENTICATED:
                    errorCode = nameof(WinSCardError.SCARD_W_CARD_NOT_AUTHENTICATED);
                    errorMessage = "The card is not authenticated and requires authentication.";
                    break;
                case WinSCardError.SCARD_W_CACHE_ITEM_NOT_FOUND:
                    errorCode = nameof(WinSCardError.SCARD_W_CACHE_ITEM_NOT_FOUND);
                    errorMessage = "The requested cache item was not found.";
                    break;
                case WinSCardError.SCARD_W_CACHE_ITEM_STALE:
                    errorCode = nameof(WinSCardError.SCARD_W_CACHE_ITEM_STALE);
                    errorMessage = "The cache item has become stale and should be refreshed.";
                    break;
                case WinSCardError.SCARD_W_CACHE_ITEM_TOO_BIG:
                    errorCode = nameof(WinSCardError.SCARD_W_CACHE_ITEM_TOO_BIG);
                    errorMessage = "The cache item is too big to be stored.";
                    break;
                default:
                    errorCode = "UNKNOWN";
                    errorMessage = "An unknown error occurred.";
                    break;
            }

            return new SmartCardResult
            {
                Success = false,
                ErrorCode = errorCode,
                ErrorMessage = errorMessage
            };
        }

        /// <summary>
        /// Creates a new <see cref="SmartCardResult"/> instance representing a failed operation with the specified error code and error message.
        /// </summary>
        /// <param name="errorCode">The error code associated with the operation.</param>
        /// <param name="errorMessage">The error message associated with the operation.</param>
        /// <returns>A new <see cref="SmartCardResult"/> instance representing a failed operation.</returns>
        public static SmartCardResult CreateFailure(string errorCode, string errorMessage)
        {
            return new SmartCardResult
            {
                Success = false,
                ErrorCode = errorCode,
                ErrorMessage = errorMessage
            };
        }

        #endregion
    }

    /// <summary>
    /// Represents the result of a smart card operation.
    /// </summary>
    /// <typeparam name="T">The type of data associated with the result.</typeparam>
    public class SmartCardResult<T> : SmartCardResult
    {
        #region Property(s)

        /// <summary>
        /// Gets or sets the data associated with the result.
        /// </summary>
        public T Data { get; set; }

        #endregion

        #region Method(s)

        /// <summary>
        /// Converts the current <see cref="SmartCardResult{T}"/> to a new <see cref="SmartCardResult{TResult}"/> using the specified converter function.
        /// </summary>
        /// <typeparam name="TResult">The type of data associated with the new result.</typeparam>
        /// <param name="converter">The converter function to convert the data.</param>
        /// <returns>A new <see cref="SmartCardResult{TResult}"/> with the converted data.</returns>
        public SmartCardResult<TResult> Convert<TResult>(Func<T, TResult> converter = null)
        {
            var convertedResult = new SmartCardResult<TResult>
            {
                Success = Success,
                ErrorCode = ErrorCode,
                ErrorMessage = ErrorMessage
            };

            if (converter != null)
            {
                convertedResult.Data = converter(Data);
            }

            return convertedResult;
        }

        /// <summary>
        /// Creates a new <see cref="SmartCardResult{T}"/> instance representing a successful operation with the specified data.
        /// </summary>
        /// <typeparam name="T">The type of data associated with the result.</typeparam>
        /// <param name="data">The data associated with the result.</param>
        /// <returns>A new <see cref="SmartCardResult{T}"/> instance representing a successful operation.</returns>
        public static SmartCardResult<T> CreateSuccess(T data)
        {
            return new SmartCardResult<T>
            {
                Success = true,
                ErrorCode = null,
                ErrorMessage = null,
                Data = data
            };
        }

        /// <summary>
        /// Creates a new <see cref="SmartCardResult{T}"/> instance representing a failed operation with the specified error code.
        /// </summary>
        /// <typeparam name="T">The type of data associated with the result.</typeparam>
        /// <param name="code">The error code associated with the operation.</param>
        /// <returns>A new <see cref="SmartCardResult{T}"/> instance representing a failed operation.</returns>
        public new static SmartCardResult<T> CreateFailure(int code)
        {
            var errorResult = CreateFailure((uint)code);
            return new SmartCardResult<T>
            {
                Success = errorResult.Success,
                ErrorCode = errorResult.ErrorCode,
                ErrorMessage = errorResult.ErrorMessage,
                Data = default
            };
        }

        /// <summary>
        /// Creates a new <see cref="SmartCardResult{T}"/> instance representing a failed operation with the specified error code.
        /// </summary>
        /// <typeparam name="T">The type of data associated with the result.</typeparam>
        /// <param name="code">The error code associated with the operation.</param>
        /// <returns>A new <see cref="SmartCardResult{T}"/> instance representing a failed operation.</returns>
        public new static SmartCardResult<T> CreateFailure(uint code)
        {
            var errorResult = SmartCardResult.CreateFailure(code);
            return new SmartCardResult<T>
            {
                Success = false,
                ErrorCode = errorResult.ErrorCode,
                ErrorMessage = errorResult.ErrorMessage,
                Data = default
            };
        }

        #endregion
    }
}