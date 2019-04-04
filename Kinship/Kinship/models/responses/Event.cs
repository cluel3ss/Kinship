using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kinship.models.responses
{
    //class EventList
    //{
    //}
    public class EventId
    {
        [JsonProperty(PropertyName = "$oid")]
        public string oid { get; set; }
    }

    public class Event
    {
        public EventId _id { get; set; }
        public string event_name { get; set; }
        public string event_link { get; set; }
        public string event_date { get; set; }
        public string event_organizer { get; set; }
        public string event_status { get; set; }
        public string event_description { get; set; }
        public string event_location { get; set; }
        public string event_issue_id { get; set; }
        public List<string> event_cordinator_number { get; set; }
    }

}
