using FrontEndAPI;
using Survey;
using Result;
namespace Backend.DatabaseAPI;

public interface IDatabase {
    int GetNextSurveyID();
    bool StoreSurvey(Survey);
    Survey GetSurvey(SurveyId);
    bool ExportSurvey(Id,Path);
    bool ImportSurvey(Path);
    Result[] GetResults(Id);
    bool StoreResults(Id, QuestionId, UserId, Results);
}
