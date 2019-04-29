using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kinship.pages.Authority
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Notification : ContentPage
	{
		public Notification ()
		{
			InitializeComponent ();
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.Black;
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
        }
	}
}