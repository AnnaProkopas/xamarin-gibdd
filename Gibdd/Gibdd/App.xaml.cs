using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gibdd
{
    public partial class App : Application
    {
        public App(IPhotographerPlatform platform)
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage(platform));
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
