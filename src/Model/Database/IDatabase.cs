namespace Model.Database;
// using FrontEndAPI;
// using Survey;
// using Result;
using System.Collections.Generic;
using Structures;


internal interface IDatabase {
    int GetNextSurveyID();
    SurveyWrapper GetSurveyWrapper(int surveyId);
    List<SurveyWrapper> GetSurveyWrapperForSuperUser(string username);
    bool ExportSurvey(int id,string path);
    bool ImportSurvey(string path);
    bool TryStorePicture(string path, int surveyId);
    void StorePictureOverwrite(string path, int surveyId);
    List<Result> GetResults(int id);
    bool StoreResults(List<Result> results);
    bool StoreResult(Result result);
}
