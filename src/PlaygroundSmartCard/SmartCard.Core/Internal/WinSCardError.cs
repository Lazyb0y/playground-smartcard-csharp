namespace SmartCard.Core.Internal
{
    /// <summary>
    /// Represents the WinSCard error codes.
    /// </summary>
    internal struct WinSCardError
    {
        // ReSharper disable InconsistentNaming

        // Success
        public const uint SCARD_S_SUCCESS = 0x00000000;

        // Errors
        public const uint SCARD_E_CANCELLED = 0x80100002;
        public const uint SCARD_E_INVALID_HANDLE = 0x80100003;
        public const uint SCARD_E_INVALID_PARAMETER = 0x80100004;
        public const uint SCARD_E_INVALID_TARGET = 0x80100005;
        public const uint SCARD_E_NO_MEMORY = 0x80100006;
        public const uint SCARD_F_WAITED_TOO_LONG = 0x80100007;
        public const uint SCARD_E_INSUFFICIENT_BUFFER = 0x80100008;
        public const uint SCARD_E_UNKNOWN_READER = 0x80100009;
        public const uint SCARD_E_TIMEOUT = 0x8010000A;
        public const uint SCARD_E_SHARING_VIOLATION = 0x8010000B;
        public const uint SCARD_E_NO_SMARTCARD = 0x8010000C;
        public const uint SCARD_E_UNKNOWN_CARD = 0x8010000D;
        public const uint SCARD_E_CANT_DISPOSE = 0x8010000E;
        public const uint SCARD_E_PROTO_MISMATCH = 0x8010000F;
        public const uint SCARD_E_NOT_READY = 0x80100010;
        public const uint SCARD_E_INVALID_VALUE = 0x80100011;
        public const uint SCARD_E_SYSTEM_CANCELLED = 0x80100012;
        public const uint SCARD_F_COMM_ERROR = 0x80100013;
        public const uint SCARD_F_UNKNOWN_ERROR = 0x80100014;
        public const uint SCARD_E_INVALID_ATR = 0x80100015;
        public const uint SCARD_E_NOT_TRANSACTED = 0x80100016;
        public const uint SCARD_E_READER_UNAVAILABLE = 0x80100017;
        public const uint SCARD_P_SHUTDOWN = 0x80100018;
        public const uint SCARD_E_PCI_TOO_SMALL = 0x80100019;
        public const uint SCARD_E_READER_UNSUPPORTED = 0x8010001A;
        public const uint SCARD_E_DUPLICATE_READER = 0x8010001B;
        public const uint SCARD_E_CARD_UNSUPPORTED = 0x8010001C;
        public const uint SCARD_E_NO_SERVICE = 0x8010001D;
        public const uint SCARD_E_SERVICE_STOPPED = 0x8010001E;
        public const uint SCARD_E_UNEXPECTED = 0x8010001F;
        public const uint SCARD_E_ICC_INSTALLATION = 0x80100020;
        public const uint SCARD_E_ICC_CREATEORDER = 0x80100021;
        public const uint SCARD_E_UNSUPPORTED_FEATURE = 0x80100022;
        public const uint SCARD_E_DIR_NOT_FOUND = 0x80100023;
        public const uint SCARD_E_FILE_NOT_FOUND = 0x80100024;
        public const uint SCARD_E_NO_DIR = 0x80100025;
        public const uint SCARD_E_NO_FILE = 0x80100026;
        public const uint SCARD_E_NO_ACCESS = 0x80100027;
        public const uint SCARD_E_WRITE_TOO_MANY = 0x80100028;
        public const uint SCARD_E_BAD_SEEK = 0x80100029;
        public const uint SCARD_E_INVALID_CHV = 0x8010002A;
        public const uint SCARD_E_UNKNOWN_RES_MNG = 0x8010002B;
        public const uint SCARD_E_NO_SUCH_CERTIFICATE = 0x8010002C;
        public const uint SCARD_E_CERTIFICATE_UNAVAILABLE = 0x8010002D;
        public const uint SCARD_E_NO_READERS_AVAILABLE = 0x8010002E;
        public const uint SCARD_E_COMM_DATA_LOST = 0x8010002F;
        public const uint SCARD_E_NO_KEY_CONTAINER = 0x80100030;
        public const uint SCARD_E_SERVER_TOO_BUSY = 0x80100031;
        public const uint SCARD_E_PIN_CACHE_EXPIRED = 0x80100032;
        public const uint SCARD_E_NO_PIN_CACHE = 0x80100033;
        public const uint SCARD_E_READ_ONLY_CARD = 0x80100034;

        // Warnings
        public const uint SCARD_W_UNSUPPORTED_CARD = 0x80100065;
        public const uint SCARD_W_UNRESPONSIVE_CARD = 0x80100066;
        public const uint SCARD_W_UNPOWERED_CARD = 0x80100067;
        public const uint SCARD_W_RESET_CARD = 0x80100068;
        public const uint SCARD_W_REMOVED_CARD = 0x80100069;
        public const uint SCARD_W_SECURITY_VIOLATION = 0x8010006A;
        public const uint SCARD_W_WRONG_CHV = 0x8010006B;
        public const uint SCARD_W_CHV_BLOCKED = 0x8010006C;
        public const uint SCARD_W_EOF = 0x8010006D;
        public const uint SCARD_W_CANCELLED_BY_USER = 0x8010006E;
        public const uint SCARD_W_CARD_NOT_AUTHENTICATED = 0x8010006F;
        public const uint SCARD_W_CACHE_ITEM_NOT_FOUND = 0x80100070;
        public const uint SCARD_W_CACHE_ITEM_STALE = 0x80100071;
        public const uint SCARD_W_CACHE_ITEM_TOO_BIG = 0x80100072;
    }
}