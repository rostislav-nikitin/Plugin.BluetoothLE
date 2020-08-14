﻿namespace Plugin.BluetoothLE.Abstractions
{
    using System;

    public class StateChangedEventArgs : EventArgs
    {
        public StateChangedEventArgs(ConnectionState connectionState)
        {
            ConnectionState = connectionState;
        }

        public ConnectionState ConnectionState { get; }
    }
}