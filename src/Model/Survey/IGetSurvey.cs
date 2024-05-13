namespace FrontEndAPI
{
    public interface IGetSurvey {
        IGetQuestion GetNextQuestion {get;}
    
        IGetQuestion GetQuestionA {get;}
    
        IGetQuestion GetQuestionB {get;}

        IGetAnswer GetAnswer {get;}
    
        IGetQuestion GetPreviousQuestion {get;}
    }
}