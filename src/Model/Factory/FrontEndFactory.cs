namespace Model.Factory;

using Model.Database;

public static class FrontEndFactory 
{
    public static IDatabase CreateDatabase() => new DatabaseServices();
}