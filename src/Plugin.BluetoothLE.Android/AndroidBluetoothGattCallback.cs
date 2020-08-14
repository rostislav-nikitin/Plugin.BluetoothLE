namespace Plugin.BluetoothLE.Droid
{
    using Android.Bluetooth;
    using Android.Runtime;

    internal class AndroidBluetoothGattCallback : BluetoothGattCallback
    {
        internal delegate void ConnectionStateChangeOccured(object sender, ConnectionStateChangeOccuredEventArgs connectionStateChangeOccuredEventArgs);

        internal delegate void ServicesAlreadyDiscovered(object sender, ServicesAlreadyDiscoveredEventArgs servicesDiscoveredEventArgs);

        internal delegate void CharacteristicAlreadyChanged(object sender, CharacteristicAlreadyChangedEventArgs characteristicAlreadyChangedEventArgs);



        internal event ConnectionStateChangeOccured OnConnectionStateChangeOccured;
        public override void OnConnectionStateChange(BluetoothGatt gatt, [GeneratedEnum] GattStatus status, [GeneratedEnum] ProfileState newState)
        {
            base.OnConnectionStateChange(gatt, status, newState);
            OnConnectionStateChangeOccured?.Invoke(this, 
                new ConnectionStateChangeOccuredEventArgs((Abstractions.GattStatus)(int)status, 
                (Abstractions.ProfileState)(int)newState));
        }

        internal event ServicesAlreadyDiscovered OnServicesAlreadyDiscovered;

        public override void OnServicesDiscovered(BluetoothGatt gatt, [GeneratedEnum] GattStatus status)
        {
            base.OnServicesDiscovered(gatt, status);
            OnServicesAlreadyDiscovered?.Invoke(this, new ServicesAlreadyDiscoveredEventArgs(status));
        }

        internal event CharacteristicAlreadyChanged OnCharacteristicAlreadyChanged;

        public override void OnCharacteristicChanged(BluetoothGatt gatt, BluetoothGattCharacteristic characteristic)
        {
            base.OnCharacteristicChanged(gatt, characteristic);
            var value = characteristic.GetValue();
            OnCharacteristicAlreadyChanged?.Invoke(this, new CharacteristicAlreadyChangedEventArgs(characteristic.Uuid, value));
        }


    }
}