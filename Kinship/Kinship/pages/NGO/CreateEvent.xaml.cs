﻿using Kinship.interfaces;
using Kinship.internalData;
using Kinship.models.requests;
using Kinship.models.responses;
using Refit;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kinship.pages.NGO
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CreateEvent : ContentPage
	{
        IAPIService aPIService;
        InsertEvent insertEvent;
        Event newEventResponse;
        string IssueID;

        public CreateEvent(string issueId = null)
		{
			InitializeComponent ();
            if (LoggedInUser.userType.Equals(Constants.UserType.PUBLIC) || LoggedInUser.userType.Equals(Constants.UserType.AUTHORITY))
                base.OnBackButtonPressed();
            this.IssueID = issueId;
            aPIService = RestService.For<IAPIService>(Constants.mongoDBBaseUrl);
            insertEvent = new InsertEvent();
            newEventResponse = new Event();

        }

        protected override bool OnBackButtonPressed()
        {
            this.Navigation.PopAsync();
            return base.OnBackButtonPressed();
        }

        private async void UploadEvent_Clicked(object sender, EventArgs e)
        {
            insertEvent.event_name = Event_Name.Text;
            insertEvent.event_description = Event_Description.Text;
            insertEvent.event_link = Event_Link.Text;
            insertEvent.event_cordinator_number = Event_Contact.Text.Split(',').ToList();
            insertEvent.event_date = Event_Date.ToString();
            insertEvent.event_location = Event_Place.Text;
            insertEvent.event_organizer = LoggedInUser.userID;
            insertEvent.event_status = "OPEN";
            if (string.IsNullOrEmpty(IssueID))
                insertEvent.event_issue_id = "";
            else
                insertEvent.event_issue_id = IssueID;
            newEventResponse = await aPIService.InsertNewEvent(Constants.mongoDBBName, Constants.mongoDBCollectionEvents, Constants.mongoDBKey, insertEvent);

            if (!string.IsNullOrEmpty(newEventResponse._id.oid))
            {
                await DisplayAlert("Success", "Successsfully Inserted The Record.", "ok");
                base.OnBackButtonPressed();
            }
            else
            {
                await DisplayAlert("Failure", "Failed To Insert The Record.", "ok");
            }
        }
    }
}