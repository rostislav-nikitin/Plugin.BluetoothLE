using Plugin.BluetoothLE.Abstractions;

namespace Plugin.BluetoothLE.Droid
{
    internal class ConnectionStateChangeOccuredEventArgs
    {
        public ConnectionStateChangeOccuredEventArgs(GattStatus status, ProfileState newState)
        {
            Status = status;
            NewState = newState;
        }

        public GattStatus Status { get; }
        public ProfileState NewState { get; }
    }
}