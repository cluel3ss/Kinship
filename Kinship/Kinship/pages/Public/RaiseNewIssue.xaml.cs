﻿using Kinship.interfaces;
using Kinship.internalData;
using Kinship.models.requests;
using Kinship.models.responses;
using Refit;
using System;
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
            insertIssues.photo = CommonFunctionalities.ImageToBase64();
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

    }
}