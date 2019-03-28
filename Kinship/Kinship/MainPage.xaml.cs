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
        ILoginService loginServices;

        public MainPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            //loginServices = RestService.For<ILoginService>(CommonFunctionalities.baseAuthenticationUrl);
            //string tempResponse = await loginServices.LoginUser();

            //MongoClient mongoClient = new MongoClient("mongodb://Xonshiz:GuessMe24@kinshipcluster-0x3r5.mongodb.net/test?retryWrites=true");
            //await DisplayAlert("Hey", mongoClient.ListDatabaseNamesAsync().ToJson(), "ok");
        }

        private void Login_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new Dashboard());

        }
    }
}
