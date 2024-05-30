namespace Model.FrontEndAPI;
using Model.Structures;

public interface IFrontEndExperimenter
{
    int GetUserId();   
    void StoreResultFromQuestion(Result answer);
}