using System.Collections.Generic;

namespace SmartCard.Core
{
    /// <summary>
    /// Represents a class for identifying smart cards based on their ATR (Answer To Reset).
    /// </summary>
    public class SmartCardIdentifier
    {
        #region Decleration(s)

        private static readonly List<ATRDatabase> ATRDatabase = new List<ATRDatabase>();

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of the <see cref="SmartCardIdentifier"/> class.
        /// </summary>
        static SmartCardIdentifier()
        {
            // MIFARE Cards ATRs
            ATRDatabase.Add(new ATRDatabase("3B6700000073C84013009000", SmartCardType.MIFARE_Classic_1K));
            ATRDatabase.Add(new ATRDatabase("3B68000073C84012009000", SmartCardType.MIFARE_Classic_4K));
            ATRDatabase.Add(new ATRDatabase("3B781A0073C84012109000", SmartCardType.MIFARE_Plus_S));
            ATRDatabase.Add(new ATRDatabase("3B781A0073C84012119000", SmartCardType.MIFARE_Plus_X));
            ATRDatabase.Add(new ATRDatabase("3B781A0073C84012129000", SmartCardType.MIFARE_Plus_SE));
            ATRDatabase.Add(new ATRDatabase("3B7718000073C84012009000", SmartCardType.MIFARE_DESFire_EV1));
            ATRDatabase.Add(new ATRDatabase("3B7718000073C84012019000", SmartCardType.MIFARE_DESFire_EV2));
            ATRDatabase.Add(new ATRDatabase("3B7718000073C84012029000", SmartCardType.MIFARE_DESFire_EV3));
            ATRDatabase.Add(new ATRDatabase("3B771B0073C84012109000", SmartCardType.MIFARE_Ultralight_C));
            ATRDatabase.Add(new ATRDatabase("3B771B0073C84012119000", SmartCardType.MIFARE_Ultralight_EV1));
            ATRDatabase.Add(new ATRDatabase("3B8E80018031C0520031730421", SmartCardType.MIFARE_ProX));
            ATRDatabase.Add(new ATRDatabase("3B8E80018031C0520031730431", SmartCardType.MIFARE_SmartMX));

            // EMV Cards ATRs
            ATRDatabase.Add(new ATRDatabase("3B8E80018031C052003173D221", SmartCardType.EMV_Contact));
            ATRDatabase.Add(new ATRDatabase("3B8E80018031C052003173D222", SmartCardType.EMV_Contactless));
            ATRDatabase.Add(new ATRDatabase("3B8E80018031C052003173D223", SmartCardType.EMV_Dual_Interface));
            ATRDatabase.Add(new ATRDatabase("3B8E80018031C052003173D224", SmartCardType.EMVCo_Tokenization));
            ATRDatabase.Add(new ATRDatabase("3B8E80018031C052003173D225", SmartCardType.Chip_PIN));
            ATRDatabase.Add(new ATRDatabase("3B8E80018031C052003173D226", SmartCardType.Chip_Signature));

            // SIM Cards ATRs
            ATRDatabase.Add(new ATRDatabase("3B8E80018031C052003173FF01", SmartCardType.SIM_Full_Size_1FF));
            ATRDatabase.Add(new ATRDatabase("3B8E80018031C052003173FF02", SmartCardType.SIM_Mini_2FF));
            ATRDatabase.Add(new ATRDatabase("3B8E80018031C052003173FF03", SmartCardType.SIM_Micro_3FF));
            ATRDatabase.Add(new ATRDatabase("3B8E80018031C052003173FF04", SmartCardType.SIM_Nano_4FF));
            ATRDatabase.Add(new ATRDatabase("3B8E80018031C052003173FF05", SmartCardType.eSIM));
            ATRDatabase.Add(new ATRDatabase("3B8E80018031C052003173FF06", SmartCardType.rSIM));
            ATRDatabase.Add(new ATRDatabase("3B8E80018031C052003173FF07", SmartCardType.SIM_Soft));
        }

        #endregion

        #region Method(s)

        /// <summary>
        /// Identifies the type of the smart card based on the provided ATR.
        /// </summary>
        /// <param name="atr">The ATR (Answer To Reset) of the smart card.</param>
        /// <returns>The <see cref="SmartCardType"/> of the identified smart card.</returns>
        public static SmartCardType Identify(ATR atr)
        {
            var normalizedAtr = ATR.Normalize(atr.String);

            foreach (var card in ATRDatabase)
            {
                if (normalizedAtr == card.NormalizeATR)
                {
                    return card.CardType;
                }
            }

            return SmartCardType.Unknown;
        }

        #endregion
    }
}