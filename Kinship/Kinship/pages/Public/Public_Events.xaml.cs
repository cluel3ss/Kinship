using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Kinship.models;

namespace Kinship.pages.Public
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Public_Events : ContentPage
	{
        private List<EventsList> events = new List<EventsList>() { };

        public Public_Events ()
		{
			InitializeComponent ();
		}


        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await ListofEvents();
          
        } 

        private async Task ListofEvents()
        {
            events.Clear();

            events = new List<EventsList>()
            {
                new EventsList {
                    Name = "abc",
                    Num = "13124",
                    imgsource = "https://bit.ly/2FItnD6"
                },
                new EventsList {
                    Name = "yo",
                    Num = "dfzsrgt",
                    imgsource = "https://bit.ly/2FItnD6"
                }
            };

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