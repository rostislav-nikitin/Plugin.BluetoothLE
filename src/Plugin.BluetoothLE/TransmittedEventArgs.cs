namespace Plugin.BluetoothLE.Abstractions
{
    using System;

    public class TransmittedEventArgs : DataExchangeEventArgsBase
    {
        public TransmittedEventArgs(Memory<byte> buffer) : base(buffer)
        {
        }
    }
}
