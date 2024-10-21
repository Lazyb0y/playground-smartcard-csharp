namespace SmartCard.Core
{
    /// <summary>
    /// Represents an ATR (Answer To Reset) database entry.
    /// </summary>
    public class ATRDatabase
    {
        /// <summary>
        /// Gets or sets the normalized ATR value.
        /// </summary>
        public string NormalizeATR { get; private set; }

        /// <summary>
        /// Gets or sets the type of smart card.
        /// </summary>
        public SmartCardType CardType { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ATRDatabase"/> class.
        /// </summary>
        /// <param name="atr">The ATR value.</param>
        /// <param name="cardType">The type of smart card.</param>
        public ATRDatabase(string atr, SmartCardType cardType)
        {
            NormalizeATR = ATR.Normalize(atr);
            CardType = cardType;
        }
    }
}