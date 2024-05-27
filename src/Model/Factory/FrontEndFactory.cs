namespace Model.Factory;

using Model.Database;
using Model.FrontEndAPI;
public static class FrontEndFactory {
    private static DatabaseServices databaseServices = new DatabaseServices();
    public static IFrontEndMainMenu CreateMainMenu() {
        return new FrontEndMainMenu(databaseServices);
    }

    public static IFrontEndExperimenter CreateExperimenterMenu() {
        return new FrontEndExperimenter(databaseServices);
    }

    public static IFrontEndSuperUser CreateSuperUserMenu() {
        return new FrontEndSuperUserMenu(databaseServices);
    }
}