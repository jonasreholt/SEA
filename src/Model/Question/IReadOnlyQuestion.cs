namespace Model.Question;
using Model.Answer;

public interface IReadOnlyQuestion {
    int ReadOnlyId {get;}
    string ReadOnlyCaption {get;}
    string ReadOnlyPicture {get;}
    string ReadOnlyText{get;}
    Answer ReadOnlyAnswer {get;}
}
