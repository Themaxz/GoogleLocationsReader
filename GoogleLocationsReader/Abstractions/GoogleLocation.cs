using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleLocationsReader.Abstractions
{
    public class GoogleLocation
    {
        [JsonProperty("timestampMs")]
        public string Time { get; set; }
        [JsonProperty("latitudeE7")]
        public int Latitude { get; set; }
        [JsonProperty("longitudeE7")]
        public int Longitude { get; set; }
        public int Accuracy { get; set; }
        public List<Activity> Activity { get; set; } = new List<Activity>();

    }
}

