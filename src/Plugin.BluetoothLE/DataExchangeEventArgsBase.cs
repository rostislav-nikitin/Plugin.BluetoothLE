﻿namespace Plugin.BluetoothLE.Abstractions
{
    using System;

    public abstract class DataExchangeEventArgsBase
    {
        public DataExchangeEventArgsBase(Memory<byte> buffer)
        {
            Buffer = buffer;
        }

        public Memory<byte> Buffer { get; }
    }
}
