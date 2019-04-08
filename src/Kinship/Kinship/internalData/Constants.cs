using System;
using System.Collections.Generic;
using System.Text;

namespace Kinship.internalData
{
    class Constants
    {
        public const string mongoDBBaseUrl = "https://api.mlab.com/api/1";
        public const string mongoDBKey = "";
        public const string mongoDBBName = "kinship_db";
        public const string mongoDBCollectionEvents = "events";
        public const string mongoDBCollectionIssues = "issues";
        public const string mongoDBCollectionUsers = "users";
        public enum UserType
        {
            PUBLIC,
            NGO,
            AUTHORITY
        }
    }
}
