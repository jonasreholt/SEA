namespace Survey;

using Question;
using Answer;

public interface IReadOnlySurvey {
    IReadOnlyQuestion? TryGetNextReadOnlyQuestion(QuestionVersion questionVersion);

    IReadOnlyQuestion? TryGetPreviousReadOnlyQuestion(QuestionVersion questionVersion);

    IReadOnlyQuestion ReadOnlyQuestionA {get;}

    IReadOnlyQuestion ReadOnlyQuestionB {get;}

    IReadOnlyAnswer ReadOnlyAnswer {get;}
}