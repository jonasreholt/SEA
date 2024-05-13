namespace FrontEndAPI;

using Question;
using Answer;

public interface IReadOnlySurvey {
    IReadOnlyQuestion TryGetNextReadOnlyQuestion();

    IReadOnlyQuestion TryGetPreviousReadOnlyQuestion();

    IReadOnlyQuestion ReadOnlyQuestionA {get;}

    IReadOnlyQuestion ReadOnlyQuestionB {get;}

    IReadOnlyAnswer ReadOnlyAnswer {get;}
}