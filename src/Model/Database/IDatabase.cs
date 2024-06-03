namespace Model.Database;
using System.Collections.Generic;
using Structures;


public interface IDatabase
{
    int GetUserId();
    (int, Survey) StartSurvey(SurveyWrapper surveyWrapper);
    void StopSurvey();
    (UserId, SurveyWrapper) GetSurveyWrapper(int surveyId);
    List<SurveyWrapper> GetSurveyWrappersForSuperUser(UserId userId);
    bool Store(SurveyWrapper surveyWrapper, UserId userId, bool overwrite = false);

    void Serialize(SurveyWrapper surveyWrapper, string path);
    Task<T> Deserialize<T>(string path);

    void ExportResults(SurveyWrapper surveyWrapper, string path);
}
