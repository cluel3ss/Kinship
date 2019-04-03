using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Kinship.models;
using System.Collections.ObjectModel;
using Kinship.interfaces;
using Kinship.models.responses;
using Refit;
using Kinship.internalData;

namespace Kinship.pages.NGO
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Address_Issue : ContentPage
	{
        private ObservableCollection<Issue> issues = new ObservableCollection<Issue>() { };
        IAPIService aPIService;


        public Address_Issue ()
		{
			InitializeComponent ();
            aPIService = RestService.For<IAPIService>(Constants.mongoDBBaseUrl);
        }


        //private List<NGO_Events> ngo_events = new List<NGO_Events>() { };

       

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await ListofNGOEvents();

        }

        private async Task ListofNGOEvents()
        {
            issues.Clear();
            issues = await aPIService.GetIssueList(Constants.mongoDBBName, Constants.mongoDBCollectionIssues, Constants.mongoDBKey);
            //ngo_events = new List<NGO_Events>()
            //{
            //    new NGO_Events {
            //        Name = "abc",
            //        Num = "13124",
            //        imgsource = "main.jpg"
            //    },
            //    new NGO_Events {
            //        Name = "yo",
            //        Num = "dfzsrgt",
            //        imgsource = "main.jpg"
            //    },
            //     new NGO_Events {
            //        Name = "abc",
            //        Num = "13124",
            //        imgsource = "main.jpg"
            //    }
            //};

            NGOEventsList.ItemsSource = issues;
           

        }

    }
}



