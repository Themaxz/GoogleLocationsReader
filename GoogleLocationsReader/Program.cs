using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoogleLocationsReader.Abstractions;


namespace GoogleLocationsReader
{
    class Program
    {
        static void Main(string[] args)
        {
            var locations = JsonHelper.ReadJson<GoogleLocationRoot>("locations.json");
            List<LocationRow> rows = new List<LocationRow>();
            foreach (var location in locations.Locations)
            {
                rows.Add(new LocationRow
                {
                    Time = (new DateTime(1970, 1, 1)).AddMilliseconds(double.Parse(location.Time)),
                    Latitude = location.Latitude,
                    Longitude = location.Longitude
                });
            }
            var rows2018 = rows.Where(x => x.Time.Year == 2018).ToList();
            var table = AnalyticsHelperMethods.ExportToDatatableGeneric<LocationRow>(rows2018, "Locations2018");
            AnalyticsHelperMethods.ExportToCSV(table, "MyLocations2018.csv");
        }
    }
}
