using Kinship.internalData;
using Xamarin.Forms;
using Kinship.pages;
using System;
using Kinship.models.responses;
using Kinship.models.requests;
using Kinship.interfaces;
using Refit;
using System.Collections.ObjectModel;

namespace Kinship
{
    public partial class MainPage : ContentPage
    {
        IAPIService aPIService;

        public MainPage()
        {
            InitializeComponent();
            Entry_userEmail.Text = "kanojia24.10@gmail.com";
            Entry_userPassword.Text = "Hello";
            AccountTypeSelect.SelectedIndex = 0;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            aPIService = RestService.For<IAPIService>(Constants.mongoDBBaseUrl);
        }

        private void ButtonLoginFrame_Clicked(object sender, EventArgs e)
        {
            BoardLogin.IsVisible = true;
            BoardSignUp.IsVisible = false;
        }

        private void ButtonRegisterFrame_Clicked(object sender, EventArgs e)
        {
            BoardLogin.IsVisible = false;
            BoardSignUp.IsVisible = true;
        }

        private async void Button_Login_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Entry_userEmail.Text) || string.IsNullOrEmpty(Entry_userEmail.Text))
            {
                await DisplayAlert("Empty Fields", "Please Fill The Empty Fields", "Ok");
                return;
            }
            else
            {
                MyActivityIndicatorLogin.IsVisible = true;
                ObservableCollection<LogIn> logIn = new ObservableCollection<LogIn>() { };
                string query = @"{""user_email"": """ + Entry_userEmail.Text.Trim() + @"""}";
                logIn = await aPIService.LoginUser(Constants.mongoDBBName, Constants.mongoDBCollectionUsers, Constants.mongoDBKey, query);

                if (logIn.Count < 0)
                {
                    await DisplayAlert("Wrong Credentials", "Check your credentials again", "ok");
                    return;
                }
                else
                {
                    if (logIn[0].user_password == Entry_userPassword.Text.Trim())
                    {
                        if (logIn[0].account_status)
                        {
                            LoggedInUser.userID = logIn[0]._id._oid;
                            if (logIn[0].user_type == "PUBLIC")
                                LoggedInUser.userType = Constants.UserType.PUBLIC;
                            else if (logIn[0].user_type == "NGO")
                                LoggedInUser.userType = Constants.UserType.NGO;
                            else if (logIn[0].user_type == "AUTHORITY")
                                LoggedInUser.userType = Constants.UserType.AUTHORITY;
                            MyActivityIndicatorLogin.IsVisible = false;
                            await Navigation.PushAsync(new Dashboard());
                        }
                        else
                        {
                            await DisplayAlert("Inactive", "Your Account Is Not Activated Yet.", "ok");
                        }
                    }
                    MyActivityIndicatorLogin.IsVisible = false;
                }
                MyActivityIndicatorLogin.IsVisible = false;
            }
        }

        private async void Button_SignUp_Clicked(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(Entry_SignUp_userName.Text) || string.IsNullOrEmpty(Entry_SignUp_userEmail.Text))
            {
                await DisplayAlert("Empty Fields", "Please Fill The Empty Fields", "Ok");
                return;
            }
            else
            {
                MyActivityIndicator.IsVisible = true;
                SignUpResponse signUpResponse = new SignUpResponse();
                SignupRequest signUpRequest = new SignupRequest();
                signUpRequest.user_name = Entry_SignUp_userName.Text.Trim();
                signUpRequest.user_email = Entry_SignUp_userEmail.Text.Trim();
                signUpRequest.user_password = Entry_SignUp_userPassword.Text.Trim();
                if (AccountTypeSelect.SelectedIndex == 0)
                {
                    signUpRequest.user_type = "PUBLIC";
                    signUpRequest.account_status = true;
                }
                else if (AccountTypeSelect.SelectedIndex == 1)
                {
                    signUpRequest.user_type = "NGO";
                    signUpRequest.account_status = false;
                }
                else if (AccountTypeSelect.SelectedIndex == 2)
                {
                    signUpRequest.user_type = "AUTHORITY";
                    signUpRequest.account_status = false;
                }
                signUpResponse = await aPIService.SignUpUser(Constants.mongoDBBName, Constants.mongoDBCollectionUsers, Constants.mongoDBKey, signUpRequest);
                MyActivityIndicator.IsVisible = false;
                if (!string.IsNullOrEmpty(signUpResponse._id.oid))
                {
                    await DisplayAlert("Success", "Signed Up Successfully.", "Ok");
                    Entry_SignUp_userName.Text = null;
                    Entry_SignUp_userEmail.Text = null;
                    Entry_SignUp_userPassword.Text = null;
                }
                else
                    await DisplayAlert("Failed", "Try again later.", "Ok");
                return;
            }
           
        }
    }
}
