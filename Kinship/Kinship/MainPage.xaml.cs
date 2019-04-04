using Kinship.interfaces;
using Kinship.internalData;
using Refit;
using Xamarin.Forms;
using Kinship.pages;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Kinship
{
    public partial class MainPage : ContentPage
    {
        //ILoginService loginServices;

        public MainPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
        }

        private void Login_Clicked(object sender, System.EventArgs e)
        {
            LoggedInUser.userType = Constants.UserType.NGO;
            Navigation.PushAsync(new Dashboard());

        }
    }
}
