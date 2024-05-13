using FrontEndAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.FrontEndAPI;
public interface IClientRequest {
    bool ValidateSuperUser();

    Survey GetSurvey(int surveyId);

    void ModifySurvey(int durveyId); // Possibly (SuperUserId, SurveyId)?

    void StoreSurveyInDatabase(Survey survey);

    void StoreResultFromQuestion(int surveyID, int questionsID, int userID, IAnswer answer);

    void ExportSurveyFromDatabase(int surveyId);

    void ImportSurvey(string filePath);

    void ExportResults(int surveyId);
}
