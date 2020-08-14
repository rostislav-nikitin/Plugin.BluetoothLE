[assembly: Xamarin.Forms.Dependency(typeof(Plugin.BluetoothLE.Abstractions.ConnectionStateMapper))]

namespace Plugin.BluetoothLE.Abstractions
{
    using System.Runtime.Serialization;

    public enum ConnectionState
    {
        [EnumMember(Value = "Created...")]
        Created,
        [EnumMember(Value ="Initializing...")]
        Initializing,
        [EnumMember(Value = "Connecting...")]
        Connecting,
        [EnumMember(Value = "Connected")]
        Connected,
        [EnumMember(Value = "ErrorOccured")]
        ErrorOccured,
        [EnumMember(Value = "Reconnecting...")]
        Reconnecting,
        [EnumMember(Value = "Disconnecting...")]
        Disconnecting,
        [EnumMember(Value = "Disconnected")]
        Disconnected,
        [EnumMember(Value = "Disposing...")]
        Disposing,
        [EnumMember(Value = "Disposed")]
        Disposed
    }

    
    public class ConnectionStateMapper
    { 
        public ConnectionState Map(ProfileState profileState)
        {
            ConnectionState result;

            switch (profileState)
            {
                case ProfileState.Connected:
                    result = ConnectionState.Connected;
                    break;
                case ProfileState.Connecting:
                    result = ConnectionState.Connecting;
                    break;
                case ProfileState.Disconnecting:
                    result = ConnectionState.Disconnecting;
                    break;
                case ProfileState.Disconnected:
                    result = ConnectionState.Disconnected;
                    break;
                default:
                    result = ConnectionState.Created;
                    break;
            }

            return result;
        }
    }
}