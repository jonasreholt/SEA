using Model.Answer;
using Model.Survey;

namespace Model.FrontEndAPI;

using Question = Model.Question.Question;
public static class ExampleSurvey
{
    private static int _questionId = 0;
    private static Question GetScaleQuestion(string caption, string image, string text, int min, int max)
    {
        var question = new Question(_questionId++);
        question.ModifyCaption = caption;
        question.ModifyPicture = image;
        question.ModifyText = text;
        var answer = question.ModifyAnswer;
        answer.AddAnswerOption(min.ToString());
        answer.AddAnswerOption(max.ToString());
        answer.ModifyAnswerType = AnswerType.Scale;
        return question;
    }

    private static Question GetMultiQuestion(string caption, string image, string text, string[] options)
    {
        var question = new Question(_questionId++);
        question.ModifyCaption = caption;
        question.ModifyPicture = image;
        question.ModifyText = text;
        var answer = question.ModifyAnswer;
        foreach (var option in options)
        {
            answer.AddAnswerOption(option);
        }
        answer.ModifyAnswerType = AnswerType.MultipleChoice;
        return question;
    }
    
    private static Question GetTextQuestion(string caption, string image, string text)
    {
        var question = new Question(_questionId++);
        question.ModifyCaption = caption;
        question.ModifyPicture = image;
        question.ModifyText = text;
        var answer = question.ModifyAnswer;
        answer.ModifyAnswerType = AnswerType.Text;
        return question;
    }

    internal static SurveyWrapper GetSurvey()
    {
        var q1 = GetScaleQuestion(
            "Caption1",
            string.Empty,
            "Question1",
            1,
            8);
        var q2 = GetScaleQuestion(
            string.Empty,
            string.Empty,
            "Question2",
            1, 3);
        var q3 = GetMultiQuestion(
            "Multi Question example",
            string.Empty,
            "Question3",
            ["This", "that", "Naah"]);
        var q4 = GetTextQuestion(
            "Text Caption",
            string.Empty,
            "What do you think?");



        var surveyWrap = new SurveyWrapper(123456);
        var survey = surveyWrap.AddNewVersion();
        var page1 = survey.AddNewQuestion();
        ((List<Question>)page1).Add(q1);
        ((List<Question>)page1).Add(q2);

        var page2 = survey.AddNewQuestion();
        ((List<Question>)page2).Add(q3);
        ((List<Question>)page2).Add(q4);

        return surveyWrap;
    }
}