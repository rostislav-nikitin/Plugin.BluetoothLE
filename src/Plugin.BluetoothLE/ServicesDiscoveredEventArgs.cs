namespace Plugin.BluetoothLE.Abstractions
{
    using System.Collections.Generic;

    public class ServicesDiscoveredEventArgs
    {

        public ServicesDiscoveredEventArgs(GattStatus status, IEnumerable<BluetoothServiceModel> services)
        {
            Status = status;
            Services = services;
        }
        public GattStatus Status { get; }

        public IEnumerable<BluetoothServiceModel> Services { get; }
    }
}