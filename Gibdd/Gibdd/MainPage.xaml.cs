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
        public AutoIncrementalValueProvider timer;
        public MainPage()
        {
            InitializeComponent();

            timer = new AutoIncrementalValueProvider();

            var binding = new Binding()
            {
                Source = timer,
                Path = "Counter",
            };

            timerLabel.SetBinding(Label.TextProperty, binding);
        }
    }
    public class AutoIncrementalValueProvider : INotifyPropertyChanged
    {
        private int counter;
        private int fastCounter;
        public int Counter {
            get => counter; 
            private set {
                counter = value;
                OnPropertyChanged("Counter");
            }
        }
        public int FastCounter
        {
            get => fastCounter;
            private set
            {
                fastCounter = value;
                OnPropertyChanged("Counter");
            }
        }
        public AutoIncrementalValueProvider()
        {
            Counter = 300;
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                Counter += 1;
                return true;
            });
            Device.StartTimer(TimeSpan.FromSeconds(300), () =>
            {
                Counter += 1;
                return true;
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            switch (Device.Idiom)
            {
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
    }
    public class MyTriggerAction : TriggerAction<Entry>
    {
        protected override void Invoke(Entry sender)
        {
            if (!Int32.TryParse(sender.Text, out var number))
            {
                sender.BackgroundColor = Color.IndianRed;
            }
            else {
                sender.BackgroundColor = Color.GreenYellow;
            }
        }
    }
    
}
