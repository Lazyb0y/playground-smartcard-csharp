namespace SmartCard.Core.EventArgs
{
    /// <summary>
    /// Represents the event arguments for the card status changed event.
    /// </summary>
    public class CardStatusChangedEventArgs : System.EventArgs
    {
        /// <summary>
        /// Gets the name of the card reader.
        /// </summary>
        public string ReaderName { get; }

        /// <summary>
        /// Gets the status of the smart card.
        /// </summary>
        public SmartCardStatus Status { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CardStatusChangedEventArgs"/> class.
        /// </summary>
        /// <param name="readerName">The name of the card reader.</param>
        /// <param name="status">The status of the smart card.</param>
        public CardStatusChangedEventArgs(string readerName, SmartCardStatus status)
        {
            ReaderName = readerName;
            Status = status;
        }
    }
}