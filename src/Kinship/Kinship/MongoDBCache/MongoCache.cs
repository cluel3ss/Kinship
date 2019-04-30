using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Kinship.internalData;
using Kinship.models;
using Kinship.models.requests;
using Kinship.models.responses;
using MongoDB.Driver;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Kinship.MongoDBCache
{
    public class MongoCache
    {
        public const string ISSUE_TABLE = "issues";
        public const string EVENT_TABLE = "events";

        public static void WriteIssues(ObservableCollection<Issue> json)
        {
            Application.Current.Properties[Constants.mongoDBCollectionIssues] = json;
        }

        public static ObservableCollection<Issue> GetIssues()
        {
            return (ObservableCollection<Issue>) Application.Current.Properties[Constants.mongoDBCollectionIssues];
        }

        public static void WriteEvents(ObservableCollection<Event> json)
        {
            Application.Current.Properties[Constants.mongoDBCollectionEvents] = json;
        }

        public static ObservableCollection<Event> GetEvents()
        {
            return (ObservableCollection<Event>)Application.Current.Properties[Constants.mongoDBCollectionEvents];
        }

        public static void WriteOfflineIssue(InsertIssues issueJson)
        {
            if (issueJson == null)
                return;
            List<InsertIssues> eventList = new List<InsertIssues> { };
            if (Application.Current.Properties.ContainsKey(Constants.mongoDBofflineIssues))
                eventList = (List<InsertIssues>)Application.Current.Properties[Constants.mongoDBofflineIssues];
            eventList.Add(issueJson);
            Application.Current.Properties[Constants.mongoDBofflineIssues] = null;
            Application.Current.Properties[Constants.mongoDBofflineIssues] = JsonConvert.SerializeObject(issueJson);
            Application.Current.SavePropertiesAsync();
        }

        public static List<InsertIssues> ReadOfflineIssue()
        {
            if (Application.Current.Properties.ContainsKey(Constants.mongoDBofflineIssues))
                return JsonConvert.DeserializeObject<List<InsertIssues>>(Application.Current.Properties[Constants.mongoDBofflineIssues].ToString());
            else
                return (List<InsertIssues>)null;
        }

        public static void WriteOfflineEvent(InsertEvent eventJson)
        {
            if (eventJson == null)
                return;
            List<InsertEvent> eventList = new List<InsertEvent> { };
            if (Application.Current.Properties.ContainsKey(Constants.mongoDBofflineEvents))
                eventList = (List<InsertEvent>)Application.Current.Properties[Constants.mongoDBofflineEvents];
            eventList.Add(eventJson);
            Application.Current.Properties[Constants.mongoDBofflineEvents] = null;
            Application.Current.Properties[Constants.mongoDBofflineEvents] = JsonConvert.SerializeObject(eventList);
            Application.Current.SavePropertiesAsync();
        }

        public static List<InsertEvent> ReadOfflineEvent()
        {
            if (Application.Current.Properties.ContainsKey(Constants.mongoDBofflineEvents))
                return JsonConvert.DeserializeObject<List<InsertEvent>>(Application.Current.Properties[Constants.mongoDBofflineEvents].ToString());
            else
                return (List<InsertEvent>)null;
        }

        public static void WriteUserLogin(LogIn logInData)
        {
            if (logInData == null)
                return;
            AppSetting appSetting = new AppSetting();
            appSetting.isSignedIn = true;
            appSetting.creationDate = DateTime.Now;
            appSetting.loginData = logInData;

            if (Application.Current.Properties.ContainsKey(Constants.mongoDBCollectionUsers))
                Application.Current.Properties[Constants.mongoDBCollectionUsers] = null;

            Application.Current.Properties[Constants.mongoDBCollectionUsers] = JsonConvert.SerializeObject(appSetting);
            Application.Current.SavePropertiesAsync();
        }

        public static AppSetting ReadUserLogin()
        {
            if (Application.Current.Properties.ContainsKey(Constants.mongoDBCollectionUsers))
                return JsonConvert.DeserializeObject<AppSetting>(Application.Current.Properties[Constants.mongoDBCollectionUsers].ToString());
            else
                return null;
        }

        public static bool Logout()
        {
            try
            {
                Application.Current.Properties.Clear();
                Application.Current.SavePropertiesAsync();
                AppSetting appSetting = new AppSetting();
                appSetting.isSignedIn = false;
                appSetting = null;
                return true;
            }
            catch (Exception logoutException)
            {
                Console.WriteLine("Exception In Logout : " + logoutException);
                return false;
            }
        }

    }
}
