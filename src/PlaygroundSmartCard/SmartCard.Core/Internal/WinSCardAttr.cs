namespace SmartCard.Core.Internal
{
    /// <summary>
    /// Represents the WinSCard attribute constants.
    /// </summary>
    internal struct WinSCardAttr
    {
        // ReSharper disable InconsistentNaming

        public const uint SCARD_ATTR_ATR_STRING = 0x00090303;
        public const uint SCARD_ATTR_CHANNEL_ID = 0x00020110;
        public const uint SCARD_ATTR_CHARACTERISTICS = 0x00060150;
        public const uint SCARD_ATTR_CURRENT_BWT = 0x00080207;
        public const uint SCARD_ATTR_CURRENT_CLK = 0x00080202;
        public const uint SCARD_ATTR_CURRENT_CWT = 0x00080208;
        public const uint SCARD_ATTR_CURRENT_D = 0x00080204;
        public const uint SCARD_ATTR_CURRENT_EBC_ENCODING = 0x0008020A;
        public const uint SCARD_ATTR_CURRENT_F = 0x00080203;
        public const uint SCARD_ATTR_CURRENT_IFSC = 0x0008020B;
        public const uint SCARD_ATTR_CURRENT_IFSD = 0x0008020C;
        public const uint SCARD_ATTR_CURRENT_N = 0x00080205;
        public const uint SCARD_ATTR_CURRENT_PROTOCOL_TYPE = 0x00080201;
        public const uint SCARD_ATTR_CURRENT_W = 0x00080206;
        public const uint SCARD_ATTR_DEFAULT_CLK = 0x00030121;
        public const uint SCARD_ATTR_DEFAULT_DATA_RATE = 0x00030123;
        public const uint SCARD_ATTR_DEVICE_FRIENDLY_NAME = 0x00010103;
        public const uint SCARD_ATTR_DEVICE_IN_USE = 0x00010101;
        public const uint SCARD_ATTR_DEVICE_SYSTEM_NAME = 0x00010104;
        public const uint SCARD_ATTR_DEVICE_UNIT = 0x00010100;
        public const uint SCARD_ATTR_ICC_INTERFACE_STATUS = 0x00090301;
        public const uint SCARD_ATTR_ICC_PRESENCE = 0x00090300;
        public const uint SCARD_ATTR_ICC_TYPE_PER_ATR = 0x00090302;
        public const uint SCARD_ATTR_MAX_CLK = 0x00030122;
        public const uint SCARD_ATTR_MAX_DATA_RATE = 0x00030124;
        public const uint SCARD_ATTR_MAX_IFSD = 0x00030125;
        public const uint SCARD_ATTR_POWER_MGMT_SUPPORT = 0x00040131;
        public const uint SCARD_ATTR_PROTOCOL_TYPES = 0x00030120;
        public const uint SCARD_ATTR_VENDOR_IFD_SERIAL_NO = 0x00010102;
        public const uint SCARD_ATTR_VENDOR_IFD_TYPE = 0x00010105;
        public const uint SCARD_ATTR_VENDOR_IFD_VERSION = 0x00010106;
        public const uint SCARD_ATTR_VENDOR_NAME = 0x00010107;
    }
}