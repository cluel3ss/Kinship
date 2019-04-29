
using System;
using Kinship.models.responses;

namespace Kinship.models
{
    public class AppSetting
    {
        public bool isSignedIn { get; set; }
        public LogIn loginData { get; set; }
        public DateTime creationDate { get; set; }
    }
}
