using System;
using System.Collections.Generic;
using Kinship.interfaces;
using Kinship.internalData;
using Kinship.models;
using Kinship.models.requests;
using Kinship.models.responses;
using Kinship.MongoDBCache;
using Kinship.pages;
using Refit;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Kinship
{
    public partial class App : Application
    {
        IAPIService aPIService;
        List<InsertIssues> issuesToBeUploaded = new List<InsertIssues> { };
        List<InsertEvent> eventsToBeUploaded = new List<InsertEvent> { };
        NewRecordResponse newRecordResponse;
        Event newEventResponse;
        public App()
        {
            InitializeComponent();

            AppSetting appSetting = new AppSetting();
            appSetting = MongoCache.ReadUserLogin();

            // We need the user to sign in again after every 3 days.
            if (DateTime.Now.Subtract(appSetting.creationDate).TotalDays > 3)
            {
                appSetting.isSignedIn = false;
                Application.Current.Properties[Constants.mongoDBCollectionUsers] = null;
            }

            if (appSetting == null || !appSetting.isSignedIn)
            {
                MainPage = new NavigationPage(new MainPage())
                {
                    BarBackgroundColor = Color.Black,
                    BarTextColor = Color.White
                };
            }
            else if (appSetting.isSignedIn)
            {
                MainPage = new NavigationPage(new Dashboard())
                {
                    BarBackgroundColor = Color.Black,
                    BarTextColor = Color.White
                };
            }

        }

        protected override async void OnStart()
        {
            // Handle when your app starts
            issuesToBeUploaded = MongoCache.ReadOfflineIssue();
            eventsToBeUploaded = MongoCache.ReadOfflineEvent();

            if (issuesToBeUploaded != null)
            {
                newRecordResponse = new NewRecordResponse();
                aPIService = RestService.For<IAPIService>(Constants.mongoDBBaseUrl);
                int currentIssue = 0;

                if (issuesToBeUploaded.Count > 0)
                {
                    try
                    {
                        foreach (var issue in issuesToBeUploaded)
                        {
                            newRecordResponse = await aPIService.InsertNewIssue(Constants.mongoDBBName, Constants.mongoDBCollectionIssues, Constants.mongoDBKey, issue);
                            if (!string.IsNullOrEmpty(newRecordResponse._id.oid))
                            {
                                // This is Success.
                                /*
                                 * First, we get the issue list from application settings and then we iterate over each and every saved instance.
                                 * Now, when we have success, we remove that item, so that it doesn't appear again in the list.
                                 * Now, we need to increment the issue count.                            
                                 */
                                issuesToBeUploaded.RemoveAt(currentIssue);
                                currentIssue++;
                            }
                            else
                            {
                                // This is Failure.
                                Console.WriteLine("Failure Occurred. Moving To Next One.");
                            }
                        }
                        foreach (var item in issuesToBeUploaded)
                            MongoCache.WriteOfflineIssue(item);
                    }
                    catch (Exception ex)
                    {
                        // Move On.
                        Console.WriteLine("Error While Uploading Issue Backlog : " + ex.Message);
                    }
                }
            } // Issue Uploaded

            if (eventsToBeUploaded != null)
            {
                newEventResponse = new Event();
                aPIService = RestService.For<IAPIService>(Constants.mongoDBBaseUrl);
                int currentEvent = 0;

                if (eventsToBeUploaded.Count > 0)
                {
                    try
                    {
                        foreach (var eventVal in eventsToBeUploaded)
                        {

                            newEventResponse = await aPIService.InsertNewEvent(Constants.mongoDBBName, Constants.mongoDBCollectionEvents, Constants.mongoDBKey, eventVal);
                            if (!string.IsNullOrEmpty(newEventResponse._id.oid))
                            {
                                // This is Success.
                                /*
                                 * First, we get the event list from application settings and then we iterate over each and every saved instance.
                                 * Now, when we have success, we remove that item, so that it doesn't appear again in the list.
                                 * Now, we need to increment the issue count.                            
                                 */
                                eventsToBeUploaded.RemoveAt(currentEvent);
                                currentEvent++;
                            }
                            else
                            {
                                // This is Failure.
                                Console.WriteLine("Failure Occurred. Moving To Next One.");
                            }
                        }

                        foreach (var item in eventsToBeUploaded)
                            MongoCache.WriteOfflineEvent(item);
                    }
                    catch (Exception ex)
                    {
                        // Move On.
                        Console.WriteLine("Error While Uploading Event Backlog : " + ex.Message);
                    }
                }
            } // Event Uploaded
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
