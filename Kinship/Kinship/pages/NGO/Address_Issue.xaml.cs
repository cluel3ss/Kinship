using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Kinship.models;

namespace Kinship.pages.NGO
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Address_Issue : ContentPage
	{
		public Address_Issue ()
		{
			InitializeComponent ();
		}


        private List<NGO_Events> ngo_events = new List<NGO_Events>() { };

       

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await ListofNGOEvents();

        }

        private async Task ListofNGOEvents()
        {
            ngo_events.Clear();

            ngo_events = new List<NGO_Events>()
            {
                new NGO_Events {
                    Name = "abc",
                    Num = "13124",
                    imgsource = "main.jpg"
                },
                new NGO_Events {
                    Name = "yo",
                    Num = "dfzsrgt",
                    imgsource = "main.jpg"
                },
                 new NGO_Events {
                    Name = "abc",
                    Num = "13124",
                    imgsource = "main.jpg"
                }
            };

            NGOEventsList.ItemsSource = ngo_events;
           

        }

    }
}



