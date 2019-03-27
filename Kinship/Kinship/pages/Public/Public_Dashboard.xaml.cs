using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Kinship.pages;

namespace Kinship.pages.Public
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Public_Dashboard : ContentPage
	{
		public Public_Dashboard ()
		{
			InitializeComponent ();
		}

        private void Upload_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Public_Upload());
        }
        private void Events_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Public_Events());
        }
    }
}