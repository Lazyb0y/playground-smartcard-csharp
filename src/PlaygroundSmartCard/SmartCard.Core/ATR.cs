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

        /// <summary>
        /// Gets the type of the smart card based on the ATR string.
        /// </summary>
        /// <returns>The type of the smart card.</returns>
        public SmartCardType GetCardType()
        {
            var sanitizedATR = String
                .Replace(" ", string.Empty)
                .Replace("-", string.Empty)
                .ToUpper();

            if (sanitizedATR.StartsWith("3B65"))
            {
                return SmartCardType.EMV;
            }

            if (sanitizedATR.StartsWith("3B8F80"))
            {
                return SmartCardType.Mifare;
            }

            if (sanitizedATR.StartsWith("3B3F11008012009131C0640E0146AC72F74105"))
            {
                return SmartCardType.Scosta;
            }

            if (sanitizedATR.StartsWith("3B9F"))
            {
                return SmartCardType.SIM;
            }

            return SmartCardType.Unknown;
        }
    }
}