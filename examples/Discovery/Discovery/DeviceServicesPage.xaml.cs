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
    public partial class DeviceServicesPage : ContentPage
    {
        public DeviceServicesPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            CheckAndCreateConnection();
        }

        private void CheckAndCreateConnection()
        {
            if (App.Connection == null)
            {
                CreateConnection();
            }
        }

        private void CreateConnection()
        {
            var device = (BluetoothDeviceModel)BindingContext;
            if (device != null)
            {
                var adapter = DependencyService.Resolve<IBluetoothAdapter>();
                var connection = adapter.CreateManagedConnection(device);
                connection.OnStateChanged += Connection_OnStateChanged;
                connection.OnServicesDiscovered += Connection_OnServicesDiscovered;
                connection.Connect();
                App.Connection = connection;
            }
        }

        private void Connection_OnStateChanged(object sender, StateChangedEventArgs stateChangedEventArgs)
        {
            if(stateChangedEventArgs.ConnectionState == ConnectionState.Connected)
            {
                App.Connection.DiscoverServices();
            }
        }

        private void Connection_OnServicesDiscovered(object sender, ServicesDiscoveredEventArgs servicesDiscoveredEventArgs)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                lvServices.ItemsSource = servicesDiscoveredEventArgs.Services.ToList();
            });
        }

        private async void lvServices_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var service = (BluetoothServiceModel)e.SelectedItem;
            if(service != null)
            {
                await Navigation.PushAsync(new DeviceServiceCharactristicsPage { BindingContext = service });
            }
        }
    }
}