using Newtonsoft.Json;
using System;

namespace GriffithTimetableConverter.Timetable.JsonModel.Griffith
{
    public class KeyDate
    {
        [JsonProperty("date")]
        public DateTimeOffset Date { get; set; }

        [JsonProperty("courseCode")]
        public string CourseCode { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}