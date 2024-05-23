namespace Model.Question;
using Model.Answer;

public interface IReadOnlyQuestion {
    int QuestionId {get;}
    string ReadOnlyCaption {get;}
    string ReadOnlyPicture {get;}
    string ReadOnlyText{get;}
    IReadOnlyAnswer ReadOnlyAnswer {get;}
}
