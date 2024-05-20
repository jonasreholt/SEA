namespace Model.Question;

public interface IModifyQuestion {
    int GetId {get;}    
    QuestionType ModifyType {get; set;} // Front end needs a way to get _QuestionTypes_
    string ModifyCaption {get; set;}
    string ModifyPicture {get; set;}
    string ModifyText {get; set;}
    Answer ModifyAnswer {get; set;}
}