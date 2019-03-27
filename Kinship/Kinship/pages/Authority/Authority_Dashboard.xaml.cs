using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kinship.pages.Authority
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Authority_Dashboard : ContentPage
	{
		public Authority_Dashboard ()
		{
			InitializeComponent ();
		}

        private void Engagements_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Engagements());
        }
        private void Notification_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Notification());
        }
    }
}