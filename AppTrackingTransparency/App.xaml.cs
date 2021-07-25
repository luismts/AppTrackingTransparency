using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XEssentialsTest.Extensions;

namespace AppTrackingTransparency
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override async void OnStart()
        {
            if(DeviceInfo.Platform == DevicePlatform.iOS)
            {
                var appTrackingTransparencyPermission = DependencyService.Get<IPermission>();
                var status = await appTrackingTransparencyPermission.CheckStatusAsync();

                if (status != PermissionStatus.Granted)                
                    status = await appTrackingTransparencyPermission.RequestAsync();
                
                if (status == PermissionStatus.Granted)
                {
                    // app center or other implementations
                }                               
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
