using System;
using System.Collections.Generic;
using System.IO;
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
        string stream = "";

        public DetailofIssues(Issue issue)
        {
            InitializeComponent();
            numberOfTimePageLoaded++;
            this.issue = issue;
            aPIService = RestService.For<IAPIService>(Constants.mongoDBBaseUrl);
            issueImage.Source =  ImageSource.FromStream(() => { return CommonFunctionalities.Base64ToImage(issue.photo); });
            ratingLabel.Text = "Rating : " + issue.rating;
            statusLabel.Text = "Status : " + issue.status;
            addressLabel.Text = issue.address;
            if (string.IsNullOrEmpty(issue.additional_comments))
                additionalCommentsLabel.Text = " NONE";
            else
                additionalCommentsLabel.Text =  issue.additional_comments;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //AddressEntry.Text = issue.address;
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
                    if (issue.status_changed_by == LoggedInUser.userID || string.IsNullOrEmpty(issue.status_changed_by))
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
            else if (issue.status.ToUpper().Trim() == "FIXED")
            {
                statusPicker.SelectedIndex = 2;// Setting status in Picker

                // If it is Fixed, no need to edit anything.
                statusPicker.IsEnabled = false;
                statusPicker.IsVisible = false;
            }
        }

        async void SubmitAsync(object sender, System.EventArgs e)
        {
            if (statusPicker.SelectedIndex == 1)
            {
                if (string.IsNullOrEmpty(AdditionalCommentsProof.Text))
                {
                    await DisplayAlert("Empty Fields", "Estimated Number of days is required for this action.", "ok");
                    return;
                }
            }
            if (statusPicker.SelectedIndex == 2)
            {
                if (string.IsNullOrEmpty(stream))
                {
                    await DisplayAlert("Empty Fields", "A valid Photo proof is required for this action.", "ok");
                    return;
                }
            }
            UpdateIssues updateIssues = new UpdateIssues();
            Set set = new Set();
            UpdateIssuesResponse updateIssuesResponse = new UpdateIssuesResponse();
            set.photo = issue.photo;
            set.photo_proof = stream;
            set.address = issue.address;
            set.rating = issue.rating;
            if (string.IsNullOrEmpty(AdditionalCommentsProof.Text))
                set.additional_comments_ngo = issue.additional_comments;
            else
                set.additional_comments_ngo = AdditionalCommentsProof.Text;
            set.adder = issue.adder;
            set.event_id = issue.event_id;
            set.status = statusPicker.SelectedItem.ToString();
            set.status_changed_by = LoggedInUser.userID;
            //Console.WriteLine("Days Required : " + DaysRequired.Text.Trim());
            try
            {
                set.days_required = DaysRequired.Text.Trim();
            }
            catch (Exception ex)
            {
                set.days_required = "";
            }
            updateIssues.set = set;
            string query = @"{""_id"": {""$oid"": """ + issue._id.oid + @"""}}";
            updateIssuesResponse = await aPIService.UpdateIssue(Constants.mongoDBBName, Constants.mongoDBCollectionIssues, Constants.mongoDBKey, query, updateIssues);
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
                CameraButton.IsVisible = false;
                ProofPhotoImage.IsVisible = false;

                additionalCommentsLabelProof.IsVisible = false;
                AdditionalCommentsProof.IsVisible = false;
                if (numberOfTimePageLoaded < 1)
                    await DisplayAlert("Not Valid", "This Action Is Not Valid", "ok");
                return;
            }
            else if (statusPicker.SelectedIndex.Equals(1))
            {
                if (LoggedInUser.userType.Equals(Constants.UserType.NGO) || LoggedInUser.userType.Equals(Constants.UserType.AUTHORITY))
                {
                    submitButton.IsVisible = true;
                    daysRequiredLabel.IsVisible = true;
                    DaysRequired.IsVisible = true;

                    uploadProofLabel.IsVisible = false;
                    ProofPhotoImage.IsVisible = false;
                    CameraButton.IsVisible = false;

                    additionalCommentsLabelProof.IsVisible = false;
                    AdditionalCommentsProof.IsVisible = false;
                }

            }
            else if (statusPicker.SelectedIndex.Equals(2))
            {
                if (LoggedInUser.userType.Equals(Constants.UserType.NGO) || LoggedInUser.userType.Equals(Constants.UserType.AUTHORITY))
                {
                    submitButton.IsVisible = true;
                    daysRequiredLabel.IsVisible = false;
                    DaysRequired.IsVisible = false;

                    uploadProofLabel.IsVisible = true;
                    ProofPhotoImage.IsVisible = true;
                    CameraButton.IsVisible = true;

                    additionalCommentsLabelProof.IsVisible = true;
                    AdditionalCommentsProof.IsVisible = true;
                }
            }
        }

        protected override bool OnBackButtonPressed()
        {
            this.Navigation.PopAsync();
            return base.OnBackButtonPressed();
        }

        async void CameraButtonClicked(object sender, System.EventArgs e)
        {
            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions()
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Custom,
                CustomPhotoSize = 5
            });

            if (photo != null)
            {
                //PhotoImage.Source = ImageSource.FromStream(() => { return photo.GetStream(); });
                stream = CommonFunctionalities.ImageToBase64(photo.GetStream());
                ProofPhotoImage.Source = ImageSource.FromStream(() => { return CommonFunctionalities.Base64ToImage(stream); });
            }
        }
    }
}
