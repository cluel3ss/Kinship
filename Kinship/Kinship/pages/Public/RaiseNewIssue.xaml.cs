using Kinship.interfaces;
using Kinship.internalData;
using Kinship.models.requests;
using Kinship.models.responses;
using Refit;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kinship.pages.Public
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RaiseNewIssue : ContentPage
	{
        IAPIService aPIService;
        InsertIssues insertIssues;
        NewRecordResponse newRecordResponse;
        string stream;

        public RaiseNewIssue()
		{
			InitializeComponent ();
            if (LoggedInUser.userType.Equals(Constants.UserType.NGO) || LoggedInUser.userType.Equals(Constants.UserType.AUTHORITY))
                base.OnBackButtonPressed();
            aPIService = RestService.For<IAPIService>(Constants.mongoDBBaseUrl);
            insertIssues = new InsertIssues();
            newRecordResponse = new NewRecordResponse();
        }

        protected override bool OnBackButtonPressed()
        {
            this.Navigation.PopAsync();
            return base.OnBackButtonPressed();
        }

        private async void UploadIssue_Clicked(object sender, EventArgs e)
        {
            insertIssues.adder = LoggedInUser.userID;
            insertIssues.additional_comments = Comments.Text;
            insertIssues.address = area_issue.Text;
            insertIssues.event_id = "NONE";
            insertIssues.photo = stream;
            insertIssues.rating = rating.SelectedItem.ToString();
            insertIssues.status = "OPEN";
            insertIssues.status_changed_by = "NONE";

            newRecordResponse = await aPIService.InsertNewIssue(Constants.mongoDBBName, Constants.mongoDBCollectionIssues, Constants.mongoDBKey, insertIssues);

            if (!string.IsNullOrEmpty(newRecordResponse._id.oid))
            {
                await DisplayAlert("Success", "Successsfully Inserted The Record.", "ok");
                base.OnBackButtonPressed();
            }
            else
            {
                await DisplayAlert("Failure", "Failed To Insert The Record.", "ok");
            }
        }

        async void CameraButtonClicked(object sender, System.EventArgs e)
        {
            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Custom,
            CustomPhotoSize = 40});

            if (photo != null)
            {
                //PhotoImage.Source = ImageSource.FromStream(() => { return photo.GetStream(); });
                stream = CommonFunctionalities.ImageToBase64(photo.GetStream());
                PhotoImage.Source = ImageSource.FromStream(() => { return CommonFunctionalities.Base64ToImage(stream); });
            }
        }
    }
}