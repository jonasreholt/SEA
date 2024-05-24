namespace Tests.Frontend.Mocks;

using Model.FrontEndAPI;
using Model.Survey;
using Model.Result;

public class ClientRequestMock : IClientRequest
{
    public bool ValidateSuperUser() => true;


    public IReadOnlySurveyWrapper GetSurvey(int surveyId) => default;

    public IModifySurveyWrapper ModifySurvey(int surveyId) => default;

    public void StoreSurveyInDatabase(IModifySurvey survey)
    {

    }

    public void StoreResultFromQuestion(IResult answer)
    {

    }

    public string ExportSurveyFromDatabase(int surveyId) => string.Empty;

    public void ImportSurvey(string filePath)
    {

    }

    public string ExportResults(int surveyId) => string.Empty;

    public void StorePicture(int surveyId, string filePath)
    {

    }

    public void StorePicture(int surveyId, string filePath, string optionalPrefix)
    {

    }
}
