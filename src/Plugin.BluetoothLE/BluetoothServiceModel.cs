namespace Plugin.BluetoothLE.Abstractions
{
    using System;
    using System.Collections.Generic;

    public class BluetoothServiceModel
    {
        public BluetoothServiceModel(BluetoothDeviceModel device, Guid uuid, IEnumerable<BluetoothGattCharacteristicModel> characteristics
            )
        {
            Device = device;
            Uuid = uuid;
            Characteristics = characteristics;
        }
        public BluetoothDeviceModel Device { get; set; }
        public Guid Uuid { get; }
        public IEnumerable<BluetoothGattCharacteristicModel> Characteristics { get; set; }

    }
}