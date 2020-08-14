namespace Plugin.BluetoothLE.Droid
{
    using Java.Util;

    internal class CharacteristicAlreadyChangedEventArgs
    {

        public CharacteristicAlreadyChangedEventArgs(UUID uuid, byte[] value)
        {
            Uuid = uuid;
            Value = value;
        }

        public UUID Uuid { get; }

        public byte[] Value { get; }
    }
}