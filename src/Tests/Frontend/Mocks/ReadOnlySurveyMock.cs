namespace Tests.Frontend.Mocks;

using Model.Survey;
using Model.Question;

public class ReadOnlySurveyMock : IReadOnlySurvey
{
    public List<List<IReadOnlyQuestion>> Questions;
    private int i = -1;
    public int SurveyId {get; set; }
    public string SurveyName {get; set; }

    public bool PreviousQuestionExist() => i > 0;

    public bool NextQuestionExist() => i + 1 < Questions.Count;

    public IEnumerable<IReadOnlyQuestion>? TryGetNextReadOnlyQuestion()
        => i + 1 < Questions.Count ? Questions[++i] : null;

    public IEnumerable<IReadOnlyQuestion>? TryGetPreviousReadOnlyQuestion()
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
