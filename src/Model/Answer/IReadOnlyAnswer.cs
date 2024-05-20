namespace Model.Answer;

public interface IReadOnlyAnswer {
    AnswerType GetAnswerType {get;}
    string?[] GetAnswers {get;}
}