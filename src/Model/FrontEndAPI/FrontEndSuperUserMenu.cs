namespace Model.FrontEndAPI;

using Model.Database;
using Structures;

internal class FrontEndSuperUserMenu : IFrontEndSuperUser 
{

    private  IDatabase db = new DatabaseServices();

    internal FrontEndSuperUserMenu(DatabaseServices databaseServices) {
        this.db = databaseServices;
    }

    public bool Store(SurveyWrapper surveyWrapper, UserId userId, bool overwrite = false)
    {
        return db.Store(surveyWrapper, userId, overwrite);
    }
}