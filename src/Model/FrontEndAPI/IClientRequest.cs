using FrontEndAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Survey;
using Result;

namespace Backend.FrontEndAPI;
public interface IClientRequest {
    bool ValidateSuperUser();

    IReadOnlySurvey GetSurvey(int surveyId);

    void ModifySurvey(int durveyId); // Possibly (SuperUserId, SurveyId)?

    void StoreSurveyInDatabase(Survey.IModifySurvey survey);

    void StoreResultFromQuestion(int surveyID, int questionsID, int userID, IResult answer);

    void ExportSurveyFromDatabase(int surveyId);

    void ImportSurvey(string filePath);

    void ExportResults(int surveyId);
}
