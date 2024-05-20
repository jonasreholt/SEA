namespace Model.Answer;


// Answer is the given options to a question, by the survey creater.
// When an experimentee takes the survey, they will give a Result
public class Answer : IModifyAnswer, IReadOnlyAnswer
{
    public AnswerType ModifyAnswerType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string?[] ModifyAnswers { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public AnswerType GetAnswerType => throw new NotImplementedException();

    public string?[] GetAnswers => throw new NotImplementedException();
}