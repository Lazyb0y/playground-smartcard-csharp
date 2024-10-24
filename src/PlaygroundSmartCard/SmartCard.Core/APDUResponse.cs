using System;

namespace SmartCard.Core
{
    /// <summary>
    /// Represents a response from an APDU command.
    /// </summary>
    public class APDUResponse
    {
        #region Property(s)

        /// <summary>
        /// Gets the raw response bytes.
        /// </summary>
        public byte[] ResponseBytes { get; }

        /// <summary>
        /// Gets the data bytes from the response.
        /// </summary>
        public byte[] Data { get; }

        /// <summary>
        /// Gets the status word 1 byte.
        /// </summary>
        public byte SW1 { get; }

        /// <summary>
        /// Gets the status word 2 byte.
        /// </summary>
        public byte SW2 { get; }

        /// <summary>
        /// Gets a value indicating whether the response is invalid.
        /// </summary>
        public bool IsInvalid { get; }

        /// <summary>
        /// Gets a value indicating whether the response indicates success.
        /// </summary>
        public bool IsSuccess => SW1 == 0x90 && SW2 == 0x00;

        /// <summary>
        /// Gets a value indicating whether the response indicates a warning.
        /// </summary>
        public bool IsWarning => SW1 == 0x62 || SW1 == 0x63;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="APDUResponse"/> class.
        /// </summary>
        /// <param name="response">The raw response bytes.</param>
        public APDUResponse(byte[] response)
        {
            ResponseBytes = response;

            if (response == null || response.Length < 2)
            {
                IsInvalid = true;
                return;
            }

            /* The last two bytes are SW1 and SW2 */
            SW1 = response[response.Length - 2];
            SW2 = response[response.Length - 1];

            if (response.Length > 2)
            {
                Data = new byte[response.Length - 2];
                Array.Copy(response, 0, Data, 0, response.Length - 2);
            }
            else
            {
                Data = Array.Empty<byte>();
            }
        }

        #endregion

        #region Object

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"Data: {BitConverter.ToString(Data)}, SW1: {SW1:X2}, SW2: {SW2:X2}";
        }

        #endregion
    }
}