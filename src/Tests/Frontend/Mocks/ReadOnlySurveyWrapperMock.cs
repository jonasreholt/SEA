namespace Tests.Frontend.Mocks;

using Model.Survey;

public class ReadOnlySurveyWrapperMock : IReadOnlySurveyWrapper
{
    private List<IReadOnlySurvey> _survey;
    public IReadOnlySurvey TryGetReadOnlySurveyVersion(int index) => _survey[index];

    public int GetVersionCount() => _survey.Count;

    public string SurveyWrapperName { get; }

    public int SurveyWrapperId { get; }


    public ReadOnlySurveyWrapperMock(IReadOnlySurvey survey)
    {
        _survey = new List<IReadOnlySurvey> { survey };
    }

    public ReadOnlySurveyWrapperMock(List<IReadOnlySurvey> surveys)
    {
        _survey = surveys;
    }
}
