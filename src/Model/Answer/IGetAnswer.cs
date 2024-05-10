namespace Answer;

using System.Drawing;
public interface IGetAnswer {
AnswerType GetAnswerType {get;}
string?[] GetAnswers {get;}
}