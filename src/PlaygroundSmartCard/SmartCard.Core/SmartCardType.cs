using System.ComponentModel;

namespace SmartCard.Core
{
    /// <summary>
    /// Represents the type of smart card.
    /// </summary>
    [DefaultValue(Unknown)]
    public enum SmartCardType
    {
        Unknown,
        EMV,
        Mifare,
        Scosta,
        SIM
    }
}