using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gibdd
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        public const string LabelSampleText = "CONST TEXT";
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var result = await DisplayAlert("BUGAGA", "Alert!", "ok", "no");
            await DisplayAlert("Result", result ? "yes" : "no", "ok");
            var result1 = await DisplayActionSheet("TITLE", "btn1", "btn2", "btn3", "btn4", "btn5");
            btn.Text = result1;
        }
    }
}
