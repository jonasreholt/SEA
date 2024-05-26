namespace Model.Database;
// using FrontEndAPI;
// using Survey;
// using Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Survey = Model.Survey.Survey;
using Result = Model.Result.Result;
using Model.Result;
using Model.Survey;

internal interface IDatabase {
    int GetNextSurveyID();
    bool StoreSurvey(Survey survey);
    Survey GetSurvey(int surveyId);
    SurveyWrapper GetSurveyWrapper(int surveyId);
    bool ExportSurvey(int id,string path);
    bool ImportSurvey(string path);
    bool TryStorePicture(string path, int surveyId);
    void StorePictureOverwrite(string path, int surveyId);
    List<Result> GetResults(int id);
    bool StoreResults(List<Result> results);
    bool StoreResult(IResult result);
}
