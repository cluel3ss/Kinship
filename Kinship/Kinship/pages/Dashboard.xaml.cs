using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Kinship.pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Dashboard : ContentPage
	{
		public Dashboard ()
		{
			InitializeComponent ();
		}

        private void Public_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Public.Public_Dashboard());
        }
        private void NGO_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NGO.NGO_Dashboard());
        }
        private void Authority_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Authority.Authority_Dashboard());
        }
    }
}