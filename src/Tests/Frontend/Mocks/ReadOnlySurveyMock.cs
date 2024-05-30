namespace Tests.Frontend.Mocks;

using Model.Structures;

public class ReadOnlySurveyMock
{
    public List<List<Question>> Questions;
    private int i = -1;
    public int SurveyId {get; set; }
    public string SurveyName {get; set; }

    public bool PreviousQuestionExist() => i > 0;

    public bool NextQuestionExist() => i + 1 < Questions.Count;

    public IEnumerable<Question>? TryGetNextReadOnlyQuestion()
        => i + 1 < Questions.Count ? Questions[++i] : null;

    public IEnumerable<Question>? TryGetPreviousReadOnlyQuestion()
    {
        if (i == Questions.Count)
        {
            i -= 2;
            return Questions[i];
        }

        return i - 1 >= 0 ? Questions[--i] : null;
    }

    public void ResetCounter()
    {
        i = -1;
    }
}
