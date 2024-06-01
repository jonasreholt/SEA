using Model.Structures;

namespace Model.FrontEndAPI;

public interface IFrontEndSuperUser
{
    bool Store(SurveyWrapper surveyWrapper, UserId userId, bool overWrite = false);
}