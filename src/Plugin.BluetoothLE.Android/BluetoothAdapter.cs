[assembly: Xamarin.Forms.Dependency(typeof(Plugin.BluetoothLE.Droid.BluetoothAdapter))]

namespace Plugin.BluetoothLE.Droid
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Plugin.BluetoothLE.Abstractions;

    using AndroidBluetooth = Android.Bluetooth.BluetoothAdapter;

    
    public class BluetoothAdapter : IBluetoothAdapter
    {
        public bool BluetoothSupported => AndroidBluetooth.DefaultAdapter != null;


        public bool Enabled => AndroidBluetooth.DefaultAdapter.IsEnabled;

        public IEnumerable<BluetoothDeviceModel> BondedDevices =>
            AndroidBluetooth.DefaultAdapter.BondedDevices.Where(device => 
                device.Type == Android.Bluetooth.BluetoothDeviceType.Dual
                || device.Type == Android.Bluetooth.BluetoothDeviceType.Le)
            .Select(device => new BluetoothDeviceModel(device.Address, device.Name));

        public IBluetoothConnection CreateConnection(BluetoothDeviceModel bluetoothDeviceModel)
        {
            return new BluetoothConnection(bluetoothDeviceModel.Address);
        }

        public IBluetoothManagedConnection CreateManagedConnection(BluetoothDeviceModel bluetoothDeviceModel)
        {
            return new BluetoothManagedConnection(bluetoothDeviceModel.Address);
        }

        public void Disable()
        {
            AndroidBluetooth.DefaultAdapter.Disable();
        }

        public void Enable()
        {
            AndroidBluetooth.DefaultAdapter.Enable();
        }

        public void StartDiscovery()
        {
            throw new NotImplementedException();
        }

        public void StopDiscovery()
        {
            throw new NotImplementedException();
        }
    }
}