using Newtonsoft.Json;
using System;

namespace GriffithTimetableConverter.Timetable.JsonModel.Griffith
{
    public class Event
    {
        [JsonProperty("start")]
        public DateTimeOffset Start { get; set; }

        [JsonProperty("end")]
        public DateTimeOffset End { get; set; }

        [JsonProperty("courseCode")]
        public string CourseCode { get; set; }

        [JsonProperty("typeCode")]
        public string TypeCode { get; set; }

        [JsonProperty("typeName")]
        public string TypeName { get; set; }

        [JsonProperty("locationNumber")]
        public string LocationNumber { get; set; }

        [JsonProperty("locationType")]
        public string LocationType { get; set; }

        [JsonProperty("locationBuilding")]
        public string LocationBuilding { get; set; }

        [JsonProperty("locationGeocode")]
        public LocationGeocode LocationGeocode { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("courseTitle")]
        public string CourseTitle { get; set; }

        [JsonProperty("campus")]
        public string Campus { get; set; }
    }
}