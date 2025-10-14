using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Du_An_Web_Ban_Khoa_Hoc.Helpers
{
    public class DateOnlyJsonConverter : JsonConverter<DateOnly>
    {
        private readonly string _format = "yyyy-MM-dd";

        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                return DateOnly.ParseExact(reader.GetString()!, "yyyy-MM-dd");
            }
            else if (reader.TokenType == JsonTokenType.StartObject)
            {
                using var doc = JsonDocument.ParseValue(ref reader);
                var root = doc.RootElement;
                int year = root.GetProperty("year").GetInt32();
                int month = root.GetProperty("month").GetInt32();
                int day = root.GetProperty("day").GetInt32();

                if (year < 1 || month < 1 || month > 12 || day < 1 || day > 31)
                    throw new JsonException($"Invalid DateOnly value: {year}-{month}-{day}");

                return new DateOnly(year, month, day);
            }
            throw new JsonException("DateOnly value must be a string or an object with year/month/day.");
        }


        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(_format));
        }
    }
}
