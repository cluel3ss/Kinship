using Kinship.interfaces;
using Kinship.internalData;
using Refit;
using Xamarin.Forms;

namespace Kinship
{
    public partial class MainPage : ContentPage
    {
        ILoginService loginServices;

        public MainPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            loginServices = RestService.For<ILoginService>(CommonFunctionalities.baseAuthenticationUrl);
            string tempResponse = await loginServices.LoginUser();
        }
    }
}
