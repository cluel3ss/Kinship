using Kinship.models.responses;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Kinship.pages.Public
{
    public partial class DetailofEvents : ContentPage
    {
        Event eventObj;

        public DetailofEvents(Event eventObj)
        {
            InitializeComponent();
            this.eventObj = eventObj;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            EventNameLabel.Text = eventObj.event_name;
        }

        protected override bool OnBackButtonPressed()
        {
            this.Navigation.PopAsync();
            return base.OnBackButtonPressed();
        }
    }
}
