using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Kinship.models.responses;
using Kinship.interfaces;
using Refit;
using Kinship.internalData;
using System.Collections.ObjectModel;
using Kinship.MongoDBCache;
using System;

namespace Kinship.pages.Public
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListofEvents : ContentPage
	{
        private ObservableCollection<Event> events = new ObservableCollection<Event>() { };
        IAPIService aPIService;
        private int pageLoadTime = 0;

        public ListofEvents()
		{
			InitializeComponent ();
            aPIService = RestService.For<IAPIService>(Constants.mongoDBBaseUrl);
        }


        protected async override void OnAppearing()
        {
            base.OnAppearing();
            //await ListofEventsAsync();
            pageLoadTime++;
            eventsView.ItemsSource = MongoCache.GetEvents();
            if (pageLoadTime < 1)
                await ListofEventsAsync();

        }

        protected override bool OnBackButtonPressed()
        {
            this.Navigation.PopAsync();
            return base.OnBackButtonPressed();
        }

        private async Task ListofEventsAsync()
        {
            //MyActivityIndicator.IsVisible = true;
            events.Clear();
            events = await aPIService.GetEventList(Constants.mongoDBBName, Constants.mongoDBCollectionEvents, Constants.mongoDBKey);
            eventsView.ItemsSource = events;
            //MyActivityIndicator.IsVisible = false;
            try
            {
                MongoCache.WriteEvents(events);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Exception Happened : " + ex);
                //await DisplayAlert("Exception 1", ex.Message, "ok");
            }


        }

        async void EventSelectedAsync(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var selectedItem = e.SelectedItem as Event;
            await Navigation.PushAsync(new DetailofEvents(selectedItem));
            eventsView.SelectedItem = null;
        }
    }

}