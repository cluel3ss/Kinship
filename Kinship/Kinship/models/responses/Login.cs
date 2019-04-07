using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kinship.models.responses
{
    public class LoginId
    {
        [JsonProperty(PropertyName = "$oid")]
        public string _oid { get; set; }
    }

    public class LogIn
    {
        public LoginId _id { get; set; }
        public string user_name { get; set; }
        public string user_email { get; set; }
        public string user_password { get; set; }
        public string user_type { get; set; }
        public bool account_status { get; set; }
    }
}
