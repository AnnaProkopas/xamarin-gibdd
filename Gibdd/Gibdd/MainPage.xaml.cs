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

            //(this.Content as StackLayout)
            layout.Children.Add(secondLabel);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            complexLabel.Text = "I'm clicked";
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
