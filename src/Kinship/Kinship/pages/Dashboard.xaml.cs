using System;
using Kinship.internalData;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Kinship.pages.Public;
using Kinship.pages.NGO;
using Kinship.pages.Authority;
using System.Collections.ObjectModel;
using Xamd.ImageCarousel.Forms.Plugin;
using Kinship.MongoDBCache;
using System.Linq;

namespace Kinship.pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Dashboard : ContentPage
	{
        ObservableCollection<FileImageSource> imageSources = new ObservableCollection<FileImageSource>();


        public Dashboard ()
		{
			InitializeComponent ();
            
            imageSources.Add("bg02.jpg");
            imageSources.Add("bg1.jpeg");
            imageSources.Add("bg2.jpg");
            imageSources.Add("bg3.jpg");

            imgSlider.Images = imageSources;
            imgSlider1.Images = imageSources;
            //imgSlider2.Images = imageSources;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (LoggedInUser.userType == Constants.UserType.PUBLIC)
            {
                publicDashboard.IsVisible = true;
                ngoDashboard.IsVisible = false;
                //authorityDashboard.IsVisible = false;
            }
            else if (LoggedInUser.userType == Constants.UserType.NGO || LoggedInUser.userType == Constants.UserType.AUTHORITY)
            {
                publicDashboard.IsVisible = false;
                ngoDashboard.IsVisible = true;
                //authorityDashboard.IsVisible = false;
            }
            //else if (LoggedInUser.userType == Constants.UserType.AUTHORITY)
            //{
            //    publicDashboard.IsVisible = false;
            //    ngoDashboard.IsVisible = false;
            //    authorityDashboard.IsVisible = true;
            //}
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

        private async void About_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new pages.AboutApplication());
        }

        private async void Logout_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new pages.AboutApplication());
            bool isLoggedOut = MongoCache.Logout();
            if (isLoggedOut)
            {
                await Navigation.PushAsync(new MainPage());
            }
            else
            {
                await DisplayAlert("Failed", "Failed To Log Out", "Ok");
            }
        }
    }
}