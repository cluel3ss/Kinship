using System;
using System.Collections.Generic;
using Kinship.interfaces;
using Kinship.internalData;
using Kinship.models.requests;
using Kinship.models.responses;
using Kinship.pages.NGO;
using Refit;
using Xamarin.Forms;

namespace Kinship.pages.Public
{
    public partial class DetailofIssues : ContentPage
    {
        Issue issue;
        IAPIService aPIService;
        int numberOfTimePageLoaded = 0;

        public DetailofIssues(Issue issue)
        {
            InitializeComponent();
            numberOfTimePageLoaded++;
            this.issue = issue;
            aPIService = RestService.For<IAPIService>(Constants.mongoDBBaseUrl);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (issue.status.ToUpper().Trim() == "OPEN")
            {
                statusPicker.SelectedIndex = 0;// Setting status in Picker

                // If logged in user is NGO or AUTHORITY, they can change the status.
                if (LoggedInUser.userType.Equals(Constants.UserType.NGO) || LoggedInUser.userType.Equals(Constants.UserType.AUTHORITY))
                {
                    statusPicker.IsEnabled = true;
                    statusPicker.IsVisible = true;
                    if (LoggedInUser.userType.Equals(Constants.UserType.NGO))
                        createEventButton.IsVisible = true;
                    else
                        createEventButton.IsVisible = false;

                }
                else
                {
                    statusPicker.IsEnabled = false;
                    statusPicker.IsVisible = false;
                }
            }
            else if (issue.status.ToUpper().Trim() == "ONGOING")
            {
                statusPicker.SelectedIndex = 1; // Setting status in Picker

                if (LoggedInUser.userType.Equals(Constants.UserType.NGO) || LoggedInUser.userType.Equals(Constants.UserType.AUTHORITY))
                {
                    // If this is the person who changed the issue status, then they can update the entry status.
                    if (issue.status_changed_by == LoggedInUser.userID)
                    {
                        statusPicker.IsEnabled = true;
                        statusPicker.IsVisible = true;
                        submitButton.IsVisible = true;
                        daysRequiredLabel.IsVisible = true;
                        DaysRequired.IsVisible = true;
                    }
                    else
                    {
                        statusPicker.IsEnabled = false;
                        statusPicker.IsVisible = false;
                    }

                }
                else
                {
                    statusPicker.IsEnabled = false;
                    statusPicker.IsVisible = false;
                }
            }
            if (issue.status.ToUpper().Trim() == "FIXED")
            {
                statusPicker.SelectedIndex = 2;// Setting status in Picker

                // If it is Fixed, no need to edit anything.
                statusPicker.IsEnabled = false;
                statusPicker.IsVisible = false;
            }
        }

        async void SubmitAsync(object sender, System.EventArgs e)
        {
            UpdateIssues updateIssues = new UpdateIssues();
            Set set = new Set();
            UpdateIssuesResponse updateIssuesResponse = new UpdateIssuesResponse();
            set.photo = issue.photo;
            set.address = issue.address;
            set.rating = issue.rating;
            if (string.IsNullOrEmpty(AdditionalComments.Text))
                set.additional_comments = issue.additional_comments;
            else
                set.additional_comments = AdditionalComments.Text;
            set.adder = issue.adder;
            set.event_id = issue.event_id;
            set.status = statusPicker.SelectedItem.ToString();
            set.status_changed_by = LoggedInUser.userID;
            set.days_required = DaysRequired.Text.Trim();
            updateIssues.set = set;
            string query = @"{""_id"": {""$oid"": """ + issue._id.oid + @"""}}";
            updateIssuesResponse = await aPIService.UpdateIssue(Constants.mongoDBBName, Constants.mongoDBCollectionIssues, Constants.mongoDBKey,"", updateIssues);
            if (updateIssuesResponse.n >= 1)
            {
                await DisplayAlert("Success", "Status Changed Successfully.", "ok");
                base.OnAppearing();
                return;
            }
            else
            {
                await DisplayAlert("Failed", "Failed To Change The Status.", "ok");
                base.OnAppearing();
                return;
            }
        }

        async void CreateEventAsync(object sender, System.EventArgs e)
        {
            await this.Navigation.PushAsync(new CreateEvent(issue._id.oid));
        }

        async void StatusChangedAsync(object sender, System.EventArgs e)
        {
            if (statusPicker.SelectedIndex.Equals(0))
            {
                submitButton.IsVisible = false;
                daysRequiredLabel.IsVisible = false;
                DaysRequired.IsVisible = false;

                uploadProofLabel.IsVisible = false;
                UploadProof.IsVisible = false;

                additionalCommentsLabel.IsVisible = false;
                AdditionalComments.IsVisible = false;
                if (numberOfTimePageLoaded < 1)
                    await DisplayAlert("Not Valid", "This Action Is Not Valid", "ok");
                return;
            }
            else if (statusPicker.SelectedIndex.Equals(1))
            {
                if (numberOfTimePageLoaded > 1)
                {
                    submitButton.IsVisible = true;
                    daysRequiredLabel.IsVisible = true;
                    DaysRequired.IsVisible = true;

                    uploadProofLabel.IsVisible = false;
                    UploadProof.IsVisible = false;

                    additionalCommentsLabel.IsVisible = false;
                    AdditionalComments.IsVisible = false;
                }

            }
            else if (statusPicker.SelectedIndex.Equals(2))
            {
                submitButton.IsVisible = true;
                daysRequiredLabel.IsVisible = false;
                DaysRequired.IsVisible = false;

                uploadProofLabel.IsVisible = true;
                UploadProof.IsVisible = true;

                additionalCommentsLabel.IsVisible = true;
                AdditionalComments.IsVisible = true;
            }
        }

        protected override bool OnBackButtonPressed()
        {
            this.Navigation.PopAsync();
            return base.OnBackButtonPressed();
        }
    }
}
