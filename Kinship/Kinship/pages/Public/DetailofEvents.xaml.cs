using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Kinship.pages.Public
{
    public partial class DetailofEvents : ContentPage
    {
        string EventID;

        public DetailofEvents(string EventID)
        {
            InitializeComponent();
            this.EventID = EventID;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            DisplayAlert("ID", EventID, "OK");
        }

        protected override bool OnBackButtonPressed()
        {
            this.Navigation.PopAsync();
            return base.OnBackButtonPressed();
        }
    }
}
