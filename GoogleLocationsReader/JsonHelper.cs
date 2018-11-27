using Newtonsoft.Json;
using System.Collections.Generic;
using GoogleLocationsReader.Abstractions;
using System.IO;
public static class JsonHelper
{
    public static ReturnType ReadJson<ReturnType>(string filename)
    {
        using (StreamReader r = new StreamReader(filename))
        {
            string json = r.ReadToEnd();
            return JsonConvert.DeserializeObject<ReturnType>(json);
        }

    }
}