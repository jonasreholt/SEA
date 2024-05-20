namespace Model.Answer;

public interface IModifyAnswer {
    AnswerType ModifyAnswerType {get; set;}
    string?[] ModifyAnswers {get; set;}

}