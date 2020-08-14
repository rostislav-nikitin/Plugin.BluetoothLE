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
    public partial class DeviceServiceCharactristicsPage : ContentPage
    {
        public DeviceServiceCharactristicsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var service = (BluetoothServiceModel)BindingContext;

            if (service != null)
            {
                lvCharcteristics.ItemsSource = service.Characteristics;
            }
        }

        private async void lvCharcteristics_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var characteristic = (BluetoothGattCharacteristicModel)e.SelectedItem;

            if(characteristic != null)
            {
                await Navigation.PushAsync(
                    new DeviceCharacteristicDescriptorsPage { BindingContext = characteristic });
            }

            lvCharcteristics.SelectedItem = null;
        }
    }
}