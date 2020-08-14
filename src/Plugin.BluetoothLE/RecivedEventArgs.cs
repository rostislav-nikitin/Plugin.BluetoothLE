namespace Plugin.BluetoothLE.Abstractions
{
    using System;

    public class RecivedEventArgs : DataExchangeEventArgsBase
    {
        public RecivedEventArgs(Memory<byte> buffer) : base(buffer)
        {
        }
    }
}