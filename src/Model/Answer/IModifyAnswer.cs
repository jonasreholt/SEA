namespace Answer;

using System.Drawing;
public interface IModifyAnswer {
    AnswerType SetAnswerType {set;}
    string[] SetAnswers {set;}
}