using Newtonsoft.Json;

namespace GriffithTimetableConverter.Timetable.JsonModel
{
    public class LocationGeocode
    {
        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("lon")]
        public double Lon { get; set; }
    }
}