using Model.Result;
using Model.Database;
namespace Model.FrontEndAPI;
public class FrontEndExperimenter : IFrontEndExperimenter {

    private IDatabase databaseService;
    public FrontEndExperimenter(IDatabase database) {
        databaseService = database;
    }
    
    public void StoreResultFromQuestion(IResult answer) {
        databaseService.StoreResult(answer);   
    }
}