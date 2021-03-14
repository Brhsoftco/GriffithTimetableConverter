using Newtonsoft.Json;

namespace GriffithTimetableConverter.Timetable.JsonModel.Griffith
{
    public partial class ScheduleModel
    {
        [JsonProperty("keyDates")]
        public KeyDate[] KeyDates { get; set; }

        [JsonProperty("events")]
        public Event[] Events { get; set; }

        [JsonProperty("exams")]
        public ExamElement[] Exams { get; set; }
    }

    public partial class ScheduleModel
    {
        public static ScheduleModel FromJson(string json) => JsonConvert.DeserializeObject<ScheduleModel>(json, Converter.Settings);
    }
}