using System.Collections;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Model.Structures;

[JsonConverter(typeof(PageConverter))]
public class Page : IEnumerable<Question>
{
    [JsonInclude]
    public List<Question> Questions = new();

    public Page(List<Question> questions)
    {
        Questions = questions;
    }

    public void Add(Question question)
    {
        Questions.Add(question);
    }

    public void Insert(int index, Question question)
    {
        Questions.Insert(index, question);
    }

    public void Remove(Question question)
    {
        Questions.Remove(question);
    }
    
    public IEnumerator<Question> GetEnumerator()
    {
        return Questions.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public Page Copy()
    {
        var questionCopy = new List<Question>(Questions.Count);
        foreach (var q in Questions)
        {
            questionCopy.Add(q.Copy());
        }

        return new Page(questionCopy);
    }
}

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