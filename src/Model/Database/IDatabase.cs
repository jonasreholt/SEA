namespace Model.Database;
using System.Collections.Generic;
using Structures;


public interface IDatabase
{
    int GetUserId();
    SurveyWrapper GetSurveyWrapper(int surveyId);
    List<SurveyWrapper> GetSurveyWrappersForSuperUser(UserId userId);
    bool ImportSurvey(string path);
    List<Result> GetResults(int id);

    bool Store(SurveyWrapper surveyWrapper, UserId userId, bool overwrite = false);

    void Serialize(SurveyWrapper surveyWrapper, string path);
    Task<SurveyWrapper> Deserialize(string path);
}
