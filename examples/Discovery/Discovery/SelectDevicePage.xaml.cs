namespace Discovery
{
    using Plugin.BluetoothLE.Abstractions;
    using System;
    using System.ComponentModel;
    using Xamarin.Forms;

    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class SelectDevicePage : ContentPage
    {
        public SelectDevicePage()
        {
            InitializeComponent();
            InitializeUI();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            CheckAndDisposeConnection();
        }

        private void CheckAndDisposeConnection()
        {
            if (App.Connection != null)
            {
                App.Connection.Dispose();
            }
        }


        private void InitializeUI()
        {
            var adapter = DependencyService.Resolve<IBluetoothAdapter>();
            lvBondedDevices.ItemsSource = adapter.BondedDevices;
        }

        private async void lvBondedDevices_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var device = (BluetoothDeviceModel)e.SelectedItem;
            if(device != null)
            {
                await Navigation.PushAsync(new DeviceServicesPage { BindingContext = device });
            }

            lvBondedDevices.SelectedItem = null;
        }
    }
}
