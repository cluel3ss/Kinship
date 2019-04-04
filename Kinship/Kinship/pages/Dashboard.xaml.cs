using System;
using Kinship.internalData;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Kinship.pages.Public;
using Kinship.pages.NGO;
using Kinship.pages.Authority;

namespace Kinship.pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Dashboard : ContentPage
	{
		public Dashboard ()
		{
			InitializeComponent ();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (LoggedInUser.userType == Constants.UserType.PUBLIC)
            {
                publicDashboard.IsVisible = true;
                ngoDashboard.IsVisible = false;
                authorityDashboard.IsVisible = false;
            }
            else if (LoggedInUser.userType == Constants.UserType.NGO)
            {
                publicDashboard.IsVisible = false;
                ngoDashboard.IsVisible = true;
                authorityDashboard.IsVisible = false;
            }
            else if (LoggedInUser.userType == Constants.UserType.AUTHORITY)
            {
                publicDashboard.IsVisible = false;
                ngoDashboard.IsVisible = false;
                authorityDashboard.IsVisible = true;
            }
        }

        private async void RaiseNewIssueAsync(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RaiseNewIssue());
        }

        private async void ListofEventsAsync(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListofEvents());
        }

        private async void CreateEventAsync(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateEvent());
        }

        private async void ListofIssuesAsync(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListofIssues());
        }

        private async void EngagementsClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Engagements());
        }

        private async void NotificationClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Notification());
        }
    }
}