using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kinship.models.responses
{
    public class NewRecordId
    {
        [JsonProperty(PropertyName = "$oid")]
        public string oid { get; set; }
    }

public class NewRecordResponse
{
        //public string event_name { get; set; }
        //public string event_link { get; set; }
        //public string event_date { get; set; }
        //public string event_organizer { get; set; }
        //public string event_status { get; set; }
        //public string event_description { get; set; }
        //public string event_location { get; set; }
        //public string event_issue_id { get; set; }
        //public List<string> event_cordinator_number { get; set; }
        public string photo { get; set; }
        public string address { get; set; }
        public string rating { get; set; }
        public string additional_comments { get; set; }
        public string adder { get; set; }
        public string event_id { get; set; }
        public string status { get; set; }
        public string status_changed_by { get; set; }
        public NewRecordId _id { get; set; }
}
}
