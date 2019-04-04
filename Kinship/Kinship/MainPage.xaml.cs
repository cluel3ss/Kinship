using Kinship.internalData;
using Xamarin.Forms;
using Kinship.pages;

namespace Kinship
{
    public partial class MainPage : ContentPage
    {
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
