using Model.Structures;
using Model.Database;
namespace Model.FrontEndAPI;
internal class FrontEndExperimenter : IFrontEndExperimenter {

    private IDatabase databaseService;
    internal FrontEndExperimenter(IDatabase database) {
        databaseService = database;
    }
    
    public void StoreResultFromQuestion(Result answer) {
        databaseService.StoreResult(answer);   
    }
}