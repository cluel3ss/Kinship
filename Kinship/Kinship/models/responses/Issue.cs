using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kinship.models.responses
{
    public class IssueId
    {
        [JsonProperty(PropertyName = "$oid")]
        public string oid { get; set; }
    }

    public class Issue
    {
        public IssueId _id { get; set; }
        public string photo { get; set; }
        public string address { get; set; }
        public string rating { get; set; }
        public string additional_comments { get; set; }
        public string adder { get; set; }
        public string event_id { get; set; }
        public string status { get; set; }
        public string status_changed_by { get; set; }
    }
}
