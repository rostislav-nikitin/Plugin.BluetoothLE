namespace Plugin.BluetoothLE.Droid
{
    using AndroidBluetooth = Android.Bluetooth;
    using Plugin.BluetoothLE.Abstractions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Android.App;
    using Xamarin.Forms;
    using Android.Bluetooth;
    using Xamarin.Forms.Internals;

    internal class BluetoothManagedConnection : IBluetoothManagedConnection
    {
        private readonly string _address;
        private AndroidBluetooth.BluetoothGatt _gatt;

        public BluetoothManagedConnection(string address)
        {
            this._address = address;
        }

        public ConnectionState ConnectionState => throw new NotImplementedException();

        public event ServicesDiscovered OnServicesDiscovered;
        public event StateChanged OnStateChanged;
        public event Transmitted OnTransmitted;
        public event Recived OnRecived;
        public event Error OnError;

        public void Connect(bool autoConnect = false)
        {
            var device = AndroidBluetooth.BluetoothAdapter.DefaultAdapter?.GetRemoteDevice(_address);
            if (device == null)
            {
                throw new Exception($"Can not get remote device with address:\"{_address}\".");
            }
            var callback = new AndroidBluetoothGattCallback();
            callback.OnConnectionStateChangeOccured += Callback_OnConnectionStateChangeOccured;
            callback.OnServicesAlreadyDiscovered += Callback_OnServicesAlreadyDiscovered;
            callback.OnCharacteristicAlreadyChanged += Callback_OnCharacteristicAlreadyChanged;
            _gatt = device.ConnectGatt(Android.App.Application.Context, autoConnect, callback);
        }

        private void Callback_OnCharacteristicAlreadyChanged(object sender, CharacteristicAlreadyChangedEventArgs characteristicAlreadyChangedEventArgs)
        {
            OnRecived?.Invoke(this, new RecivedEventArgs(new Memory<byte>(characteristicAlreadyChangedEventArgs.Value)));
        }

        private void Callback_OnConnectionStateChangeOccured(object sender, ConnectionStateChangeOccuredEventArgs connectionStateChangeOccuredEventArgs)
        {
            var connectionStateMapper = DependencyService.Resolve<ConnectionStateMapper>();

            OnStateChanged?.Invoke(this, new StateChangedEventArgs(connectionStateMapper.Map(connectionStateChangeOccuredEventArgs.NewState)));
        }

        private void Callback_OnServicesAlreadyDiscovered(object sender, ServicesAlreadyDiscoveredEventArgs servicesDiscoveredEventArgs)
        {
            var services = Services.ToList();

            if (servicesDiscoveredEventArgs.Status == AndroidBluetooth.GattStatus.Success)
            {

                OnServicesDiscovered?.Invoke(this, new ServicesDiscoveredEventArgs(
                    Map(servicesDiscoveredEventArgs.Status), Services));
            }
            else
            {
                ;
            }
        }

        private Abstractions.GattStatus Map(AndroidBluetooth.GattStatus status)
        {
            return (Abstractions.GattStatus)(int)status;
        }

        public void DiscoverServices()
        {
            _gatt.DiscoverServices();
        }

        public IEnumerable<BluetoothServiceModel> Services
        {
            get
            {
                var result = _gatt.Services.Select(service =>
                      new BluetoothServiceModel(new BluetoothDeviceModel(_address, _address),
                        new Guid(service.Uuid.ToString()),
                          service.Characteristics.Select(charactristic => new BluetoothGattCharacteristicModel
                           (new Guid(service.Uuid.ToString()),
                              new Guid(charactristic.Uuid.ToString()),
                                (Abstractions.GattProperty)(int)charactristic.Properties,
                                (Abstractions.GattPermission)(int)charactristic.Permissions, 
                                charactristic.Descriptors.Select(descriptor =>
                                  new BluetoothGattDescriptorModel(new Guid(descriptor.Uuid.ToString()), 
                                  (Abstractions.GattDescriptorPermission)(int)descriptor.Permissions)
                          ))
                      )));

                /*foreach(var service in result)
                {
                    foreach(var characteristic in service.Characteristics)
                    {
                        characteristic.Service = service;
                        characteristic.ServiceUuid = service.Uuid;
                        foreach (var descriptor in characteristic.Descriptors)
                        {
                            descriptor.Characteristic = characteristic;
                        }
                    }
                }*/

                return result;
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
                Xamarin.Forms.Internals.Log.Warning("Dispose::Exception", exception.Message);
            }
        }

        public bool Transmit(BluetoothGattCharacteristicModel characteristicModel,
            Memory<byte> buffer)
        {
            return Transmit(characteristicModel, buffer.ToArray());
        }

        public bool Transmit(BluetoothGattCharacteristicModel characteristicModel, byte[] buffer)
        {
            bool result = default;

            if (_gatt.Connect())
            {
                var characteristic = _gatt.GetService(Java.Util.UUID.FromString(characteristicModel.ServiceUuid.ToString()))
                    .GetCharacteristic(Java.Util.UUID.FromString(characteristicModel.Uuid.ToString()));

                characteristic.SetValue(buffer);

                result = _gatt.WriteCharacteristic(characteristic);
            }

            return result;
        }

        public byte[] Recive(BluetoothGattCharacteristicModel bluetoothGattCharacteristicModel)
        {
            byte[] result;

            var characteristic = _gatt.GetService(Java.Util.UUID.FromString(bluetoothGattCharacteristicModel.ServiceUuid.ToString()))
                .GetCharacteristic(Java.Util.UUID.FromString(bluetoothGattCharacteristicModel.Uuid.ToString()));

            if (_gatt.ReadCharacteristic(characteristic))
            {
                result = characteristic.GetValue();
            }
            else
            {
                result = default;
            }

            return result;
        }

        public bool Notify(BluetoothGattCharacteristicModel bluetoothGattCharacteristicModel)
        {
            const bool Enable = true;

            var characteristic = _gatt.GetService(Java.Util.UUID.FromString(bluetoothGattCharacteristicModel.ServiceUuid.ToString()))
                .GetCharacteristic(Java.Util.UUID.FromString(bluetoothGattCharacteristicModel.Uuid.ToString()));

            bool result = _gatt.SetCharacteristicNotification(characteristic, Enable);

            foreach (var descriptor in characteristic.Descriptors)
            {
                descriptor.SetValue(BluetoothGattDescriptor.EnableNotificationValue.ToArray());
                _gatt.WriteDescriptor(descriptor);
            }

            return result;
        }

    }
}