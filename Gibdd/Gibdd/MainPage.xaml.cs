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

            Title = "MainPage";

            timer = new AutoIncrementalValueProvider();

            var binding = new Binding()
            {
                Source = timer,
                Path = "Counter",
            };

            timerLabel.SetBinding(Label.TextProperty, binding);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Page1());
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ListViewPage1());
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TabbedPage1("super title", (result) => Title = result));
        }
    }
    public class AutoIncrementalValueProvider : INotifyPropertyChanged
    {
        private int counter;
        private int fastCounter;
        public int Counter
        {
            get => counter;
            set
            {
                counter = value;
                OnPropertyChanged("Counter");
            }
        }
        public int FastCounter
        {
            get => fastCounter;
            set
            {
                fastCounter = value;
                OnPropertyChanged("FastCounter");
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
            Device.StartTimer(TimeSpan.FromMilliseconds(300), () =>
            {
                FastCounter += 1;
                return true;
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
