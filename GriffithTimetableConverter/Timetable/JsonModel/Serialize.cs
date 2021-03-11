using Newtonsoft.Json;

namespace GriffithTimetableConverter.Timetable.JsonModel
{
    public static class Serialize
    {
        public static string ToJson(this ScheduleModel self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }
}