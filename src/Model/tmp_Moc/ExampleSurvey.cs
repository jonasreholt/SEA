using Model.Structures;

namespace Model.FrontEndAPI;

public static class ExampleSurvey
{
    private static int _questionId = 0;

    private static Question GetQuestion(string caption, string image, SubQuestion[] qs)
    {
        return new Question(caption, image, qs.ToList());
    }
    
    private static Question GetScaleQuestion(string caption, string image, (string,int,int)[] qs)
    {
        var subqs = new List<SubQuestion>(qs.Length);
        foreach (var (q, min, max) in qs)
        {
            List<string> arr = [min.ToString(), max.ToString()];
            var answer = new Answer(AnswerType.Scale, arr);
            subqs.Add(new SubQuestion(q, answer));
        }

        return new Question(caption, image, subqs);
    }

    private static Question GetMultiQuestion(string caption, string image, (string, List<string>)[] qs)
    {
        var subqs = new List<SubQuestion>(qs.Length);
        foreach (var (q, options) in qs)
        {
            var answer = new Answer(AnswerType.MultipleChoice, options);
            subqs.Add(new SubQuestion(q, answer));
        }

        return new Question(caption, image, subqs);
    }
    
    private static Question GetTextQuestion(string caption, string image, string[] qs)
    {
        var subqs = new List<SubQuestion>(qs.Length);
        foreach (var q in qs)
        {
            var answer = new Answer(AnswerType.Text);
            subqs.Add(new SubQuestion(q, answer));
        }

        return new Question(caption, image, subqs);
    }

    internal static SurveyWrapper GetSurvey()
    {
        var q1 = new Question(
            "Caption1",
            @"C:\Users\StoreSpillemaskine\Pictures\Screenshots\Screenshot 2024-04-30 233442.png",
            [ new SubQuestion("Question1", new Answer(AnswerType.Scale, new List<string> {"1", "8"}))
            , new SubQuestion("Question2", new Answer(AnswerType.Scale, new List<string> {"1", "3"}))
            ]);

        var q2 = new Question(
            "Multi Question example",
            string.Empty,
            [ new SubQuestion("Question3", new Answer(AnswerType.MultipleChoice, new List<string> {"this", "that", "nah"}))
            , new SubQuestion("What do you think?", new Answer(AnswerType.Text))
            ]);
        
        var page1 = new Page([q1]);
        var page2 = new Page([q2]);

        var survey = new Survey();
        survey.SurveyName = "A";
        survey.Add(page1);
        survey.Add(page2);

        var surveyWrap = new SurveyWrapper(123456);
        surveyWrap.SurveyWrapperName = "Example Survey";
        surveyWrap.Add(survey);

        return surveyWrap;
    }
}