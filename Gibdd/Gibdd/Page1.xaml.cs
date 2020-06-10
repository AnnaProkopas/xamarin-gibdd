using System;
using System.Collections.Generic;
using System.IO;
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
        private byte[] imageData;

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
        private void PhotoTaken(byte[] imageData)
        {
            this.imageData = imageData;
            var source = ImageSource.FromStream(() => new MemoryStream(imageData));
            takenPhoto.Source = source;
        }

        private void SaveTakenPhotp_Clicked(object sender, EventArgs e)
        {
            
            platform.SaveImage(imageData);
        }
    }
}