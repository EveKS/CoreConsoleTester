using Newtonsoft.Json;

namespace Birthdays.JsonModel
{
    public class Location
    {

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }
    }
}
