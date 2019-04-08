using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using Kinship.interfaces;
using Kinship.models.responses;
using Refit;
using Kinship.internalData;

namespace Kinship.pages.Public
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListofIssues : ContentPage
	{
        private ObservableCollection<Issue> issues = new ObservableCollection<Issue>() { };
        IAPIService aPIService;


        public ListofIssues()
		{
			InitializeComponent ();
            aPIService = RestService.For<IAPIService>(Constants.mongoDBBaseUrl);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await ListofNGOEvents();

        }

        protected override bool OnBackButtonPressed()
        {
            this.Navigation.PopAsync();
            return base.OnBackButtonPressed();
        }

        private async Task ListofNGOEvents()
        {
            MyActivityIndicator.IsVisible = true;
            issues.Clear();
            issues = await aPIService.GetIssueList(Constants.mongoDBBName, Constants.mongoDBCollectionIssues, Constants.mongoDBKey);
            NGOEventsList.ItemsSource = issues;
            MyActivityIndicator.IsVisible = false;
        }

        async void IssueSelectedAsync(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var selectedItem = e.SelectedItem as Issue;
            await Navigation.PushAsync(new DetailofIssues(selectedItem));
            NGOEventsList.SelectedItem = null;
        }

    }
}



