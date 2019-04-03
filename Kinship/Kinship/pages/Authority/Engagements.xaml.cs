using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Kinship.models;

namespace Kinship.pages.Authority
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Engagements : ContentPage
	{
		public Engagements ()
		{
			InitializeComponent ();
		}

        private List<Authority_EngagementList> authority_eng = new List<Authority_EngagementList>() { };



        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await ListofauthorityEngagement();

        }

        private async Task ListofauthorityEngagement()
        {
            authority_eng.Clear();

            authority_eng = new List<Authority_EngagementList>()
            {
                new Authority_EngagementList {
                    Name = "abc",
                    Num = "13124",
                    imgsource = "main.jpg"
                },
                new Authority_EngagementList {
                    Name = "yo",
                    Num = "dfzsrgt",
                    imgsource = "main.jpg"
                },
                 new Authority_EngagementList {
                    Name = "abc",
                    Num = "13124",
                    imgsource = "main.jpg"
                }
            };

            Authority_engagements.ItemsSource = authority_eng;


        }

    }
}



