using Plugin.BluetoothLE.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Discovery
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeviceCharacteristicDescriptorsPage : ContentPage
    {
        private static readonly object _syncRoot = new object();
        private bool _reciving;

        public DeviceCharacteristicDescriptorsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var characteristic = (BluetoothGattCharacteristicModel)BindingContext;
            if(characteristic != null)
            {
                lvDescriptors.ItemsSource = characteristic.Descriptors;
            }
        }

        private void buttonTransmit_Clicked(object sender, EventArgs e)
        {
            var characteristic = (BluetoothGattCharacteristicModel)BindingContext;
            if (characteristic != null && App.Connection != null)
            {
                App.Connection.Transmit(characteristic, new byte[] { 5 });
            }
        }

        private async void buttonRecive_Clicked(object sender, EventArgs e)
        {
            var characteristic = (BluetoothGattCharacteristicModel)BindingContext;
            if (characteristic != null)
            {
                byte[] data = App.Connection.Recive(characteristic);
                if(data != null && data.Length > 0)
                {
                    await DisplayAlert("Info", data[0].ToString(), "Close");
                }
            }
        }

        private void buttonNotify_Clicked(object sender, EventArgs e)
        {
            var characteristic = (BluetoothGattCharacteristicModel)BindingContext;
            if (characteristic != null)
            {
                App.Connection.Notify(characteristic);

                App.Connection.OnRecived += Connection_OnRecived;
            }
        }

        private void Connection_OnRecived(object sender, RecivedEventArgs recivedEventArgs)
        {
            if (recivedEventArgs.Buffer.Length > 0)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    //System.Threading.Thread.Sleep(100);
                    //await DisplayAlert("Notify", recivedEventArgs.Buffer.ToArray()[0].ToString(), "Close");
                    //editorTransmit.Text = recivedEventArgs.Buffer.ToArray()[0].ToString();
                    _reciving = true;
                    stepperDigit.Value = recivedEventArgs.Buffer.ToArray()[0];
                    _reciving = false;
                });
            }
        }

        private void Stepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (!_reciving)
            {
                var characteristic = (BluetoothGattCharacteristicModel)BindingContext;
                if (characteristic != null && App.Connection != null)
                {
                    App.Connection.Transmit(characteristic, new byte[] { (byte)stepperDigit.Value });
                }
            }
        }
    }
}