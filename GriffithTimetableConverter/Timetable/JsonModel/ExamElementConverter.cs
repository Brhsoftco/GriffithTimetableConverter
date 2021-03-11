using Newtonsoft.Json;
using System;

namespace GriffithTimetableConverter.Timetable.JsonModel
{
    internal class ExamElementConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ExamElement) || t == typeof(ExamElement?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.StartObject:
                    var objectValue = serializer.Deserialize<ExamClass>(reader);
                    return new ExamElement { ExamClass = objectValue };

                case JsonToken.StartArray:
                    var arrayValue = serializer.Deserialize<object[]>(reader);
                    return new ExamElement { AnythingArray = arrayValue };
            }
            throw new Exception("Cannot unmarshal type ExamElement");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (ExamElement)untypedValue;
            if (value.AnythingArray != null)
            {
                serializer.Serialize(writer, value.AnythingArray);
                return;
            }
            if (value.ExamClass != null)
            {
                serializer.Serialize(writer, value.ExamClass);
                return;
            }
            throw new Exception("Cannot marshal type ExamElement");
        }

        public static readonly ExamElementConverter Singleton = new ExamElementConverter();
    }
}