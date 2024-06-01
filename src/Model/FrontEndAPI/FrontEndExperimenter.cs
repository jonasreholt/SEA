using Model.Structures;
using Model.Database;
namespace Model.FrontEndAPI;
internal class FrontEndExperimenter : IFrontEndExperimenter 
{

    private IDatabase databaseService;
    internal FrontEndExperimenter(IDatabase database) {
        databaseService = database;
    }

    public int GetUserId() => databaseService.GetUserId();
}