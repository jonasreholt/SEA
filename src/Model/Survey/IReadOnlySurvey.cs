namespace FrontEndAPI;

using Question;
using Answer;

public interface IReadOnlySurvey {
    IReadOnlyQuestion TryGetNextReadOnlyQuestion();

    IReadOnlyQuestion GetQuestionA {get;}

    IReadOnlyQuestion GetQuestionB {get;}

    IReadOnlyAnswer GetAnswer {get;}

    IReadOnlyQuestion GetPreviousQuestion {get;}
}