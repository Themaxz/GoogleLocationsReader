using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleLocationsReader.Abstractions
{
    public class LocationRow
    {
        public DateTime Time { get; set; }
        public int Latitude { get; set; }
        public int Longitude { get; set; }
    }
}
