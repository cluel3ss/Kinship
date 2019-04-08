using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kinship.models.requests
{
  
    public class Set
    {
        public string photo { get; set; }
        public string photo_proof { get; set; }
        public string address { get; set; }
        public string rating { get; set; }
        public string additional_comments { get; set; }
        public string additional_comments_ngo { get; set; }
        public string adder { get; set; }
        public string event_id { get; set; }
        public string status { get; set; }
        public string status_changed_by { get; set; }
        public string days_required { get; set; }
    }

    public class UpdateIssues
    {
        [JsonProperty(PropertyName = "$set")]
        public Set set { get; set; }
}
}
