namespace Model.Database;
// using FrontEndAPI;
// using Survey;
// using Result;
using System.Collections.Generic;
using Structures;


internal interface IDatabase
{
    int GetUserId();
    SurveyWrapper GetSurveyWrapper(int surveyId);
    List<SurveyWrapper> GetSurveyWrappersForSuperUser(UserId userId);
    bool ImportSurvey(string path);
    List<Result> GetResults(int id);

    bool Store(SurveyWrapper surveyWrapper, UserId userId, bool overwrite = false);
}
