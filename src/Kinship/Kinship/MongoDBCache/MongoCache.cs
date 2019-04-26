using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Kinship.internalData;
using Kinship.models.responses;
using MongoDB.Driver;
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
    }
}
