using System;

namespace SmartCard.Core.Cards
{
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// Represents an ISO 7816 compliant smart card.
    /// </summary>
    public class ISO7816Card : SmartCard
    {
        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="ISO7816Card"/> class with the specified scope.
        /// </summary>
        /// <param name="scope">The scope of the smart card.</param>
        public ISO7816Card(SmartCardScope scope) : base(scope)
        {
        }

        #endregion

        #region SmartCard

        /// <summary>
        /// Gets the maximum size of the APDU data.
        /// </summary>
        public override int MaxAPDUDataSize => 255;

        /// <summary>
        /// Gets the chaining bit used in APDU commands.
        /// </summary>
        public override byte ChainingBit => 0x10;

        /// <summary>
        /// Transmits a command to the smart card and returns the response.
        /// </summary>
        /// <param name="commandBytes">The command bytes to be transmitted.</param>
        /// <returns>The response bytes from the smart card.</returns>
        /// <exception cref="NotImplementedException">Thrown when the method is not implemented.</exception>
        protected override byte[] Transmit(byte[] commandBytes)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}