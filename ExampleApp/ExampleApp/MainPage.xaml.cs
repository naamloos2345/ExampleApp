using System;
using Xamarin.Forms;

namespace ExampleApp
{
    public partial class MainPage : ContentPage
    {
        string translatedNumber;

        public MainPage()
        {
            InitializeComponent();
        }

        void OnTranslate(object sender, EventArgs e)
        {
            translatedNumber = PhonewordTranslator.ToNumber(phoneNumberText.Text);
            if (!string.IsNullOrWhiteSpace(translatedNumber))
            {
                callButton.IsEnabled = true;
                callButton.Text = "Call " + translatedNumber;
            }
            else
            {
                callButton.IsEnabled = false;
                callButton.Text = "Call";
            }
        }

        async void OnCall(object sender, EventArgs e)
        {
            if (await this.DisplayAlert(
                    "Dial a Number",
                    "Would you like to call " + translatedNumber + "?",
                    "Yes",
                    "No"))
            {
                var dialer = DependencyService.Get<IDialer>();
                if (dialer != null)
                {
                    callHistoryButton.IsEnabled = true;
                    App.PhoneNumbers.Add(translatedNumber);
                    dialer.Dial(translatedNumber);
                }
            }
        }

        async void OnCallHistory(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CallHistoryPage());
        }
    }
}