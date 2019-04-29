using Kinship.interfaces;
using Kinship.internalData;
using Kinship.models.requests;
using Kinship.models.responses;
using Kinship.MongoDBCache;
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
            if (string.IsNullOrEmpty(Event_Name.Text) || 
                string.IsNullOrEmpty(Event_Description.Text) || 
                string.IsNullOrEmpty(Event_Link.Text) ||
                string.IsNullOrEmpty(Event_Contact.Text) ||
                string.IsNullOrEmpty(Event_Date.ToString()) ||
                string.IsNullOrEmpty(Event_Place.Text))
            {
                await DisplayAlert("Empty Field", "Please Fill The Fields", "Ok");
                return;
            }
            MyActivityIndicator.IsVisible = true;
            insertEvent.event_name = Event_Name.Text;
            insertEvent.event_description = Event_Description.Text;
            insertEvent.event_link = Event_Link.Text;
            insertEvent.event_cordinator_number = Event_Contact.Text.Split(',').ToList();
            insertEvent.event_date = Event_Date.Date.ToString("MM d, yyyy");
            insertEvent.event_location = Event_Place.Text;
            insertEvent.event_organizer = LoggedInUser.userID;
            insertEvent.event_status = "OPEN";
            if (string.IsNullOrEmpty(IssueID))
                insertEvent.event_issue_id = "";
            else
                insertEvent.event_issue_id = IssueID;

            try
            {
                this.newEventResponse = await aPIService.InsertNewEvent(Constants.mongoDBBName, Constants.mongoDBCollectionEvents, Constants.mongoDBKey, insertEvent);
                MyActivityIndicator.IsVisible = false;
                if (!string.IsNullOrEmpty(this.newEventResponse._id.oid))
                {
                    await DisplayAlert("Success", "Successsfully Inserted The Record.", "ok");
                    await this.Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Failure", "Failed To Insert The Record.", "ok");
                }
            }
            catch (Exception apiException)
            {
                this.newEventResponse = null;
                MongoCache.WriteOfflineEvent(insertEvent);
                await DisplayAlert("Failure", "Failed To Insert The Record. Will Try again when the internet is back.", "ok");
                await this.Navigation.PopAsync();
            }


        }
    }
}