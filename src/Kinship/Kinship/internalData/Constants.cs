using System;
using System.Collections.Generic;
using System.Text;

namespace Kinship.internalData
{
    class Constants
    {
        public const string mongoDBBaseUrl = "https://api.mlab.com/api/1";
        public const string mongoDBKey = "FBaUn9vVItIk09l8TvtCRWVtCravIGbC";
        public const string mongoDBBName = "kinship_db";
        public const string mongoDBCollectionEvents = "events";
        public const string mongoDBCollectionIssues = "issues";
        public const string mongoDBCollectionUsers = "users";
        public const string mongoDBofflineEvents = "offlineEvents";
        public const string mongoDBofflineIssues = "offlineIssues";
        public enum UserType
        {
            PUBLIC,
            NGO,
            AUTHORITY
        }
        public enum JsonResponseType
        {
            EVENTS,
            ISSUES,
            USERS
        }
    }
}
