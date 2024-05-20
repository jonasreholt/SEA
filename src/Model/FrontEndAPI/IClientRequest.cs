using Model.FrontEndAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Survey;
using Model.Result;

namespace Model.FrontEndAPI;
public interface IClientRequest {
    bool ValidateSuperUser();


    IReadOnlySurveyWrapper GetSurvey(int surveyId); // <SurveyA,SurveyB>
    // IReadOnlySurvey GetSurvey(int surveyId);
    

    IModifySurveyWrapper ModifySurvey(int surveyId); // Possibly (SuperUserId, SurveyId)?

    void StoreSurveyInDatabase(Survey.IModifySurvey survey);

    void StoreResultFromQuestion(int surveyID, int questionsID, int userID, IResult answer);

    string ExportSurveyFromDatabase(int surveyId);

    void ImportSurvey(string filePath);

    string ExportResults(int surveyId);

    void StorePicture(int surveyId, string filePath);
    void StorePicture(int surveyId, string filePath, string optionalPrefix); //filename prefix: eg version_A_fbpic1, version_B_fbpic1
}
