using System;
using System.Collections.Generic;

namespace SmartCard.Core
{
    /// <summary>
    /// Represents an APDU (Application Protocol Data Unit) command.
    /// </summary>
    public class APDUCommand
    {
        #region Property(s)

        /// <summary>
        /// Gets the class byte of the APDU command.
        /// </summary>
        public byte CLA { get; }

        /// <summary>
        /// Gets the instruction byte of the APDU command.
        /// </summary>
        public byte INS { get; }

        /// <summary>
        /// Gets the parameter 1 byte of the APDU command.
        /// </summary>
        public byte P1 { get; }

        /// <summary>
        /// Gets the parameter 2 byte of the APDU command.
        /// </summary>
        public byte P2 { get; }

        /// <summary>
        /// Gets the length of the command data.
        /// </summary>
        public byte Lc { get; }

        /// <summary>
        /// Gets the data bytes of the APDU command.
        /// </summary>
        public byte[] Data { get; }

        /// <summary>
        /// Gets the expected length of the response data.
        /// </summary>
        public byte Le { get; }

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="APDUCommand"/> class.
        /// </summary>
        /// <param name="cla">The class byte.</param>
        /// <param name="ins">The instruction byte.</param>
        /// <param name="p1">The parameter 1 byte.</param>
        /// <param name="p2">The parameter 2 byte.</param>
        /// <param name="data">The data bytes (optional).</param>
        /// <param name="le">The expected length of the response data (optional).</param>
        public APDUCommand(byte cla, byte ins, byte p1, byte p2, byte[] data = null, byte le = 0)
        {
            CLA = cla;
            INS = ins;
            P1 = p1;
            P2 = p2;
            Data = data ?? Array.Empty<byte>();
            Le = le;

            if (data != null)
            {
                Lc = (byte)data.Length;
            }
        }

        #endregion

        #region Method(s)

        /// <summary>
        /// Converts the APDU command to a byte array.
        /// </summary>
        /// <returns>A byte array representing the APDU command.</returns>
        public byte[] ToBytes()
        {
            var apdu = new List<byte> { CLA, INS, P1, P2 };

            if (Lc > 0)
            {
                apdu.Add(Lc);
                apdu.AddRange(Data);
            }

            if (Le > 0)
            {
                apdu.Add(Le);
            }

            return apdu.ToArray();
        }

        #endregion
    }
}