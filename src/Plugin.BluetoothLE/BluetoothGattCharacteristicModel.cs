namespace Plugin.BluetoothLE.Abstractions
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class BluetoothGattCharacteristicModel
    {
        public BluetoothGattCharacteristicModel(
            Guid serviceUuid,
            Guid uuid,
            GattProperty properties,
            GattPermission permissions,
            IEnumerable<BluetoothGattDescriptorModel> descriptors)
        {
            ServiceUuid = serviceUuid;
            Uuid = uuid;
            Properties = properties;
            Permissions = permissions;
            Descriptors = descriptors;
        }

        public BluetoothServiceModel Service { get; set; }

        public Guid Uuid { get; }

        public GattProperty Properties { get; }

        public bool CanRead => ((int)Properties & (int)GattProperty.Read) != 0;

        public bool CanWrite => ((int)Properties & (int)GattProperty.Write) != 0;

        public bool CanBeNotified => ((int)Properties & (int)GattProperty.Notify) != 0;

        public GattPermission Permissions { get; }

        public IEnumerable<BluetoothGattDescriptorModel> Descriptors { get; }

        public IEnumerable<GattProperty> PropertiesAsArray
        {
            get
            {
                List<GattProperty> result = new List<GattProperty>();

                var propertiesAsInt = (int)Properties;

                foreach (var value in Enum.GetValues(typeof(GattProperty)))
                {
                    int valueAsInt = (int)value;
                    if ((propertiesAsInt & valueAsInt) == valueAsInt)
                    {
                        result.Add((GattProperty)valueAsInt);
                    }
                }

                return result;
            }
        }

        public string PropertiesAsString
        {
            get
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (var property in PropertiesAsArray)
                {
                    stringBuilder.AppendLine(property.ToString());
                }

                return stringBuilder.ToString();
            }
        }

        public IEnumerable<GattPermission> PermissionsAsArray
        {
            get
            {
                List<GattPermission> result = new List<GattPermission>();

                var permissionsAsInt = (int)Permissions;

                foreach (var value in Enum.GetValues(typeof(GattPermission)))
                {
                    int valueAsInt = (int)value;
                    if((permissionsAsInt & valueAsInt) == valueAsInt)
                    {
                        result.Add((GattPermission)valueAsInt);
                    }
                }

                return result;
            }
        }

        public string PermissionsAsString
        {
            get
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach(var permission in PermissionsAsArray)
                {
                    stringBuilder.AppendLine(permission.ToString());
                }

                return stringBuilder.ToString();
            }
        }

        public Guid ServiceUuid { get; }
    }
}