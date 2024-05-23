namespace Tests.Frontend.Mocks;

using Model.Answer;
using System.Collections.ObjectModel;

public class ReadOnlyAnswerMock : IReadOnlyAnswer
{
    public AnswerType ReadOnlyAnswerType {get; set; }
    public ReadOnlyCollection<string> ReadOnlyAnswers {get; set; }
}
