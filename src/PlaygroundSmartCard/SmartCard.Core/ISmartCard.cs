using System;

namespace SmartCard.Core
{
    /// <summary>
    /// Represents a smart card interface.
    /// </summary>
    public interface ISmartCard : IDisposable
    {
        /// <summary>
        /// Gets or sets the name of the smart card reader.
        /// </summary>
        string ReaderName { get; set; }

        /// <summary>
        /// Connects to the smart card.
        /// </summary>
        /// <returns>A <see cref="SmartCardResult"/> representing the result of the connection.</returns>
        SmartCardResult Connect();

        /// <summary>
        /// Disconnects from the smart card.
        /// </summary>
        /// <returns>A <see cref="SmartCardResult"/> representing the result of the disconnection.</returns>
        SmartCardResult Disconnect();

        /// <summary>
        /// Gets the ATR (Answer To Reset) string of the smart card.
        /// </summary>
        /// <returns>A <see cref="SmartCardResult{T}"/> representing the result of getting the ATR string.</returns>
        SmartCardResult<string> GetATRString();
    }
}