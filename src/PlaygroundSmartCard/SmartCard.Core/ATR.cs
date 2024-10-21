using System;

namespace SmartCard.Core
{
    /// <summary>
    /// Represents an Answer To Reset (ATR) of a smart card.
    /// </summary>
    public class ATR
    {
        #region Property(s)

        /// <summary>
        /// Gets the string representation of ATR.
        /// </summary>
        public string String
        {
            get
            {
                if (Bytes == null)
                {
                    return null;
                }

                return BitConverter.ToString(Bytes);
            }
        }

        /// <summary>
        /// Gets the ATR bytes.
        /// </summary>
        public byte[] Bytes { get; }

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="ATR"/> class with the specified ATR bytes.
        /// </summary>
        /// <param name="atrBytes">The ATR bytes.</param>
        public ATR(byte[] atrBytes)
        {
            Bytes = atrBytes;
        }

        #endregion

        #region Method(s)

        /// <summary>
        /// Normalizes the specified ATR string by removing spaces, dashes, and converting to uppercase.
        /// </summary>
        /// <param name="atr">The ATR string to normalize.</param>
        /// <returns>The normalized ATR string.</returns>
        public static string Normalize(string atr)
        {
            return atr
                .Replace(" ", string.Empty)
                .Replace("-", string.Empty)
                .ToUpper();
        }

        #endregion
    }
}