using System;

namespace SmartCard.Core
{
    /// <summary>
    /// Represents a response from an Application Protocol Data Unit (APDU) command.
    /// </summary>
    public class APDUResponse
    {
        #region Property(s)

        /// <summary>
        /// Gets the raw response bytes.
        /// </summary>
        public byte[] ResponseBytes { get; }

        /// <summary>
        /// Gets the data bytes.
        /// </summary>
        public byte[] Data { get; }

        /// <summary>
        /// Gets the first status word byte.
        /// </summary>
        public byte SW1 { get; }

        /// <summary>
        /// Gets the second status word byte.
        /// </summary>
        public byte SW2 { get; }

        /// <summary>
        /// Gets a value indicating whether the response is invalid.
        /// </summary>
        public bool IsInvalid { get; }

        /// <summary>
        /// Gets the maximum size of the APDU data.
        /// </summary>
        public int MaxAPDUDataSize { get; }

        /// <summary>
        /// Gets a value indicating whether the response is successful.
        /// </summary>
        public bool IsSuccess => SW1 == 0x90 && SW2 == 0x00;

        /// <summary>
        /// Gets a value indicating whether the response contains a warning.
        /// </summary>
        public bool IsWarning => SW1 == 0x62 || SW1 == 0x63;

        /// <summary>
        /// Gets a value indicating whether more data is available.
        /// </summary>
        public bool IsMoreDataAvailable =>
            SW1 == 0x61 || (SW1 == 0x90 && SW2 == 0x00 && Data.Length == MaxAPDUDataSize);

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="APDUResponse"/> class with the specified response bytes and maximum APDU data size.
        /// </summary>
        /// <param name="response">The response bytes.</param>
        /// <param name="maxAPDUDataSize">The maximum size of the APDU data.</param>
        public APDUResponse(byte[] response, int maxAPDUDataSize = 255)
        {
            MaxAPDUDataSize = maxAPDUDataSize;

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

        /// <summary>
        /// Initializes a new instance of the <see cref="APDUResponse"/> class with the specified data bytes, status word bytes, and maximum APDU data size.
        /// </summary>
        /// <param name="data">The data bytes.</param>
        /// <param name="sw1">The first status word byte.</param>
        /// <param name="sw2">The second status word byte.</param>
        /// <param name="maxAPDUDataSize">The maximum size of the APDU data.</param>
        public APDUResponse(byte[] data, byte sw1, byte sw2, int maxAPDUDataSize = 255)
            : this(CombineDataAndStatusWords(data, sw1, sw2), maxAPDUDataSize)
        {
        }

        #endregion

        #region Object

        /// <summary>
        /// Combines the data bytes and status word bytes into a single byte array.
        /// </summary>
        /// <param name="data">The data bytes.</param>
        /// <param name="sw1">The first status word byte.</param>
        /// <param name="sw2">The second status word byte.</param>
        /// <returns>A byte array containing the combined data and status word bytes.</returns>
        private static byte[] CombineDataAndStatusWords(byte[] data, byte sw1, byte sw2)
        {
            var response = new byte[data.Length + 2];

            Array.Copy(data, 0, response, 0, data.Length);

            response[response.Length - 2] = sw1;
            response[response.Length - 1] = sw2;
            return response;
        }

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