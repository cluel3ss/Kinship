using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Kinship.models;
using Kinship.models.responses;
using Kinship.interfaces;
using Refit;
using Kinship.internalData;
using System.Collections.ObjectModel;

namespace Kinship.pages.Public
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Public_Events : ContentPage
	{
        private ObservableCollection<Event> events = new ObservableCollection<Event>() { };
        IAPIService aPIService;

        public Public_Events ()
		{
			InitializeComponent ();
            aPIService = RestService.For<IAPIService>(Constants.mongoDBBaseUrl);
        }


        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await ListofEvents();
          
        } 

        private async Task ListofEvents()
        {
            events.Clear();
            events = await aPIService.GetEventList(Constants.mongoDBBName, Constants.mongoDBCollectionEvents, Constants.mongoDBKey);
            

            //events = new List<EventsList>()
            //{
            //    new EventsList {
                    
            //    },
            //    new EventsList {
            //        Name = "yo",
            //        Num = "dfzsrgt",
            //        imgsource = "main.jpg"
            //    }
            //};

            eventsView.ItemsSource = events;
    //        eventsView.ItemsSource = new List<EventsList>() {
    //        new Events() {
    //            Name = "Umair", Num = "0456445450945", imgsource = "http://bit.ly/2oDQpPT",
    //        },
    //        new Contacts() {
    //            Name = "Cat", Num = "034456445905", imgsource = "http://gtty.im/2psFEos",
    //        },
    //        new Contacts() {
    //            Name = "Nature", Num = "56445905", imgsource = "http://gtty.im/2psFEos",
    //        },
    //};

        }

    }

}