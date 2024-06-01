using System.Collections;

namespace Model.Structures;

public class Page : IEnumerable<Question>
{
    private List<Question> _questions = new();

    public Page(List<Question> questions)
    {
        _questions = questions;
    }

    public void Add(Question question)
    {
        _questions.Add(question);
    }

    public void Insert(int index, Question question)
    {
        _questions.Insert(index, question);
    }

    public void Remove(Question question)
    {
        _questions.Remove(question);
    }
    
    public IEnumerator<Question> GetEnumerator()
    {
        return _questions.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}