namespace Model.Survey;
using System.Linq;
using Model.Question;
using Model.Answer;

public interface IReadOnlySurvey {
    IEnumerable<IReadOnlyQuestion> TryGetReadOnlyNextReadOnlyQuestion();

    IEnumerable<IReadOnlyQuestion> TryGetReadOnlyReadOnlyQuestion();

}