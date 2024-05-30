namespace Tests.Frontend.Mocks;

using Model.Structures;

public class ReadOnlySurveyWrapperMock
{
    private List<Survey> _survey;
    public Survey TryGetReadOnlySurveyVersion(int index) => _survey[index];

    public int GetVersionCount() => _survey.Count;

    public string SurveyWrapperName { get; }

    public int SurveyWrapperId { get; }


    public ReadOnlySurveyWrapperMock(Survey survey)
    {
        _survey = new List<Survey> { survey };
    }

    public ReadOnlySurveyWrapperMock(List<Survey> surveys)
    {
        _survey = surveys;
    }
}
