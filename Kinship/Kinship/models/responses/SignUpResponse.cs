using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kinship.models.responses
{
    public class SignUpId
    {
        [JsonProperty(PropertyName = "$oid")]
        public string oid { get; set; }
    }

    public class SignUpResponse
    {
        public SignUpId _id { get; set; }
        public string user_name { get; set; }
        public string user_email { get; set; }
        public string user_password { get; set; }
        public string user_type { get; set; }
        public string account_status { get; set; }
    }
}
