using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kinship.pages.NGO
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NGO_Dashboard : ContentPage
	{
		public NGO_Dashboard ()
		{
			InitializeComponent ();
		}

        private void Create_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Create_Event());
        }
        private void Ongoing_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Address_Issue());
        }
    }
}