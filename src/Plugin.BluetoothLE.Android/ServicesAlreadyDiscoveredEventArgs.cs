namespace Plugin.BluetoothLE.Droid
{
    using Android.Bluetooth;
    using Android.Runtime;
    
    internal class ServicesAlreadyDiscoveredEventArgs
    {

        public ServicesAlreadyDiscoveredEventArgs([GeneratedEnum] GattStatus status)
        {
            Status = status;
        }

        public GattStatus Status { get; }
    }
}