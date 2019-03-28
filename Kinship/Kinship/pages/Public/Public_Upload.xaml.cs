using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kinship.pages.Public
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Public_Upload : ContentPage
	{
		public Public_Upload ()
		{
			InitializeComponent ();
		}

        private void UploadIssue_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Success!", "Your issue has been submitted", "OK");
        }
    }
}