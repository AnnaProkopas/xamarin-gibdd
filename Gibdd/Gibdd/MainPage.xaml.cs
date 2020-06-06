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

            var trigger = new Trigger(typeof(Entry));
            trigger.Property = Entry.IsFocusedProperty;
            trigger.Value = true;

            trigger.Setters.Add(new Setter() {
                Property = BackgroundColorProperty,
                Value = Color.LightSeaGreen
            });

            trigger.EnterActions.A(new MyTriggerAction());
            trigger.ExitActions.Add(new MyTriggerAction());

            NumberBox.Triggers.Add(trigger);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            switch (Device.Idiom) {
                case TargetIdiom.Phone:
                    btn.Text = "btn on phone";
                    break;
                case TargetIdiom.Tablet:
                    btn.Text = "btn on tablet";
                    break;
                case TargetIdiom.Watch:
                    btn.Text = "btn on watch";
                    break;
            }
        }

        private void Button_Switch_Color_Clicked(object sender, EventArgs e)
        {
            Resources["buttonColor"] = Color.LawnGreen;
        }

        public class MyTriggerAction : TriggerAction<Entry>
        {
            protected override void Invoke(Entry sender)
            {
                if (sender.IsFocused)
                {
                    sender.FadeTo(1);
                }
                else {
                    sender.FadeTo(0.5);
                }
            }
        }
    }
}
