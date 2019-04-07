using System;
using System.Collections.Generic;
using System.Text;

namespace Kinship.models.requests
{
    public class SignupRequest
    {
        public string user_name { get; set; }
        public string user_email { get; set; }
        public string user_password { get; set; }
        public string user_type { get; set; }
        public bool account_status { get; set; }
    }
}
