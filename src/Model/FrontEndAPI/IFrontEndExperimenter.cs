namespace Model.FrontEndAPI;
using Model.Survey;
using Model.Result;

public interface IFrontEndExperimenter {
    
    void StoreResultFromQuestion(IResult answer);
}