using System.Text.Json;
using System.Text.Json.Serialization;
using Model.Structures;

namespace backend.JsonConverters;

internal class DatabaseConverter : JsonConverter<Dictionary<UserId, List<SurveyWrapper>>>
{
    public override Dictionary<UserId, List<SurveyWrapper>>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var dict = new Dictionary<UserId, List<SurveyWrapper>>();
        var jsonObject = JsonDocument.ParseValue(ref reader).RootElement;

        foreach (var property in jsonObject.EnumerateObject())
        {
            var userId = UserId.Parse(property.Name);
            var surveyWrappers = JsonSerializer.Deserialize<List<SurveyWrapper>>(property.Value.GetRawText(), options);
            dict[userId] = surveyWrappers;
        }

        return dict;
    }

    public override void Write(Utf8JsonWriter writer, Dictionary<UserId, List<SurveyWrapper>> value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        foreach (var kvp in value)
        {
            writer.WritePropertyName(kvp.Key.ToString());
            JsonSerializer.Serialize(writer, kvp.Value, options);
        }
        
        writer.WriteEndObject();
    }
}