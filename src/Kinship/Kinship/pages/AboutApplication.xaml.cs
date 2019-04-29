using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Kinship.pages
{
    public partial class AboutApplication : ContentPage
    {
        public AboutApplication()
        {
            InitializeComponent();
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.Black;
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
        }
    }
}
