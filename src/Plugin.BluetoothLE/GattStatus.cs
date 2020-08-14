namespace Plugin.BluetoothLE.Abstractions
{
    public enum GattStatus
    {
        Success = 0,
        ReadNotPermitted = 2,
        WriteNotPermitted = 3,
        InsufficientAuthentication = 5,
        RequestNotSupported = 6,
        InvalidOffset = 7,
        InvalidAttributeLength = 13,
        InsufficientEncryption = 15,
        ConnectionCongested = 143,
        Failure = 257
    }
}