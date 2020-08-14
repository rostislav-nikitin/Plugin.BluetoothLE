namespace Plugin.BluetoothLE.Abstractions
{
    public enum GattPermission
    {
        Read = 1,
        ReadEncrypted = 2,
        ReadEncryptedMitm = 4,
        Write = 16,
        WriteEncrypted = 32,
        WriteEncryptedMitm = 64,
        WriteSigned = 128,
        WriteSignedMitm = 256
    }
}
