namespace Plugin.BluetoothLE.Abstractions
{
    using System;

    public class BluetoothGattDescriptorModel
    {
        public BluetoothGattDescriptorModel(Guid uuid, GattDescriptorPermission permissions)
        {
            Uuid = uuid;
            Permissions = permissions;
        }


        public Guid Uuid { get; }

        public GattDescriptorPermission Permissions { get; }
        public BluetoothGattCharacteristicModel Characteristic { get; set; }
    }
}