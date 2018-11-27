using Newtonsoft.Json;
using System.Collections.Generic;

namespace GoogleLocationsReader.Abstractions
{
    public class Activity
    {
        public string TimestampMs { get; set; }
        [JsonProperty("activity")]
        public List<ActivityType> ActivityType { get; set; }
    }
}