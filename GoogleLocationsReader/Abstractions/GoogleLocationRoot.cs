using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleLocationsReader.Abstractions
{
    public class GoogleLocationRoot
    {
        [JsonProperty("locations")]
        public List<GoogleLocation> Locations { get; set; }
    }
}
