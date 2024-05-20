namespace Model.Answer;
using System.Collections.ObjectModel;

public interface IReadOnlyAnswer {
    AnswerType ReadOnlyAnswerType {get;}
    ReadOnlyCollection<string> ReadOnlyAnswers {get;}
}