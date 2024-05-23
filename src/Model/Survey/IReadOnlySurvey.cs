namespace Model.Survey;
using System.Linq;
using Model.Question;
using Model.Answer;

public interface IReadOnlySurvey {
    int SurveyId {get;}
    string SurveyName {get;}
    bool PreviousQuestionExist {get;}
    bool NextQuestionExist {get;}
    IEnumerable<IReadOnlyQuestion>? TryGetNextReadOnlyQuestion();
    IEnumerable<IReadOnlyQuestion>? TryGetPreviousReadOnlyQuestion();
}