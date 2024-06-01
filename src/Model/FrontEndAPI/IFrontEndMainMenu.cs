using Model.Database;

namespace Model.FrontEndAPI;
using Model.Structures;

public interface IFrontEndMainMenu {
    List<SurveyWrapper>? ValidateSuperUser(UserId userId);
    bool ImportSurvey(string filePath);

    SurveyWrapper? GetSurvey(int pincode);
}