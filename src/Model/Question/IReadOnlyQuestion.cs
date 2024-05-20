namespace Model.Question;
using Model.Answer;

public interface IReadOnlyQuestion {
    int GetId {get;}
    string GetCaption {get;}
    string GetPicture {get;}
    string GetText{get;}
    Answer GetAnswer {get;}
}
