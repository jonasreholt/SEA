namespace Backend.DatabaseAPI;
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
public interface IDatabase {
    int GetNextSurveyID();
    bool StoreSurvey(Survey survey);
    Survey GetSurvey(int surveyId);
    bool ExportSurvey(int id,string path);
    bool ImportSurvey(string path);
    List<Result> GetResults(int id);
    bool StoreResults(int id, int questionId, int userId, List<Result> results);
}
