﻿namespace Plugin.BluetoothLE.Abstractions
{
    public class BluetoothDeviceModel
    {
        public BluetoothDeviceModel(string Address, string Name)
        {
            this.Address = Address;
            this.Name = Name;
        }

        public string Address { get; }
        public string Name { get; }
    }
}