using System.Collections;
using System.Text.Json.Serialization;
using backend.JsonConverters;

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
