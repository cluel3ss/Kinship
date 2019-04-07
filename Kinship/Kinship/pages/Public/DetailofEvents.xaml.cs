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
            //EventNameLabel.Text = eventObj.event_name;
            EventLinkLabel.Text = eventObj.event_link;
            EventDescriptionLabel.Text = eventObj.event_description;
            EventStatusLabel.Text = eventObj.event_status;
            EventDateLabel.Text = eventObj.event_date;
            EventLocationLabel.Text = eventObj.event_location;
            Title = eventObj.event_name;
           
        }

        protected override bool OnBackButtonPressed()
        {
            this.Navigation.PopAsync();
            return base.OnBackButtonPressed();
        }
    }
}
