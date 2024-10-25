using System;
using System.Collections.Generic;

namespace SmartCard.Core
{
    /// <summary>
    /// Represents an Application Protocol Data Unit (APDU) command.
    /// </summary>
    public class APDUCommand
    {
        #region Property(s)

        /// <summary>
        /// Gets the Class byte of the APDU command.
        /// </summary>
        public byte CLA { get; }

        /// <summary>
        /// Gets the Instruction byte of the APDU command.
        /// </summary>
        public byte INS { get; }

        /// <summary>
        /// Gets the Parameter 1 byte of the APDU command.
        /// </summary>
        public byte P1 { get; }

        /// <summary>
        /// Gets the Parameter 2 byte of the APDU command.
        /// </summary>
        public byte P2 { get; }

        /// <summary>
        /// Gets the Length of the command data.
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

        /// <summary>
        /// Gets the maximum size of the APDU data.
        /// </summary>
        public int MaxAPDUDataSize { get; }

        /// <summary>
        /// Gets the chaining bit used for APDU command chaining.
        /// </summary>
        public byte ChainingBit { get; }

        /// <summary>
        /// Gets a value indicating whether the APDU command needs chaining.
        /// </summary>
        public bool NeedsChaining => Data.Length > MaxAPDUDataSize;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="APDUCommand"/> class.
        /// </summary>
        /// <param name="cla">The Class byte.</param>
        /// <param name="ins">The Instruction byte.</param>
        /// <param name="p1">The Parameter 1 byte.</param>
        /// <param name="p2">The Parameter 2 byte.</param>
        /// <param name="data">The data bytes.</param>
        /// <param name="le">The expected length of the response data.</param>
        /// <param name="maxAPDUDataSize">The maximum size of the APDU data.</param>
        /// <param name="chainingBit">The chaining bit used for APDU command chaining.</param>
        public APDUCommand(byte cla, byte ins, byte p1, byte p2, byte[] data = null, byte le = 0, int maxAPDUDataSize = 255, byte chainingBit = 0x10)
        {
            MaxAPDUDataSize = maxAPDUDataSize;
            ChainingBit = chainingBit;

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
        /// Gets the chained APDU commands if the data exceeds the maximum APDU data size.
        /// </summary>
        /// <returns>An enumerable of chained APDU commands.</returns>
        public IEnumerable<APDUCommand> GetChainedCommands()
        {
            var offset = 0;
            while (offset < Data.Length)
            {
                var length = Math.Min(MaxAPDUDataSize, Data.Length - offset);
                var chunk = new byte[length];
                Array.Copy(Data, offset, chunk, 0, length);
                offset += length;

                var cla = offset < Data.Length ? (byte)(CLA | ChainingBit) : CLA;

                // Set Le only for the last command in the chain
                var le = offset >= Data.Length ? Le : (byte)0;

                yield return new APDUCommand(cla, INS, P1, P2, chunk, le, MaxAPDUDataSize, ChainingBit);
            }
        }

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