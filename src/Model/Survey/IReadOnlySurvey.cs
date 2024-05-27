namespace Model.Survey;
using System.Linq;
using Model.Question;
using Model.Answer;

public interface IReadOnlySurvey {
    int SurveyId {get;}
    string SurveyName {get;}
    bool PreviousQuestionExist();
    bool NextQuestionExist();
    IEnumerable<IReadOnlyQuestion>? TryGetNextReadOnlyQuestion();
    IEnumerable<IReadOnlyQuestion>? TryGetPreviousReadOnlyQuestion();
    void ResetCounter();
}