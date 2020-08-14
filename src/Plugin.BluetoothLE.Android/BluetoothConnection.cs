namespace Plugin.BluetoothLE.Droid
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Android.Bluetooth;
    using Android.Runtime;
    using Java.Util;
    using Plugin.BluetoothLE.Abstractions;
    using Xamarin.Forms.Internals;

    public class BluetoothConnection : IBluetoothConnection
    {
        private string _address;
        private Android.Bluetooth.BluetoothGatt _gatt;

        public BluetoothConnection()
        {
            /*Android.Bluetooth.BluetoothAdapter adapter = Android.Bluetooth.BluetoothAdapter.DefaultAdapter;

            var device = adapter.BondedDevices.Where(device => device.Type == BluetoothDeviceType.Dual
                || device.Type == BluetoothDeviceType.Le).FirstOrDefault();

            Context context = Android.App.Application.Context;
            var callback = new GattCallback();

            device.ConnectGatt(context, true, callback);*/
            
        }

        public BluetoothConnection(string address)
        {
            _address = address;
        }

        public bool DataAvailable => throw new NotImplementedException();

        public void Connect(bool autoConnect = false)
        {
            var device = Android.Bluetooth.BluetoothAdapter.DefaultAdapter?.GetRemoteDevice(_address);
            if(device == null)
            {
                throw new Exception($"Can not get remote device with address:\"{_address}\".");
            }
            var callback = new AndroidBluetoothGattCallback();
            _gatt = device.ConnectGatt(Android.App.Application.Context, autoConnect, callback);
        }

        public IEnumerable<BluetoothServiceModel> Services
        {
            get
            {
                if (_gatt.Connect() && _gatt.DiscoverServices())
                {
                    BluetoothDeviceModel device = new BluetoothDeviceModel(_address, _address);

                    var result = _gatt.Services.Select(service =>
                          new BluetoothServiceModel(device, new Guid(service.Uuid.ToString()),
                              service.Characteristics.Select(charactristic => new BluetoothGattCharacteristicModel
                              (
                                  new Guid(service.Uuid.ToString()),
                                  new Guid(charactristic.Uuid.ToString()),
                                  (Abstractions.GattProperty)charactristic.Properties,
                                  (Abstractions.GattPermission)charactristic.Permissions,
                                  charactristic.Descriptors.Select(descriptor =>
                                      new BluetoothGattDescriptorModel(new Guid(descriptor.Uuid.ToString()), 
                                      (Abstractions.GattDescriptorPermission)descriptor.Permissions)
                              ))
                          )));

                    return result;
                }
                else
                {
                    throw new Exception($"Can not connect or discover services for device with address: \"{_address}\".");
                }
            }
        }

        public void Dispose()
        {
            try
            {
                if (_gatt != null)
                {
                    _gatt.Close();
                    _gatt = null;
                }
            }
            catch(Exception exception)
            {
                Log.Warning("Dispose::Exception", exception.Message);
            }
        }

        public Task<int> ReciveAsync(Memory<byte> buffer, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<int> ReciveAsync(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public Task<int> ReciveAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public bool Transmit(BluetoothGattCharacteristicModel characteristicModel,
            Memory<byte> buffer)
        {
            return Transmit(characteristicModel, buffer.ToArray());
        }

        public bool Transmit(BluetoothGattCharacteristicModel characteristicModel, byte[] buffer)
        {
            /*BluetoothGattCharacteristic characteristic =
                new BluetoothGattCharacteristic(UUID.FromString(characteristicModel.Uuid.ToString()),
                    (Android.Bluetooth.GattProperty)characteristicModel.Properties,
                    (Android.Bluetooth.GattPermission)characteristicModel.Permissions);*/
            var characteristic = _gatt.GetService(UUID.FromString(characteristicModel.ServiceUuid.ToString()))
                .GetCharacteristic(UUID.FromString(characteristicModel.Uuid.ToString()));

            characteristic.SetValue(buffer);

            bool result = _gatt.WriteCharacteristic(characteristic);

            return result;
        }


    }

}
 