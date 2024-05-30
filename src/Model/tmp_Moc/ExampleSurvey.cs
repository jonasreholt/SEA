using Model.Structures;

namespace Model.FrontEndAPI;

public static class ExampleSurvey
{
    private static int _questionId = 0;
    private static Question GetScaleQuestion(string caption, string image, string text, int min, int max)
    {
        string[] arr = [min.ToString(), max.ToString()];
        var answer = new Answer(AnswerType.Scale, arr);
        var question = new Question(caption, image, text, answer);
        return question;
    }

    private static Question GetMultiQuestion(string caption, string image, string text, string[] options)
    {
        var answer = new Answer(AnswerType.MultipleChoice, options);
        var question = new Question(caption, image, text, answer);
        return question;
    }
    
    private static Question GetTextQuestion(string caption, string image, string text)
    {
        var answer = new Answer(AnswerType.Text);
        var question = new Question(caption, image, text, answer);
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

        var page1 = new Page([q1, q2]);
        var page2 = new Page([q3, q4]);

        var survey = new Survey();
        survey.Add(page1);
        survey.Add(page2);

        var surveyWrap = new SurveyWrapper(123456);
        surveyWrap.SurveyWrapperName = "Example Survey";
        surveyWrap.Add(survey);

        return surveyWrap;
    }
}