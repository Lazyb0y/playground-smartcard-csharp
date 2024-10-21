using System.ComponentModel;

namespace SmartCard.Core
{
    /// <summary>
    /// Represents the type of smart card.
    /// </summary>
    [DefaultValue(Unknown)]
    public enum SmartCardType
    {
        // ReSharper disable InconsistentNaming

        Unknown,

        // MIFARE Cards (NXP Semiconductors)
        MIFARE_Classic_1K,
        MIFARE_Classic_4K,
        MIFARE_Plus_S,
        MIFARE_Plus_X,
        MIFARE_Plus_SE,
        MIFARE_DESFire_EV1,
        MIFARE_DESFire_EV2,
        MIFARE_DESFire_EV3,
        MIFARE_Ultralight_C,
        MIFARE_Ultralight_EV1,
        MIFARE_ProX,
        MIFARE_SmartMX,

        // EMV Cards (Payment Standards)
        EMV_Contact,
        EMV_Contactless,
        EMV_Dual_Interface,
        EMVCo_Tokenization,
        Chip_PIN,
        Chip_Signature,

        // SIM Cards (Subscriber Identity Module)
        SIM_Full_Size_1FF,
        SIM_Mini_2FF,
        SIM_Micro_3FF,
        SIM_Nano_4FF,
        SIM_Soft,
        eSIM,
        rSIM
    }
}