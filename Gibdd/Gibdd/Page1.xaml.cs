using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gibdd
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        private IPhotographerPlatform platform;
        public Page1(IPhotographerPlatform platform)
        {
            InitializeComponent();
            this.platform = platform;
            if (!platform.IsCameraAvailable()) {
                takePhotoBtn.IsEnabled = false;
            }
            platform.PhotoCallback = PhotoTaken;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            
            Navigation.PopModalAsync();
        }

        private void takePhotoBtn_Clicked(object sender, EventArgs e)
        {
            platform.TakePhoto();
        }
        private void PhotoTaken(ImageSource source)
        {
            takenPhoto.Source = source;
        }
    }
}