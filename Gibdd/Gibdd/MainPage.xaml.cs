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

            // MainPage.xaml.g.cs :
            //this..LoadFromXaml(""); //- позволяет загружать странички с серверов без обновления приложения
            InitializeComponent();

            var secondLabel = new Label();
            secondLabel.Text = "I'm the second";
            ImageSource imageSource = ImageSource.FromUri(new Uri("https://cdn.kapwing.com/final_5d0bcfdd9d322b0014964492_224032.jpg"));
            image.Source = imageSource;

            //(this.Content as StackLayout)
            layout.Children.Add(secondLabel);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            complexLabel.Text = "I'm clicked";
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            defaultLabel.Text = "You wrote " + e.NewTextValue;
        }

        private void Entry_Completed(object sender, EventArgs e)
        {
            defaultLabel.Text = "Ok, got it: " + editBox.Text;
        }
    }

    public class RedColorExtension : IMarkupExtension
    //RedColorExtension ~ RedColor
    {
        public double Redness { get; set; } // { get; set; } позволяет брать и получать
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return Color.FromRgb(Redness, 0, 0);
        }
    }
}
