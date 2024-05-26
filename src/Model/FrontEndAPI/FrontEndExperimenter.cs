using Model.Result;
using Model.Database;
namespace Model.FrontEndAPI;
internal class FrontEndExperimenter : IFrontEndExperimenter {

    private IDatabase databaseService;
    internal FrontEndExperimenter(IDatabase database) {
        databaseService = database;
    }
    
    public void StoreResultFromQuestion(IResult answer) {
        databaseService.StoreResult(answer);   
    }
}