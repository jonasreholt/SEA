// namespace Model.FrontEndAPI;
// using Model.Survey;
// using Model.Result;

// public interface IClientRequest {
//     bool ValidateSuperUser();

//     IReadOnlySurveyWrapper GetSurvey(int surveyId); // <SurveyA,SurveyB>

//     IModifySurveyWrapper ModifySurvey(int surveyId); // Possibly (SuperUserId, SurveyId)?

//     void StoreSurveyInDatabase (IModifySurvey survey);

//     void StoreResultFromQuestion(IResult answer);

//     string ExportSurveyFromDatabase(int surveyId);

//     void ImportSurvey(string filePath);

//     string ExportResults(int surveyId);

//     void StorePicture(int surveyId, string filePath);
//     void StorePicture(int surveyId, string filePath, string optionalPrefix); //filename prefix: eg version_A_fbpic1, version_B_fbpic1
// }
