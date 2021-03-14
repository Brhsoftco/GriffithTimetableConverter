using GriffithTimetableConverter.Timetable.JsonModel.Griffith;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;

namespace GriffithTimetableConverter.Timetable.JsonModel
{
    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings()
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                ExamElementConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}