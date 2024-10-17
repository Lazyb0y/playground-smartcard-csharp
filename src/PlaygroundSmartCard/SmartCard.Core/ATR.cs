namespace SmartCard.Core
{
    /// <summary>
    /// Represents an Answer To Reset (ATR) of a smart card.
    /// </summary>
    public class ATR
    {
        #region Declaration(s)

        private readonly string _sanitizedATRString;

        #endregion

        #region Property(s)

        /// <summary>
        /// Gets the original ATR string.
        /// </summary>
        public string ATRString { get; }

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="ATR"/> class with the specified ATR string.
        /// </summary>
        /// <param name="atrString">The ATR string.</param>
        public ATR(string atrString)
        {
            ATRString = atrString;

            _sanitizedATRString = atrString
                .Replace(" ", string.Empty)
                .Replace("-", string.Empty)
                .ToUpper();
        }

        #endregion

        /// <summary>
        /// Gets the type of the smart card based on the ATR string.
        /// </summary>
        /// <returns>The type of the smart card.</returns>
        public SmartCardType GetCardType()
        {
            if (_sanitizedATRString.StartsWith("3B65"))
            {
                return SmartCardType.EMV;
            }

            if (_sanitizedATRString.StartsWith("3B8F80"))
            {
                return SmartCardType.Mifare;
            }

            if (_sanitizedATRString.StartsWith("3B3F11008012009131C0640E0146AC72F74105"))
            {
                return SmartCardType.Scosta;
            }

            if (_sanitizedATRString.StartsWith("3B9F"))
            {
                return SmartCardType.SIM;
            }

            return SmartCardType.Unknown;
        }

        /// <summary>
        /// Returns a string that represents the current ATR object.
        /// </summary>
        /// <returns>A string that represents the current ATR object.</returns>
        public override string ToString()
        {
            return ATRString;
        }
    }
}