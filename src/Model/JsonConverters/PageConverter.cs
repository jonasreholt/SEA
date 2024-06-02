namespace backend.JsonConverters;

using System.Text.Json;
using System.Text.Json.Serialization;
using Model.Structures;

internal class PageConverter : JsonConverter<Page>
{
    public override Page? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var questions = JsonSerializer.Deserialize<List<Question>>(ref reader, options);

        return new Page(questions);
    }

    public override void Write(Utf8JsonWriter writer, Page value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value.Questions, options);
    }
}
