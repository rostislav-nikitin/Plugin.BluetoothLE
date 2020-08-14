using Plugin.BluetoothLE.Abstractions;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Discovery
{
    public partial class App : Application
    {
        public static IBluetoothManagedConnection Connection { get; internal set; }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new SelectDevicePage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
